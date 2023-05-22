using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��ź ������
public class ItemBomb : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		// ���� �浹
		if(collision.CompareTag("Player")|| collision.CompareTag("PlayerHead"))
		{
			// �÷��̾� ������Ʈ�� ��ź++
			Player player;
			if(collision.CompareTag("Player"))
				player = collision.GetComponent<Player>();
			else
				player = collision.transform.parent.GetComponent<Player>();
			player.Bombs++;

			Debug.Log("GetBomb");

			// �ڱ��ڽ� ����
			Destroy(this.gameObject);
		}

	}
}
