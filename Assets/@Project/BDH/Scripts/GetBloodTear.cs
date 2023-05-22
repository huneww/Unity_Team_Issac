using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �÷��̾� ������ ��� ��ũ��Ʈ
public class GetBloodTear : MonoBehaviour
{
	// ���� ���� ��������Ʈ
	public Sprite[] Bloods;

	// renderer
	SpriteRenderer b_renderer;

	// �Ӹ��� ����ȭ
	public GameObject p_Head;
	PlayerHead head;

	// �÷��̾� ��ü
	public GameObject playerObj;
	Player player;

	// ���� ������
	float fireDelay;
	float maxDelay;
	float headDelay;

	// Start is called before the first frame update
	void Start()
	{
		// �� ������Ʈ ��������
		b_renderer = GetComponent<SpriteRenderer>();
		head = transform.parent.GetComponent<PlayerHead>();
		player = playerObj.GetComponent<Player>();
	}

	void Update()
	{
		idleBlood();
		shotBlood();
	}

	// �Ǵ��� ��Ų ����
	void idleBlood()
	{
		if (!Input.GetButton("FireHorizontal") && !Input.GetButton("FireVertical"))
		{
			// ���� �Ӹ� ������
			if (headDelay < 0)
			{
				if (head.vAxis != 0)
				{
					// �밢 �̵��� �ƴ� ��
					if (head.hAxis == 0)
					{
						if (head.vAxis == -1)
						{
							// ���� �Ӹ�
							b_renderer.sprite = Bloods[0];
						}
						else
						{
							// �ĸ� �Ӹ�
							b_renderer.sprite = Bloods[2];
						}
					}
					// �밢 �̵��� ��
					else if (head.hAxis == 1)
					{
						// ������ �Ӹ�
						b_renderer.sprite = Bloods[4];
					}
					else if (head.hAxis == -1)
					{
						// ���� �Ӹ�
						b_renderer.sprite = Bloods[6];
					}
				}
				// �¿� �̵��� ��
				else if (head.hAxis == 1)
				{
					// ������ �Ӹ�
					b_renderer.sprite = Bloods[4];
				}
				else if (head.hAxis == -1)
				{
					// ���� �Ӹ�
					b_renderer.sprite = Bloods[6];
				}
				// Idle ������ ��
				else
				{
					// ���� �Ӹ�
					b_renderer.sprite = Bloods[0];
				}
			}
		}
		else if (Input.GetButton("FireVertical") && !Input.GetButton("FireHorizontal"))
		{
			// ���� �Ӹ� ������
			if (headDelay < 0)
			{
				// ���ڸ����� ���� ������ ��
				if (head.vFire == 1)
				{
					// �ĸ� �Ӹ�
					b_renderer.sprite = Bloods[2];
				}
				// ���ڸ����� �Ʒ��� ������ ��
				else if (head.vFire == -1)
				{
					// ���� �Ӹ�
					b_renderer.sprite = Bloods[0];
				}
			}
		}
		else if (!Input.GetButton("FireVertical") && Input.GetButton("FireHorizontal"))
		{
			// ���� �Ӹ� ������
			if (headDelay < 0)
			{
				// ���ڸ����� �¿� ������ ��
				if (head.hFire == 1)
				{
					// �� �Ӹ�
					b_renderer.sprite = Bloods[4];
				}
				// ���ڸ����� �¿� ������ ��
				if (head.hFire == -1)
				{
					// �� �Ӹ�
					b_renderer.sprite = Bloods[6];
				}
			}
		}
	}

	// ���� ���� �� �Ǵ��� ��Ų ����
	void shotBlood()
	{
		// ���� ������ ��ü���� ��������
		maxDelay = player.curTears;

		// ���� ������ ����
		fireDelay -= Time.deltaTime;
		// �Ӹ� ������ ����
		headDelay -= Time.deltaTime;

		// ���� �����̰� 0�� ��
		if (fireDelay < 0)
		{
			// ���� ������ ��
			if (Input.GetButton("FireVertical"))
			{
				// �Ʒ��� ������ ��
				if (head.vFire < 0)
				{
					// ���� �Ӹ�
					b_renderer.sprite = Bloods[1];

					// ��������Ʈ ���� ������
					headDelay = 0.1f;
					// ���� ������ �ʱ�ȭ
					fireDelay = maxDelay;
				}
				// ���� ������ ��
				else if (head.vFire > 0)
				{
					// �ĸ� �Ӹ�
					b_renderer.sprite = Bloods[3];

					// ��������Ʈ ���� ������
					headDelay = 0.1f;
					// ���� ������ �ʱ�ȭ
					fireDelay = maxDelay;
				}
			}
			// �¿� ������ ��
			else if (Input.GetButton("FireHorizontal"))
			{
				// ���� ����
				if (head.hFire > 0)
				{
					// ���� �Ӹ�
					b_renderer.sprite = Bloods[5];

					// ��������Ʈ ���� ������
					headDelay = 0.1f;
					// ���� ������ �ʱ�ȭ
					fireDelay = maxDelay;
				}
				// ���� ����
				else if (head.hFire < 0)
				{
					// ���� �Ӹ�
					b_renderer.sprite = Bloods[7];

					// ��������Ʈ ���� ������
					headDelay = 0.1f;
					// ���� ������ �ʱ�ȭ
					fireDelay = maxDelay;
				}
			}
		}
	}
}
