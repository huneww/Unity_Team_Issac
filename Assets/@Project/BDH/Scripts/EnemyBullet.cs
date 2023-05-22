using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� ��ũ��Ʈ
public class EnemyBullet : MonoBehaviour
{
	// ���� �߷�
	public float gravity;
	// �ٴ� ������ ����� ����
	float tearHigh;

	// rigidbody2D
	Rigidbody2D t_rigid;

	// �ı� �ִϸ��̼�
	public GameObject tearPoofa;

	private void Awake()
	{
		t_rigid = GetComponent<Rigidbody2D>();
		// ���� �߷� ���ӵ�
		tearHigh = -0.007f;
	}

	private void Update()
	{
		High();
		onGround();
	}

	void High()
	{
		// �߷� ���ӵ��� ���� õõ�� ������ ����
		t_rigid.AddForce(Vector3.down * gravity, ForceMode2D.Impulse);
		tearHigh += -0.007f;
	}

	void onGround()
	{
		// �߷� ���ӵ��� 2�� �Ǿ��� �� �ٴڿ� ��Ҵٰ� ����
		if (tearHigh < -2f)
		{
			// ������Ʈ ����
			Destroy(gameObject);

			// ���� ����? �ִϸ��̼� ���
			GameObject tearpoofa = Instantiate(tearPoofa, transform.position, Quaternion.identity);
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Player")
		{
			// ������Ʈ ����
			Destroy(gameObject);

			// ���� ����? �ִϸ��̼� ���
			GameObject tearpoofa = Instantiate(tearPoofa, transform.position, Quaternion.identity);

			// ���� �ǰݽ� Hit�Լ� ȣ��
			Player player = collision.GetComponent<Player>();
			player.Hit(1);
		}
		else if (collision.tag == "PlayerHead")
		{
			// ������Ʈ ����
			Destroy(gameObject);

			// ���� ����? �ִϸ��̼� ���
			GameObject tearpoofa = Instantiate(tearPoofa, transform.position, Quaternion.identity);

			// �Ӹ� �ǰݽ� PlayerHead ���� �ڵ带 �̿��� ������ Hit�Լ� ȣ��
			PlayerHead player = collision.GetComponent<PlayerHead>();
			player.Hit(1);
		}
	}
}
