using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ����
// ���� �ı��� ������ ������ ���
public class ColorRock : MonoBehaviour
{
	// �����ų ������ ��� List
	public List<GameObject> m_drop_Prefab;

	// �ı��� �� �̹���
	[SerializeField]
	GameObject m_DestroyRock_Prefab;

	bool a = true;

    private void OnTriggerEnter2D(Collider2D collision)
	{
		// ���� ������ ���� ���
		if (collision.CompareTag("BombRange") && a)
		{
			a = false;

			// �ı��� �� �̹��� ����
			Instantiate(m_DestroyRock_Prefab, this.transform.position,Quaternion.identity);
			Debug.Log("Boom");
			DropRandomItems();
			// ���� �浹�ڽ� ���� �� �̹��� ����
			Destroy(this.gameObject);
		}
	}

	private void DropRandomItems()
	{

		// ��� ����
		int rand = Random.Range(0, m_drop_Prefab.Count);

		// ���õ� ������ ���
		Instantiate(m_drop_Prefab[rand], this.transform.position, Quaternion.identity);

	}

}
