using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
	// ���� �߷�
	public float gravity;
	// �ٴ� ������ ����� ����
	float tearHigh;

	// rigidbody2D
	Rigidbody2D t_rigid;

	// �ı� �ִϸ��̼�
	public GameObject tearPoofa;
	public GameObject bigTearPoofa;

	private void Start()
	{
		t_rigid = GetComponent<Rigidbody2D>();
		// ���� �߷� ���ӵ�
		tearHigh = -0.022f;
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
		tearHigh += -0.022f;
	}

	void onGround()
	{
		// �߷� ���ӵ��� 1.9f�� �Ǿ��� �� �ٴڿ� ��Ҵٰ� ����
		if (tearHigh < -1.9f)
		{
			// ������Ʈ ����
			Destroy(gameObject);
			InGameSFXManager.instance.TearDestroy();

			if (gameObject.name.Contains("x01Tear") || gameObject.name.Contains("x01Blood"))
			{
				// ���� ����? �ִϸ��̼� ���
				GameObject tearpoofa = Instantiate(tearPoofa, transform.position, Quaternion.identity);
			}

			if (gameObject.name.Contains("x02Tear") || gameObject.name.Contains("x02Blood"))
			{
				// ���� ����? �ִϸ��̼� ���
				GameObject tearpoofa = Instantiate(bigTearPoofa, transform.position, Quaternion.identity);
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		// ������ ���� ��ų� ������ ����� ��
		if (collision.gameObject.layer == LayerMask.NameToLayer("Block") || collision.tag == "Enemy")
		{
			// ���۴��̴� ������ ���
			if (!collision.name.Contains("Base"))
			{
				// ������Ʈ ����
				Destroy(gameObject);
				InGameSFXManager.instance.TearDestroy();

				if (gameObject.name.Contains("x01Tear") || gameObject.name.Contains("x01Blood"))
				{
					// ���� ����? �ִϸ��̼� ���
					GameObject tearpoofa = Instantiate(tearPoofa, transform.position, Quaternion.identity);
				}

				if (gameObject.name.Contains("x02Tear") || gameObject.name.Contains("x02Blood"))
				{
					// ���� ����? �ִϸ��̼� ���
					GameObject tearpoofa = Instantiate(bigTearPoofa, transform.position, Quaternion.identity);
				}
			}
		}

		// ������ ���̳� �˹����⿡ ����� ��
		if (collision.name.Contains("Fire") || collision.tag.Contains("Poop"))
		{
			// ������Ʈ ����
			Destroy(gameObject);
			InGameSFXManager.instance.TearDestroy();

			if (gameObject.name.Contains("x01Tear") || gameObject.name.Contains("x01Blood"))
			{
				// ���� ����? �ִϸ��̼� ���
				GameObject tearpoofa = Instantiate(tearPoofa, transform.position, Quaternion.identity);
			}

			if (gameObject.name.Contains("x02Tear") || gameObject.name.Contains("x02Blood"))
			{
				// ���� ����? �ִϸ��̼� ���
				GameObject tearpoofa = Instantiate(bigTearPoofa, transform.position, Quaternion.identity);
			}
		}

		// �� ���� �Ͻ� �� ���� ���
		if (collision.tag == "EnemyBullet")
		{
			// �� ������ ���� �ߵ�
			if (gameObject.name.Contains("x01Blood"))
			{
				// �� ���� ����
				Bullet_Script e_bullet = collision.GetComponent<Bullet_Script>();
				e_bullet.bulletDestroy();

				// ������Ʈ ����
				Destroy(gameObject);
				InGameSFXManager.instance.TearDestroy();

				// ���� ����? �ִϸ��̼� ���
				GameObject tearpoofa = Instantiate(tearPoofa, transform.position, Quaternion.identity);
			}

			// �� ������ ���� �ߵ�
			if (gameObject.name.Contains("x02Blood"))
			{
				// �� ���� ����
				Bullet_Script e_bullet = collision.GetComponent<Bullet_Script>();
				e_bullet.bulletDestroy();

				// ������Ʈ ����
				Destroy(gameObject);
				InGameSFXManager.instance.TearDestroy();

				// ���� ����? �ִϸ��̼� ���
				GameObject tearpoofa = Instantiate(bigTearPoofa, transform.position, Quaternion.identity);
			}
		}
	}
}
