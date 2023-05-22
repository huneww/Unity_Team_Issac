using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �� ��ũ��Ʈ ������
public class NewDoorScripts : MonoBehaviour
{
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

	MapTrigger floor;

	private void Start()
	{
		// ��Ϸ��̾� ������ �ڽĿ�����Ʈ
		m_childBlock = transform.GetChild(5).gameObject;

		// �ִϸ����� �޾ƿ���
		m_animator = transform.GetChild(3).GetComponent<Animator>();

		floor = gameObject.transform.parent.GetChild(1).GetComponent<MapTrigger>();

		// �������� ����
		upOffset = transform.position + (Vector3.up*1.5f);
		downOffset = transform.position + (Vector3.down*1.5f);
		leftOffset = transform.position + (Vector3.left*1.5f);
		rightOffset = transform.position + (Vector3.right*1.5f);


		// ����ĳ��Ʈ
		RaycastHit2D rayUp = Physics2D.Raycast(upOffset, Vector3.up, 1f, LayerMask.GetMask("Door"));
		RaycastHit2D rayDown = Physics2D.Raycast(downOffset, Vector3.down, 1f, LayerMask.GetMask("Door"));
		RaycastHit2D rayLeft = Physics2D.Raycast(leftOffset, Vector3.left, 1f, LayerMask.GetMask("Door"));
		RaycastHit2D rayRight = Physics2D.Raycast(rightOffset, Vector3.right, 1f, LayerMask.GetMask("Door"));

		// ����ĳ��Ʈ �迭ȭ
		RaycastHit2D[] rays = new RaycastHit2D[4]
			{rayUp,rayDown,rayLeft,rayRight};

		// ����ĳ��Ʈ �˻�
		for(int i = 0; i < rays.Length; i++)
		{
			if(rays[i])
			{
				m_NextDoor = rays[i].transform.GetChild(4).gameObject;
				break;
			}
		}
	}

	private void Update()
	{
		if (!floor.isClear && m_isOpen)
		{
			CloseTheDoor();
		}
		else if (floor.isClear && !m_isOpen)
		{
			OpenTheDoor();
		}
		
	}

	// �浹�˻� Ʈ����
	private void OnTriggerEnter2D(Collider2D collision)
	{
		// �÷��̾� �浹 ��
		if (collision.CompareTag("Player"))
		{
			// �÷��̾� �ش� ��ġ�� �̵�
			collision.gameObject.transform.position = m_NextDoor.transform.position;

			// ī�޶� ��ġ �̵�
			var camera = GameObject.FindGameObjectWithTag("MainCamera");
			camera.transform.position = m_NextDoor.transform.parent.parent.transform.position + new Vector3(0,0,-10);
		}
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
