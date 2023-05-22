using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��ٶ� �� �ı�
// ���μ��� ���̿� �°� 2���� �ı���������Ʈ ����
public class LongRock : MonoBehaviour
{
	// �ı��� �� �̹��� ������
	[SerializeField]
	GameObject m_DestroyRock_Prefab;

	// �������� Ŭ���� �����ϱ� ���� �����迭
	private GameObject[] m_clone;

	// ������ ��ġ
	float SpawnX;
	float SpawnY;

	// ��������Ʈ�� ���μ��� üũ
	bool m_trueXfalseY;

	// ���� ������ ��ġ�� ����
	private void Start()
	{
		// ��������Ʈ�� ���� ���� ���̰� ����
		SpawnX = this.GetComponent<SpriteRenderer>().sprite.bounds.size.x;
		SpawnY = this.GetComponent<SpriteRenderer>().sprite.bounds.size.y;
		SpawnX = SpawnX / 4;
		SpawnY = SpawnY / 4;

		// ���ΰ��� ũ�ٸ� ���ΰ��� 0 / bool���� false
		if (SpawnX < SpawnY)
		{
			SpawnX = 0;
			m_trueXfalseY = false;
		}
		// ���ΰ��� ũ�ٸ� ���ΰ��� 0 / bool���� true
		else if (SpawnY < SpawnX)
		{
			SpawnY = 0;
			m_trueXfalseY = true;
		}

		m_clone = new GameObject[2];

	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		// ���� ������ ���� ���
		if (collision.CompareTag("BombRange"))
		{
			// �ı��� �� �̹��� ����
			m_clone[0] = Instantiate(m_DestroyRock_Prefab, this.transform.position,Quaternion.identity);
			m_clone[0].transform.localPosition = new Vector3(SpawnX, SpawnY, 0);

			// Ȯ���� ���μ��� ���̿� ���� x�� Ȥ�� y�� ����
			if (m_trueXfalseY)
				SpawnX *= -1;
			else
				SpawnY *= -1;

			m_clone[1] = Instantiate(m_DestroyRock_Prefab, this.transform.position,Quaternion.identity);
			m_clone[1].transform.localPosition = new Vector3(SpawnX, SpawnY, 0);

			Debug.Log("Boom");

			// �� ����
			Destroy(this.gameObject);
		}
	}
}
