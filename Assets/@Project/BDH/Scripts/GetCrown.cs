using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �÷��̾� ������ ��� ��ũ��Ʈ
public class GetCrown : MonoBehaviour
{
	// ���ÿհ� ��������Ʈ
	public Sprite[] Crowns;

	// renderer
	SpriteRenderer c_renderer;

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
		c_renderer = GetComponent<SpriteRenderer>();
		head = transform.parent.GetComponent<PlayerHead>();
		player = playerObj.GetComponent<Player>();
	}

	void Update()
	{
		idleCorwn();
		shotCorwn();
	}

	// ���ÿհ� ��Ų ����
	void idleCorwn()
	{
		if (!Input.GetButton("FireHorizontal") && !Input.GetButton("FireVertical"))
		{
			// ���� �Ӹ� ������
			if (headDelay < 0)
			{
				// ���Ʒ� ������ ��
				if (head.vAxis != 0)
				{
					// �밢 �̵��� �ƴ� ��
					if (head.hAxis == 0)
					{
						if (head.vAxis != 0)
						{
							// �� �Ʒ� �Ӹ�
							c_renderer.sprite = Crowns[0];
						}
					}
					// �밢 �̵��� ��
					else
					{
						// �� �Ӹ�
						c_renderer.sprite = Crowns[2];
					}
				}
				// �¿� �̵��� ��
				else if (head.hAxis != 0)
				{
					// �� �Ӹ�
					c_renderer.sprite = Crowns[2];
				}
				// Idle ������ ��
				else
				{
					// �� �Ʒ� �Ӹ�
					c_renderer.sprite = Crowns[0];
				}
			}
		}
		// ���� ���� ��
		else if (Input.GetButton("FireVertical") && !Input.GetButton("FireHorizontal"))
		{
			// ���� �Ӹ� ������
			if (headDelay < 0)
			{
				// ���ڸ����� ���� ������ ��
				if (head.vFire != 0)
				{
					// �� �Ʒ� �Ӹ�
					c_renderer.sprite = Crowns[0];
				}
			}
		}
		// ���� ���� ��
		else if (!Input.GetButton("FireVertical") && Input.GetButton("FireHorizontal"))
		{
			// ���� �Ӹ� ������
			if (headDelay < 0)
			{
				// ���ڸ����� �¿� ������ ��
				if (head.hFire != 0)
				{
					// �� �Ӹ�
					c_renderer.sprite = Crowns[2];
				}
			}
		}
	}

	// ���� ���� �� ���ÿհ� ��Ų ����
	void shotCorwn()
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
			if (Input.GetButton("FireVertical"))
			{
				// �� �Ʒ� ������ ��
				if (head.vFire != 0)
				{
					// �� �Ʒ� �Ӹ�
					c_renderer.sprite = Crowns[1];

					// ��������Ʈ ���� ������
					headDelay = 0.1f;
					// ���� ������ �ʱ�ȭ
					fireDelay = maxDelay;
				}
			}
			else if (Input.GetButton("FireHorizontal"))
			{
				// �¿� ������ ��
				if (head.hFire != 0)
				{
					// �¿� �Ӹ�
					c_renderer.sprite = Crowns[3];

					// ��������Ʈ ���� ������
					headDelay = 0.1f;
					// ���� ������ �ʱ�ȭ
					fireDelay = maxDelay;
				}
			}
		}
	}
}
