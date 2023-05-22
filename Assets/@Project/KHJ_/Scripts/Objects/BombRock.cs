using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombRock : MonoBehaviour
{
	// ��ź ������
	[SerializeField]
	GameObject m_BombAction_Prefab;

	// ������ Ŭ���� ������ ����
	private GameObject m_clone;


	private void OnTriggerStay2D(Collider2D collision)
	{
		// ���� ������ ���� ���
		if (collision.CompareTag("BombRange"))
		{
			// ��ź ��ġ
			m_clone = Instantiate(m_BombAction_Prefab, this.transform.position, Quaternion.identity);
			m_clone.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll; 
			m_clone.GetComponent<Bomb>().m_boomDelay = 0;

			// ������ ������ ���� �ڷ�ƾ
			StartCoroutine(DestroyClone());

			// �� ����
			Destroy(this.gameObject);
		}
	}
	// 1f�� �� ������ ������ ����
	IEnumerator DestroyClone()
	{
		yield return new WaitForSeconds(1f);

		Destroy(m_clone);
	}
}
