using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBody : MonoBehaviour
{
	// �÷��̾� ��ü
	public GameObject playerObj;
	Player player;

	private void Start()
	{
		// �÷��̾� ������Ʈ ��������
		player = playerObj.GetComponent<Player>();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		// �Ӹ� ���� �� ���� ��ź�� �¾��� ��
		if (collision.tag == "BombRange")
		{
			// ��Ʈ
			player.Hit(2);
		}

		// �Ӹ� ���� �� ���� �ҿ� ����� ��
		if (collision.name.Contains("Fire"))
		{
			// ��Ʈ
			player.Hit(1);
		}
	}
}
