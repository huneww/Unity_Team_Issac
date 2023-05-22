using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���̾� ������Ʈ
public class Fire : MonoBehaviour
{
	// �ִ� ü��
	public int MaxHp = 6;

	public int Hp;

	// ������ ��� ��ũ��Ʈ
	private DropItems drop;

	// �Ҳ� �ִϸ��̼� 
	private GameObject m_childFire;

	public Collider2D f_col;

	private void Start()
	{
		// �Ҳ� �ִϸ��̼� ��������
		m_childFire = (transform.gameObject);

		Hp = MaxHp;

		drop = this.gameObject.GetComponent<DropItems>();
	}

	// �浹�˻�
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (Hp <= 0)
			return;

		// ������ �浹 ��
		if(collision.CompareTag("PlayerBullet"))
		{
			Hp--;
			m_childFire.transform.localScale += new Vector3(-0.1f, -0.1f, 0);
			m_childFire.transform.position += new Vector3(0, -0.1f, 0);
			if (Hp <= 0)
			{
				// �ı� ����
				InGameSFXManager.instance.CampFireOff();

				m_childFire.SetActive(false);
				drop.ItemDropEvent();

				f_col.enabled = false;
			}
		}

		// ��ź�� ���� �� �ٷ� �ı�
		if (collision.CompareTag("BombRange"))
		{
            // �ı� ����
            InGameSFXManager.instance.CampFireOff();
            Hp = 0;
			m_childFire.SetActive(false);
			drop.ItemDropEvent();
		}
	}

}
