using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//===================================

//		��� ���ϴ� ��ũ��Ʈ

//===================================

// �� ��ũ��Ʈ
// �ΰ��� �� ������Ʈ�� ���� �۵��ؾߵ�
public class DoorActive : MonoBehaviour
{
	// �÷��̾ �̵��� ��ġ ������Ʈ
	// ��ǥ��
	public GameObject m_nextDoor;

	// �浹�˻�
	private void OnTriggerEnter2D(Collider2D collision)
	{
		// �÷��̾� �浹 ��
		if(collision.CompareTag("Player"))
		{
			// �ش� ��ġ�� �̵�
			collision.gameObject.transform.position = m_nextDoor.transform.position;
		}
	}
}
