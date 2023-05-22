using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockDoor : MonoBehaviour
{
    public GameObject m_unLockAnim_Prefab;

	private GameObject m_lockAnim_Clone;

	private bool m_isLocked = true;
	private bool m_startUnLock = false;

	// �̵��� ����
	public GameObject m_NextDoor;

	// �� �ִϸ�����
	private Animator m_animator;

	// �����̾�� ó���� �ڽ� ������Ʈ
	private GameObject m_childBlock;

	// ���� ����üũ
	public bool m_isOpen;

	// �׹��� ����ĳ��Ʈ ��������
	Vector3 upOffset;
	Vector3 downOffset;
	Vector3 leftOffset;
	Vector3 rightOffset;

	private void Start()
	{
		// ��Ϸ��̾� ������ �ڽĿ�����Ʈ
		m_childBlock = transform.GetChild(5).gameObject;

		// �ִϸ����� �޾ƿ���
		m_animator = transform.GetChild(3).GetComponent<Animator>();

		CloseTheDoor();

		// �������� ����
		upOffset = transform.position + (Vector3.up * 1.5f);
		downOffset = transform.position + (Vector3.down * 1.5f);
		leftOffset = transform.position + (Vector3.left * 1.5f);
		rightOffset = transform.position + (Vector3.right * 1.5f);


		// ����ĳ��Ʈ
		RaycastHit2D rayUp = Physics2D.Raycast(upOffset, Vector3.up, 1f, LayerMask.GetMask("Door"));
		RaycastHit2D rayDown = Physics2D.Raycast(downOffset, Vector3.down, 1f, LayerMask.GetMask("Door"));
		RaycastHit2D rayLeft = Physics2D.Raycast(leftOffset, Vector3.left, 1f, LayerMask.GetMask("Door"));
		RaycastHit2D rayRight = Physics2D.Raycast(rightOffset, Vector3.right, 1f, LayerMask.GetMask("Door"));

		// ����ĳ��Ʈ �迭ȭ
		RaycastHit2D[] rays = new RaycastHit2D[4]
			{rayUp,rayDown,rayLeft,rayRight};

		// ����ĳ��Ʈ �˻�
		for (int i = 0; i < rays.Length; i++)
		{
			if (rays[i])
			{
				m_NextDoor = rays[i].transform.GetChild(4).gameObject;
				break;
			}
		}
	}

	// �浹�˻� Ʈ����
	private void OnTriggerEnter2D(Collider2D collision)
	{
		// �÷��̾� �浹 ��
		if (collision.CompareTag("Player"))
		{
			var player = collision.GetComponent<Player>();
			if (m_isLocked && !m_startUnLock&& player.Keys >= 1)
			{
				player.Keys -= 1;

				m_lockAnim_Clone = Instantiate(m_unLockAnim_Prefab, transform);
			//	m_lockAnim_Clone.transform.localPosition = new Vector2(0f,0f);
				StartCoroutine(CheckFinishAnim());
				m_startUnLock = true;
			}

			if (!m_isLocked)
			{
				// �÷��̾� �ش� ��ġ�� �̵�
				collision.gameObject.transform.position = m_NextDoor.transform.position;

				// ī�޶� ��ġ �̵�
				var camera = GameObject.FindGameObjectWithTag("MainCamera");
				camera.transform.position = m_NextDoor.transform.parent.parent.transform.position + new Vector3(0, 0, -10);
			}
		}
	}

	IEnumerator CheckFinishAnim()
	{
		while(true)
		{
			yield return new WaitForSeconds(0.01f);
			
			var animator = m_lockAnim_Clone.GetComponent<Animator>();

			if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f)
			{
				Destroy(m_lockAnim_Clone);
				OpenTheDoor();
				var collider = transform.GetChild(3).GetComponent<BoxCollider2D>();
				collider.offset = new Vector2(0, 0.2f);
				collider.size = new Vector2(1f, 0.55f);
				break;
			}
		}
		m_isLocked = false;
	}

	// ������ �޼���
	public void OpenTheDoor()
	{
		m_animator.Play("OpenDoor");

		m_isOpen = true;
		// �� ������Ʈ ��Ȱ��ȭ
		m_childBlock.SetActive(false);
	}

	// ������ �޼���
	public void CloseTheDoor()
	{
		m_animator.Play("CloseDoor");

		m_isOpen = false;
		// �� ������Ʈ Ȱ��ȭ
		m_childBlock.SetActive(true);
	}

}
