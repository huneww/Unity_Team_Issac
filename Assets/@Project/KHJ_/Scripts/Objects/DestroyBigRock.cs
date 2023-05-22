using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ū�� �ı�
// �ı� ������Ʈ�� 2���� ���
public class DestroyBigRock : MonoBehaviour
{
	// ������ �ı�������Ʈ ������
	[SerializeField]
	GameObject m_DestroyRock_Prefab;

	// ������ ������ Ŭ���� ������ �����迭 
	GameObject[] m_clone;

	// ������ ��ġ��
	float SpawnX;
	float SpawnY;

	// ���� �� ���� ������ �ı�������Ʈ ��ġ�� ����
	private void Start()
	{
		SpawnX = this.GetComponent<SpriteRenderer>().sprite.bounds.size.x;
		SpawnX = SpawnX / 4;
		SpawnY = this.GetComponent<SpriteRenderer>().sprite.bounds.size.y;
		SpawnY = SpawnY / 4;

		m_clone = new GameObject[2];

	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		// ���� ������ ���� ���
		if (collision.CompareTag("BombRange"))
		{
			// �ı� ���� �߰�
			InGameSFXManager.instance.RockDestroy(Random.Range(0, 2));

			// �ı��� �� �̹���1 ����
			m_clone[0] = Instantiate(m_DestroyRock_Prefab,transform.position,Quaternion.identity);
			m_clone[0].transform.localPosition = new Vector3(SpawnX,SpawnY,0);

			// ������ ��ġ ����
			SpawnX *= -1;
			SpawnY *= -1;

			// �ı��� �� �̹���2 ����
			m_clone[1] = Instantiate(m_DestroyRock_Prefab, transform.position,Quaternion.identity);
			m_clone[1].transform.localPosition = new Vector3(SpawnX, SpawnY, 0);
			Debug.Log("Boom");

			// �� ����
			Destroy(this.gameObject);
		}
	}
}
