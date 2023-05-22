using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ������ ��Ʈ
public class ItemHeart : MonoBehaviour
{
	// ��Ʈ�� ����
	public enum HeartType
	{
		Red,
		HalfRed,
		Soul,
		HalfSoul,
	}

	// ��Ʈ �к��� ����
	public HeartType Type;

	// ȸ����
	int m_Heart;
	int m_Soul;

	// ���� �� Ÿ�Կ� ���� ȸ���� ����
	private void Start()
	{
		switch(Type)
		{
			case HeartType.Red:
				m_Heart = 2;
				m_Soul = 0;
				break;

			case HeartType.HalfRed:
				m_Heart = 1;
				m_Soul = 0;
				break;

			case HeartType.Soul:
				m_Heart = 0;
				m_Soul = 2;
				break;

			case HeartType.HalfSoul:
				m_Heart = 0;
				m_Soul = 1;
				break;
		}	
	}

	// �浹 �˻�
	private void OnTriggerEnter2D(Collider2D collision)
	{
		// �÷��̾��� ������ ��������
		Player player;

		// �÷��̾ �浹�ϸ�
		if (collision.CompareTag("Player")|| collision.CompareTag("PlayerHead"))
		{
			
			if (collision.CompareTag("Player"))
				player = collision.GetComponent<Player>();
			else
				player = collision.transform.parent.GetComponent<Player>();

			if ((player.curRealHeart + m_Heart <= player.maxRealHeart) && m_Heart != 0)
			{
				player.curHeart += m_Heart;
				player.curRealHeart += m_Heart;
				
				// �ڱ��ڽ� �����
				Destroy(this.gameObject);
			}
			else if (player.curHeart + m_Soul <= player.maxHeart && m_Soul != 0)
			{
				player.curHeart += m_Soul;
				player.curSoulHeart += m_Soul;

				// �ڱ��ڽ� �����
				Destroy(this.gameObject);
			}
		}
	}
}
