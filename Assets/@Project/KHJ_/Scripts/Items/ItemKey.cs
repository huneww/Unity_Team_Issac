using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� ��ũ��Ʈ
public class ItemKey : MonoBehaviour
{
	// �浹�˻�
	private void OnTriggerEnter2D(Collider2D collision)
	{
		// �÷��̾� �浹
		if(collision.CompareTag("Player")|| collision.CompareTag("PlayerHead"))
		{
			// ȹ�� ���� �߰�
			InGameSFXManager.instance.KeyGet();

			// ���� �޾ƿ���
			Player player;
			if (collision.CompareTag("Player"))
				player = collision.GetComponent<Player>();
			else
				player = collision.transform.parent.GetComponent<Player>();

			// �÷��̾ ������ ���� �� ����
			player.Keys++;


			// ������Ʈ �ı�
			Destroy(this.gameObject);
		}

	}
}
