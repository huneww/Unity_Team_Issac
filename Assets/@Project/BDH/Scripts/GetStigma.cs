using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// �÷��̾� ������ ��� ��ũ��Ʈ
public class GetStigma : MonoBehaviour
{
	// ��3�� �� ��������Ʈ
    public Sprite[] Eyes;

	// renderer
	SpriteRenderer e_renderer;

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

	// �� ��ġ ����
	bool eyeDown;

	private void Start()
	{
		// �� ������Ʈ ��������
		e_renderer = GetComponent<SpriteRenderer>();
		head = transform.parent.GetComponent<PlayerHead>();
		player = playerObj.GetComponent<Player>();
	}

	void Update()
    {
		idleEye();
		shotEye();
		eyePos();
	}

	// �̸��� �� ���� ��ġ ����
	void eyePos()
	{
		// �� ��ġ �������� �ʾ��� ��
		if (!eyeDown)
		{
			// �¿� �Ӹ��� ��
			if (e_renderer.sprite == Eyes[2] || e_renderer.sprite == Eyes[3] || e_renderer.sprite == Eyes[4] || e_renderer.sprite == Eyes[5])
			{
				// �� ��ġ ����
				transform.position = transform.position + Vector3.down * 0.05f;
				eyeDown = true;
			}
		}
		// �� ��ġ �������� ��
		else
		{
			// �¿� �Ӹ��� �ƴ� ��
			if (e_renderer.sprite == Eyes[0] || e_renderer.sprite == Eyes[1] || e_renderer.sprite == Eyes[6])
			{
				// �� ��ġ ����ȭ
				eyeDown = false;
				transform.position = transform.position + Vector3.up * 0.05f;
			}
		}
	}

	// �� 3�� �� ��Ų ����
	void idleEye()
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
						// �Ʒ� �̵�
						if (head.vAxis == -1)
						{
							// ���� �Ӹ�
							e_renderer.sprite = Eyes[0];
						}
						// �� �̵�
						else
						{
							// �ĸ� �Ӹ�
							e_renderer.sprite = Eyes[6];
						}
					}
					// �밢 �̵��� ��
					// ������ �̵�
					else if (head.hAxis == 1)
					{
						// ������ �Ӹ�
						e_renderer.sprite = Eyes[2];
					}
					// ���� �̵�
					else if (head.hAxis == -1)
					{
						// ���� �Ӹ�
						e_renderer.sprite = Eyes[4];
					}
				}
				// �¿� �̵��� ��
				// ������ �̵�
				else if (head.hAxis == 1)
				{
					// ������ �Ӹ�
					e_renderer.sprite = Eyes[2];
				}
				// ���� �̵�
				else if (head.hAxis == -1)
				{
					// ���� �Ӹ�
					e_renderer.sprite = Eyes[4];
				}
				// Idle ������ ��
				else
				{
					// ���� �Ӹ�
					e_renderer.sprite = Eyes[0];
				}
			}
		}
		// ���� ���� ��
		else if (Input.GetButton("FireVertical") && !Input.GetButton("FireHorizontal"))
		{
			// ���� �Ӹ� ������
			if (headDelay < 0)
			{
				// ���� ������ ��
				if (head.vFire == 1)
				{
					// �ĸ� �Ӹ�
					e_renderer.sprite = Eyes[6];
				}
				// �Ʒ��� ������ ��
				else if (head.vFire == -1)
				{
					// ���� �Ӹ�
					e_renderer.sprite = Eyes[0];
				}
			}
		}
		// ���� ���� ��
		else if (!Input.GetButton("FireVertical") && Input.GetButton("FireHorizontal"))
		{
			// ���� �Ӹ� ������
			if (headDelay < 0)
			{
				// �¿� ������ ��
				// ������ �Ӹ�
				if (head.hFire == 1)
				{
					// ������ �Ӹ�
					e_renderer.sprite = Eyes[2];
				}
				// ���� ������ ��
				if (head.hFire == -1)
				{
					// ���� �Ӹ�
					e_renderer.sprite = Eyes[4];
				}
			}
		}
	}

	// ���� ���� �� �� 3�� �� ��Ų ����
	void shotEye()
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
				// �Ʒ��� ������ ��
				if (head.vFire < 0)
				{
					// ���� �Ӹ�
					e_renderer.sprite = Eyes[1];

					// ��������Ʈ ���� ������
					headDelay = 0.1f;
					// ���� ������ �ʱ�ȭ
					fireDelay = maxDelay;
				}
				// ���� ������ ��
				else if (head.vFire > 0)
				{
					// �ĸ� �Ӹ�
					e_renderer.sprite = Eyes[6];

					// ��������Ʈ ���� ������
					headDelay = 0.1f;
					// ���� ������ �ʱ�ȭ
					fireDelay = maxDelay;
				}
			}
			// �¿� ����
			else if (Input.GetButton("FireHorizontal"))
			{
				// ������ ������ ��
				if (head.hFire > 0)
				{
					// ������ �Ӹ�
					e_renderer.sprite = Eyes[3];

					// ��������Ʈ ���� ������
					headDelay = 0.1f;
					// ���� ������ �ʱ�ȭ
					fireDelay = maxDelay;
				}
				// ���� ������ ��
				else if (head.hFire < 0)
				{
					// ���� �Ӹ�
					e_renderer.sprite = Eyes[5];

					// ��������Ʈ ���� ������
					headDelay = 0.1f;
					// ���� ������ �ʱ�ȭ
					fireDelay = maxDelay;
				}
			}
		}
	}
}
