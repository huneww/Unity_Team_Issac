using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
	// ���� ������
	public float m_boomDelay = -1;

	// ���� �ִϸ��̼� ������
	[SerializeField]
	GameObject m_BombAction_Prefab;

	// ��ź ȿ��
	SpriteRenderer b_renderer;

	// ��ź ���� ����Ʈ ����Ʈ
	public List<GameObject> m_bombEffect;

	// ������ �������� ������ ���� Ŭ��
	private GameObject m_Prefab_Clone;

	// ������ ����Ʈ ������ ���� Ŭ��
	private GameObject m_Effect_Clone;

	private void Awake()
	{
		b_renderer = GetComponent<SpriteRenderer>();
	}

	private void Start()
	{
		// ������ ���ÿ� �ڷ�ƾ ����
		StartCoroutine(DestroyBomb());
	}

	public void setDelay(float dt)
	{
		// ���� ������ ����
		m_boomDelay = dt;
	}

	// ��ź ���� ������ �ڷ�ƾ
	IEnumerator DestroyBomb()
	{
		// �����̰� �������� �ʾ��� ��� �⺻�� �ο�
		if (m_boomDelay == -1)
		{
			setDelay(0);
		}
		// �����̰� ������ ���¶�� ��ź ȿ�� ����
		if (m_boomDelay > 0)
		{
			StartCoroutine(colorChange());
		}


		yield return new WaitForSeconds(m_boomDelay);

		m_boomDelay = 0;

		m_Prefab_Clone = Instantiate(m_BombAction_Prefab, this.transform.position + Vector3.up, Quaternion.identity);

		m_Effect_Clone = Instantiate(m_bombEffect[Random.Range(0,m_bombEffect.Count)],this.transform.position ,Quaternion.identity);

		InGameSFXManager.instance.Boom();

		this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
		this.GetComponent<SpriteRenderer>().sprite = null;
		StartCoroutine(DestroyBombAction());
	}

	// ���� �� ������Ʈ ���� �ڷ�ƾ
	IEnumerator DestroyBombAction()
	{
		yield return new WaitForSeconds(m_boomDelay + 0.5f);

		Debug.Log("boom");

		Destroy(m_Prefab_Clone);
		Destroy(this.gameObject);
	}

	// ��ź ȿ��
	IEnumerator colorChange()
	{
		// ��ź�� ������ ������ �ǰ����� ȿ�� �ο�
		while(m_boomDelay != 0)
		{
			b_renderer.color = Color.yellow;

			yield return new WaitForSeconds(0.05f);
			b_renderer.color = Color.red;

			yield return new WaitForSeconds(0.05f);
			b_renderer.color = Color.white;

			yield return new WaitForSeconds(0.5f);
		}
	}

	// �浹�˻�
	private void OnTriggerEnter2D(Collider2D collision)
	{
		// ������⿡ ���� ���� ��� ó��
		if(collision != null)
		{

		}
	}

}
