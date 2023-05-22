using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �� ������Ʈ
public class Poop : MonoBehaviour
{
	// ü��(1��� 1�� ����)
	[SerializeField]
	int Hp;

	// ������ ��� ��ũ��Ʈ
	private DropItems drop;

	public int HP { get { return Hp; } }

	// ü�´� ����� �̹��� List
	public List<Sprite> sprites = new List<Sprite>();

	// ���� ���� �� Hp�� List�� ũ�⸸ŭ ����
	private void Start()
	{
		Hp = sprites.Count - 1;
		drop = this.gameObject.GetComponent<DropItems>();
	}

	// �浹 �˻�
	private void OnTriggerEnter2D(Collider2D collision)
	{
		// Hp�� 0���� �۾����ٸ� 0���� ����
		if (Hp <= 0)
			return;
		// ���� �浹 ��
		if(collision.CompareTag("PlayerBullet"))
		{
			// Hp -1
			Hp--;
			// ���� Hp�� 0���� �۾����ٸ� 
			if (Hp <= 0)
			{
				// Hp�� 0���� ����
				Hp = 0;
				// �浹�˻�� BoxCollider ��Ȱ��ȭ
				this.gameObject.GetComponent<CircleCollider2D>().enabled = false;
				drop.ItemDropEvent();

				// �ı� ���� �߰�
				InGameSFXManager.instance.Poop();
			}
			// �浹�� ���� �̹��� �ٲٱ�
			this.transform.GetComponent<SpriteRenderer>().sprite = sprites[Hp];

			
		}

		if(collision.CompareTag("BombRange"))
		{
			// Hp --
			Hp -= 5;
			// ���� Hp�� 0���� �۾����ٸ� 
			if (Hp <= 0)
			{
				// Hp�� 0���� ����
				Hp = 0;
				// �浹�˻�� BoxCollider ��Ȱ��ȭ
				this.gameObject.GetComponent<CircleCollider2D>().enabled = false;
				drop.ItemDropEvent();
			}
			// �浹�� ���� �̹��� �ٲٱ�
			this.transform.GetComponent<SpriteRenderer>().sprite = sprites[Hp];
		}
	}
}
