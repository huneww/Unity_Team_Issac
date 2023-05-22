using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHead : MonoBehaviour
{
	// �÷��̾� �̵� ���� int �ʵ�
	public float hAxis;
	public float vAxis;

	// ��������Ʈ �ø��� ���� bool �ʵ�
	bool isFliped;

	// ���� ���� ��ư
	public float vFire;
	public float hFire;

	// �������ͽ�
	// ���� ������
	float fireDelay;
	float maxDelay;
	float headDelay;
	// ��Ÿ�
	public float fireRange;

	// ����ü ������Ʈ
	public GameObject x1Tear;
	public GameObject x2Tear;
	// ���� ����ü ������Ʈ
	public GameObject x1Blood;
	public GameObject x2Blood;

	// ���⿡ ���� �� ��������Ʈ ����� ���� ��������Ʈ �迭
	public Sprite[] Heades;

	// ������ ȹ�� ���� �ٸ� ��������Ʈ �迭
	public Sprite[] onionHeades;

	// ��������Ʈ ������
	SpriteRenderer h_renderer;

	// �÷��̾� ��ü
	public GameObject playerObj;
	Player player;

	// ���� ��ȭ�� ������Ʈ
	public GameObject[] itemObjs;

	// ���� ��ȭ ������ ���� bool����
	bool stigmaActive;
	bool crownActive;
	bool onionActive;
	bool brimstoneActive;

	private void Start()
	{
		// �� ������Ʈ ��������
		player = playerObj.GetComponent<Player>();
		h_renderer = GetComponent<SpriteRenderer>();
	}

	// Update is called once per frame
	void Update()
	{
		GetInput();
		FlipX();
		HeadSide();
		Shot();
		plusFace();
	}

	// ��ư �Է� ���� �Լ�
	void GetInput()
	{
		// �̵� ���� ��ư ����
		hAxis = Input.GetAxisRaw("Horizontal");
		vAxis = Input.GetAxisRaw("Vertical");
		// ���� ���� ��ư ����
		hFire = Input.GetAxisRaw("FireHorizontal");
		vFire = Input.GetAxisRaw("FireVertical");
	}

	// �̵� �� �Ӹ� ����
	void HeadSide()
	{
		if (!Input.GetButton("FireHorizontal") && !Input.GetButton("FireVertical"))
		{
			// ���� �Ӹ� ������
			if (headDelay < 0)
			{
				if (vAxis != 0)
				{
					// �밢 �̵��� �ƴ� ��
					if (hAxis == 0)
					{
						// �Ʒ��� �̵�
						if (vAxis == -1)
						{
							// ���� �Ӹ�
							h_renderer.sprite = Heades[0];
						}
						// ���� �̵�
						else
						{
							// �ĸ� �Ӹ�
							h_renderer.sprite = Heades[2];
						}
					}
					// �밢 �̵��� ��
					else
					{
						// �� �Ӹ�
						h_renderer.sprite = Heades[1];
					}
				}
				// �¿� �̵��� ��
				else if (hAxis != 0)
				{
					// �� �Ӹ�
					h_renderer.sprite = Heades[1];
				}
				// Idle ������ ��
				else
				{
					// ���� �Ӹ�
					h_renderer.sprite = Heades[0];
				}
			}
		}
		else if (Input.GetButton("FireVertical") && !Input.GetButton("FireHorizontal"))
		{
			// ���� �Ӹ� ������
			if (headDelay < 0)
			{
				// ���� ������ ��
				if (vFire == 1)
				{
					// �ĸ� �Ӹ�
					h_renderer.sprite = Heades[2];
				}
				// �Ʒ��� ������ ��
				else if (vFire == -1)
				{
					// ���� �Ӹ�
					h_renderer.sprite = Heades[0];
				}
			}
		}
		else if (!Input.GetButton("FireVertical") && Input.GetButton("FireHorizontal"))
		{
			// ���� �Ӹ� ������
			if (headDelay < 0)
			{
				// �¿� ������ ��
				if (hFire != 0)
				{
					// �� �Ӹ�
					h_renderer.sprite = Heades[1];
				}
			}
		}
	}

	// ��������Ʈ �¿� �ø� �Լ�
	void FlipX()
	{
		// ���� �� �� ���� ���� (������ ������ ���)
		if (Input.GetButton("FireHorizontal") && headDelay < 0)
		{
			// ������ ���� ���� ���� �� �������� ����
			if (!isFliped && hFire == -1)
			{
				// ��������Ʈ �ø�
				h_renderer.flipX = true;
				isFliped = true;
			}
			// �������� ���� ���� ���� �� ������ ����
			else if (isFliped && hFire == 1)
			{
				// ��������Ʈ �ø�
				h_renderer.flipX = false;
				isFliped = false;
			}
		}

		// �� �� �̵� ��ư�� ������ �� (�����ϰ� ���� ���� ��)
		if (!Input.GetButton("FireHorizontal") && Input.GetButton("Horizontal"))
		{
			// ������ ���� ���� ���� ��
			if (!isFliped && hAxis == -1 && hAxis != 0)
			{
				// ��������Ʈ �ø�
				h_renderer.flipX = true;
				isFliped = true;
			}
			// �������� ���� ���� ���� ��
			else if (isFliped && hAxis == 1 && hAxis != 0)
			{
				// ��������Ʈ �ø�
				h_renderer.flipX = false;
				isFliped = false;
			}
		}
	}

	// ���� �� �Ӹ� ����
	void Shot()
	{
		// ��ü���� �� ������ ��������
		maxDelay = player.curTears;

		// ���� ������ ����
		fireDelay -= Time.deltaTime;
		// �Ӹ� ������ ����
		headDelay -= Time.deltaTime;

		// ���� �����̰� 0���� ���� ��� ���� ����
		if (fireDelay < 0)
		{
			// �� �� ���� ��
			if (Input.GetButton("FireVertical"))
			{
				// ���� ������ �Ʒ��� ���
				if (vFire < 0)
				{
					InGameSFXManager.instance.PlayerTearFire();

					// �Ʒ� ���� ��������Ʈ
					h_renderer.sprite = Heades[3];
					// ��������Ʈ ���� ������
					headDelay = 0.1f;
					// ���� ������ �ʱ�ȭ
					fireDelay = maxDelay;

					// ū ���� ������ ������
					if (player.haveItems[0] == Alter.Collection.TheSadOnion)
					{
						// �� �� ���� �߻� ������ ������
						if (player.haveItems[3] == Alter.Collection.Stigmata)
						{
							// ����ü �߻�
							GameObject tear1 = Instantiate(x2Tear, transform.position + Vector3.down, transform.rotation);
							Rigidbody2D rigid1 = tear1.GetComponent<Rigidbody2D>();
							rigid1.AddForce(Vector3.down * fireRange, ForceMode2D.Impulse);

							// ����ü �߻�
							GameObject tear2 = Instantiate(x2Tear, transform.position + Vector3.down, transform.rotation);
							Rigidbody2D rigid2 = tear2.GetComponent<Rigidbody2D>();
							rigid2.AddForce(Vector3.down * fireRange, ForceMode2D.Impulse);
							rigid2.AddForce(Vector3.left * 0.5f * fireRange, ForceMode2D.Impulse);

							// ����ü �߻�
							GameObject tear3 = Instantiate(x2Tear, transform.position + Vector3.down, transform.rotation);
							Rigidbody2D rigid3 = tear3.GetComponent<Rigidbody2D>();
							rigid3.AddForce(Vector3.down * fireRange, ForceMode2D.Impulse);
							rigid3.AddForce(Vector3.right * 0.5f * fireRange, ForceMode2D.Impulse);
						}
						// ������ �ϳ� �� ��
						else
						{
							// ����ü �߻�
							GameObject tear = Instantiate(x2Tear, transform.position + Vector3.down, transform.rotation);
							Rigidbody2D rigid = tear.GetComponent<Rigidbody2D>();
							rigid.AddForce(Vector3.down * fireRange, ForceMode2D.Impulse);
						}
					}
					// ���� ������ ��
					else
					{
						// �� �� ���� �߻� ������ ������
						if (player.haveItems[3] == Alter.Collection.Stigmata)
						{
							// ����ü �߻�
							GameObject tear = Instantiate(x1Tear, transform.position + Vector3.down, transform.rotation);
							Rigidbody2D rigid = tear.GetComponent<Rigidbody2D>();
							rigid.AddForce(Vector3.down * fireRange, ForceMode2D.Impulse);

							// ����ü �߻�
							GameObject tear2 = Instantiate(x1Tear, transform.position + Vector3.down, transform.rotation);
							Rigidbody2D rigid2 = tear2.GetComponent<Rigidbody2D>();
							rigid2.AddForce(Vector3.down * fireRange, ForceMode2D.Impulse);
							rigid2.AddForce(Vector3.left * 0.5f * fireRange, ForceMode2D.Impulse);

							// ����ü �߻�
							GameObject tear3 = Instantiate(x1Tear, transform.position + Vector3.down, transform.rotation);
							Rigidbody2D rigid3 = tear3.GetComponent<Rigidbody2D>();
							rigid3.AddForce(Vector3.down * fireRange, ForceMode2D.Impulse);
							rigid3.AddForce(Vector3.right * 0.5f * fireRange, ForceMode2D.Impulse);
						}
						// ������ �ϳ� �� ��
						else
						{
							// ����ü �߻�
							GameObject tear = Instantiate(x1Tear, transform.position + Vector3.down, transform.rotation);
							Rigidbody2D rigid = tear.GetComponent<Rigidbody2D>();
							rigid.AddForce(Vector3.down * fireRange, ForceMode2D.Impulse);
						}
					}
				}
				// ���� ������ ���� ���
				else
				{
					InGameSFXManager.instance.PlayerTearFire();

					// �� ���� ��������Ʈ
					h_renderer.sprite = Heades[5];
					// ��������Ʈ ���� ������
					headDelay = 0.1f;
					// ���� ������ �ʱ�ȭ
					fireDelay = maxDelay;

					// ū ���� ������ ������
					if (player.haveItems[0] == Alter.Collection.TheSadOnion)
					{
						// �� �� ���� �߻� ������ ������
						if (player.haveItems[3] == Alter.Collection.Stigmata)
						{
							// ����ü �߻�
							GameObject tear = Instantiate(x2Tear, transform.position + Vector3.up, transform.rotation);
							Rigidbody2D rigid = tear.GetComponent<Rigidbody2D>();
							rigid.AddForce(Vector3.up * fireRange, ForceMode2D.Impulse);

							// ����ü �߻�
							GameObject tear2 = Instantiate(x2Tear, transform.position + Vector3.up, transform.rotation);
							Rigidbody2D rigid2 = tear2.GetComponent<Rigidbody2D>();
							rigid2.AddForce(Vector3.up * fireRange, ForceMode2D.Impulse);
							rigid2.AddForce(Vector3.left * 0.5f * fireRange, ForceMode2D.Impulse);

							// ����ü �߻�
							GameObject tear3 = Instantiate(x2Tear, transform.position + Vector3.up, transform.rotation);
							Rigidbody2D rigid3 = tear3.GetComponent<Rigidbody2D>();
							rigid3.AddForce(Vector3.up * fireRange, ForceMode2D.Impulse);
							rigid3.AddForce(Vector3.right * 0.5f * fireRange, ForceMode2D.Impulse);
						}
						// ������ �ϳ� �� ��
						else
						{
							// ����ü �߻�
							GameObject tear = Instantiate(x2Tear, transform.position + Vector3.up, transform.rotation);
							Rigidbody2D rigid = tear.GetComponent<Rigidbody2D>();
							rigid.AddForce(Vector3.up * fireRange, ForceMode2D.Impulse);
						}
					}
					// ���� ������ ��
					else
					{
						// �� �� ���� �߻� ������ ������
						if (player.haveItems[3] == Alter.Collection.Stigmata)
						{
							// ����ü �߻�
							GameObject tear = Instantiate(x1Tear, transform.position + Vector3.up, transform.rotation);
							Rigidbody2D rigid = tear.GetComponent<Rigidbody2D>();
							rigid.AddForce(Vector3.up * fireRange, ForceMode2D.Impulse);

							// ����ü �߻�
							GameObject tear2 = Instantiate(x1Tear, transform.position + Vector3.up, transform.rotation);
							Rigidbody2D rigid2 = tear2.GetComponent<Rigidbody2D>();
							rigid2.AddForce(Vector3.up * fireRange, ForceMode2D.Impulse);
							rigid2.AddForce(Vector3.left * 0.5f * fireRange, ForceMode2D.Impulse);

							// ����ü �߻�
							GameObject tear3 = Instantiate(x1Tear, transform.position + Vector3.up, transform.rotation);
							Rigidbody2D rigid3 = tear3.GetComponent<Rigidbody2D>();
							rigid3.AddForce(Vector3.up * fireRange, ForceMode2D.Impulse);
							rigid3.AddForce(Vector3.right * 0.5f * fireRange, ForceMode2D.Impulse);
						}
						// ������ �ϳ� �� ��
						else
						{
							// ����ü �߻�
							GameObject tear = Instantiate(x1Tear, transform.position + Vector3.up, transform.rotation);
							Rigidbody2D rigid = tear.GetComponent<Rigidbody2D>();
							rigid.AddForce(Vector3.up * fireRange, ForceMode2D.Impulse);
						}
					}
				}
			}
			// ���� ������ �¿� �� ���
			else if (Input.GetButton("FireHorizontal"))
			{
				InGameSFXManager.instance.PlayerTearFire();

				// ���� ���� ��������Ʈ
				h_renderer.sprite = Heades[4];
				// ��������Ʈ ���� ������
				headDelay = 0.1f;
				// ���� ������ �ʱ�ȭ
				fireDelay = maxDelay;
				// ���� ������ ������ ���
				if (hFire < 0)
				{
					// ū ���� ������ ������
					if (player.haveItems[0] == Alter.Collection.TheSadOnion)
					{
						// �� �� ���� �߻� ������ ������
						if (player.haveItems[3] == Alter.Collection.Stigmata)
						{
							// ����ü �߻�
							GameObject tear = Instantiate(x2Tear, transform.position + Vector3.left, transform.rotation);
							Rigidbody2D rigid = tear.GetComponent<Rigidbody2D>();
							rigid.AddForce(Vector3.left * fireRange, ForceMode2D.Impulse);
							rigid.AddForce(Vector3.up * 0.3f * fireRange, ForceMode2D.Impulse);

							// ����ü �߻�
							GameObject tear2 = Instantiate(x2Tear, transform.position + Vector3.left, transform.rotation);
							Rigidbody2D rigid2 = tear2.GetComponent<Rigidbody2D>();
							rigid2.AddForce(Vector3.left * fireRange, ForceMode2D.Impulse);

							// ����ü �߻�
							GameObject tear3 = Instantiate(x2Tear, transform.position + Vector3.left, transform.rotation);
							Rigidbody2D rigid3 = tear3.GetComponent<Rigidbody2D>();
							rigid3.AddForce(Vector3.left * fireRange, ForceMode2D.Impulse);
							rigid3.AddForce(Vector3.down * 0.3f * fireRange, ForceMode2D.Impulse);
						}
						// ������ �ϳ� �� ��
						else
						{
							// ����ü �߻�
							GameObject tear = Instantiate(x2Tear, transform.position + Vector3.left, transform.rotation);
							Rigidbody2D rigid = tear.GetComponent<Rigidbody2D>();
							rigid.AddForce(Vector3.left * fireRange, ForceMode2D.Impulse);
						}

					}
					// ���� ������ ��
					else
					{
						// �� �� ���� �߻� ������ ������
						if (player.haveItems[3] == Alter.Collection.Stigmata)
						{
							// ����ü �߻�
							GameObject tear = Instantiate(x1Tear, transform.position + Vector3.left, transform.rotation);
							Rigidbody2D rigid = tear.GetComponent<Rigidbody2D>();
							rigid.AddForce(Vector3.left * fireRange, ForceMode2D.Impulse);
							rigid.AddForce(Vector3.up * 0.3f * fireRange, ForceMode2D.Impulse);

							// ����ü �߻�
							GameObject tear2 = Instantiate(x1Tear, transform.position + Vector3.left, transform.rotation);
							Rigidbody2D rigid2 = tear2.GetComponent<Rigidbody2D>();
							rigid2.AddForce(Vector3.left * fireRange, ForceMode2D.Impulse);

							// ����ü �߻�
							GameObject tear3 = Instantiate(x1Tear, transform.position + Vector3.left, transform.rotation);
							Rigidbody2D rigid3 = tear3.GetComponent<Rigidbody2D>();
							rigid3.AddForce(Vector3.left * fireRange, ForceMode2D.Impulse);
							rigid3.AddForce(Vector3.down * 0.3f * fireRange, ForceMode2D.Impulse);
						}
						// ������ �ϳ� �� ��
						else
						{
							// ����ü �߻�
							GameObject tear = Instantiate(x1Tear, transform.position + Vector3.left, transform.rotation);
							Rigidbody2D rigid = tear.GetComponent<Rigidbody2D>();
							rigid.AddForce(Vector3.left * fireRange, ForceMode2D.Impulse);
						}
					}
				}
				// ���� ������ ������
				else
				{
					InGameSFXManager.instance.PlayerTearFire();

					// ū ���� ������ ������
					if (player.haveItems[0] == Alter.Collection.TheSadOnion)
					{
						// �� �� ���� �߻� ������ ������
						if (player.haveItems[3] == Alter.Collection.Stigmata)
						{
							// ����ü �߻�
							GameObject tear = Instantiate(x2Tear, transform.position + Vector3.right, transform.rotation);
							Rigidbody2D rigid = tear.GetComponent<Rigidbody2D>();
							rigid.AddForce(Vector3.right * fireRange, ForceMode2D.Impulse);
							rigid.AddForce(Vector3.up * 0.3f * fireRange, ForceMode2D.Impulse);

							// ����ü �߻�
							GameObject tear2 = Instantiate(x2Tear, transform.position + Vector3.right, transform.rotation);
							Rigidbody2D rigid2 = tear2.GetComponent<Rigidbody2D>();
							rigid2.AddForce(Vector3.right * fireRange, ForceMode2D.Impulse);

							// ����ü �߻�
							GameObject tear3 = Instantiate(x2Tear, transform.position + Vector3.right, transform.rotation);
							Rigidbody2D rigid3 = tear3.GetComponent<Rigidbody2D>();
							rigid3.AddForce(Vector3.right * fireRange, ForceMode2D.Impulse);
							rigid3.AddForce(Vector3.down * 0.3f * fireRange, ForceMode2D.Impulse);
						}
						// ������ �ϳ� �� ��
						else
						{
							// ����ü �߻�
							GameObject tear = Instantiate(x2Tear, transform.position + Vector3.right, transform.rotation);
							Rigidbody2D rigid = tear.GetComponent<Rigidbody2D>();
							rigid.AddForce(Vector3.right * fireRange, ForceMode2D.Impulse);
						}
					}
					// ���� ������ ��
					else
					{
						// �� �� ���� �߻� ������ ������
						if (player.haveItems[3] == Alter.Collection.Stigmata)
						{
							// ����ü �߻�
							GameObject tear = Instantiate(x1Tear, transform.position + Vector3.right, transform.rotation);
							Rigidbody2D rigid = tear.GetComponent<Rigidbody2D>();
							rigid.AddForce(Vector3.right * fireRange, ForceMode2D.Impulse);
							rigid.AddForce(Vector3.up * 0.3f * fireRange, ForceMode2D.Impulse);

							// ����ü �߻�
							GameObject tear2 = Instantiate(x1Tear, transform.position + Vector3.right, transform.rotation);
							Rigidbody2D rigid2 = tear2.GetComponent<Rigidbody2D>();
							rigid2.AddForce(Vector3.right * fireRange, ForceMode2D.Impulse);

							// ����ü �߻�
							GameObject tear3 = Instantiate(x1Tear, transform.position + Vector3.right, transform.rotation);
							Rigidbody2D rigid3 = tear3.GetComponent<Rigidbody2D>();
							rigid3.AddForce(Vector3.right * fireRange, ForceMode2D.Impulse);
							rigid3.AddForce(Vector3.down * 0.3f * fireRange, ForceMode2D.Impulse);
						}
						// ������ �ϳ� �� ��
						else
						{
							// ����ü �߻�
							GameObject tear = Instantiate(x1Tear, transform.position + Vector3.right, transform.rotation);
							Rigidbody2D rigid = tear.GetComponent<Rigidbody2D>();
							rigid.AddForce(Vector3.right * fireRange, ForceMode2D.Impulse);
						}
					}
				}
			}
		}
	}

	// �Ӹ� ��Ʈ�� ��Ʈ ���� �Լ�
	public void Hit(int damage)
	{
		player.Hit(damage);
	}

	// ���� ��ȭ �Լ�
	void plusFace()
	{
		// Stigmata ȹ�� ��
		if ((player.haveItems[3] == Alter.Collection.Stigmata) && !stigmaActive)
		{
			// 1ȸ�� �� �� ���� ����
			stigmaActive = true;
			itemObjs[0].SetActive(true);
		}
		// BloodOfTheMartyr ȹ�� ��
		if ((player.haveItems[1] == Alter.Collection.BloodOfTheMartyr) && !crownActive)
		{
			// 1ȸ�� �� �� ���� ����
			crownActive = true;
			itemObjs[1].SetActive(true);
		}
		// TheSadOnion ȹ�� ��
		if ((player.haveItems[0] == Alter.Collection.TheSadOnion) && !onionActive)
		{
			// 1ȸ�� �� �� ���� ����
			onionActive = true;
			for (int i = 0; i < 6; i++)
			{
				Heades[i] = onionHeades[i];
			}
		}
		// Brimstone ȹ�� ��
		if ((player.haveItems[7] == Alter.Collection.Brimstone) && !brimstoneActive)
		{
			// 1ȸ�� �� �� ���� ����
			brimstoneActive = true;
			itemObjs[2].SetActive(true);

			// 1ȸ�� �� �� �Ǵ����� ���� ����
			x1Tear = x1Blood;
			x2Tear = x2Blood;
		}
	}
}
