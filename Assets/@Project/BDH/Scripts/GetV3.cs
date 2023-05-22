using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �÷��̾� ������ ��� ��ũ��Ʈ
public class GetV3 : MonoBehaviour
{
	// �Ӹ��� ����ȭ
	public GameObject p_Head;
	PlayerHead head;

	// ����ڵ� �ִϸ��̼�
	Animator h_anim;
	// �¿� �̹��� ������ ���� renderer
	SpriteRenderer h_renderer;

	// �¿� �̹��� ����, ���� ��ġ ������ ���� �ʵ�
	bool isFliped;
	bool heartDown;

	// �÷��̾� ��ü
	public GameObject playerObj;

	// Start is called before the first frame update
	void Start()
	{
		// �� ������Ʈ ��������
		h_anim = GetComponent<Animator>();
		h_renderer = GetComponent<SpriteRenderer>();
		head = p_Head.GetComponent<PlayerHead>();

		// �ʵ� �ʱⰪ
		isFliped = false;
		heartDown = false;
	}

	private void Update()
	{
		animationControl();
		heartPos();
		FlipX();
	}

	// ���� ���� ��ġ ����
	void heartPos()
	{
		// �¿� �̵� ��
		if (h_anim.GetBool("isHorizontal"))
		{
			// ���� ��ġ �������� �ʾ��� ��
			if (!heartDown)
			{
				// ���� ��ġ ����
				transform.position = playerObj.transform.position + new Vector3(0.02f, -0.13f, 0);

				transform.position = transform.position + Vector3.down * 0.1f;
				transform.position = transform.position + Vector3.right * 0.02f;
				heartDown = true;
			}
		}
		// �¿� �̵� ���� �ƴ� ��
		else
		{
			// ���� ��ġ �������� ��
			if (heartDown)
			{
				// ���� ��ġ ����ȭ
				transform.position = playerObj.transform.position + new Vector3(0.04f, -0.23f, 0);

				transform.position = transform.position + Vector3.up * 0.1f;
				transform.position = transform.position - Vector3.right * 0.02f;

				heartDown = false;
			}
		}
	}

	// ����ڵ�
	void animationControl()
	{
		// �ִϸ��̼� ����

		// ���� �̵��� ��
		if (head.vAxis != 0)
		{
			// �밢 �̵��� �ƴ� ��
			if (head.hAxis == 0)
			{
				// ���� �̵�
				if (head.vAxis > 0)
				{
					// �ĸ� �ִϸ��̼�
					h_anim.SetBool("isUp", true);
					h_anim.SetBool("isHorizontal", false);
				}
				// �Ʒ��� �̵�
				else if(head.vAxis < 0)
				{
					// ���� �ִϸ��̼�
					h_anim.SetBool("isUp", false);
					h_anim.SetBool("isHorizontal", false);
				}
			}
			// �밢 �̵��� ��
			else
			{
				// �� �� �̵� �ִϸ��̼�
				h_anim.SetBool("isUp", false);
				h_anim.SetBool("isHorizontal", true);
			}
		}
		// �¿� �̵��� ��
		else if (head.hAxis != 0)
		{
			// �� �� �̵� �ִϸ��̼�
			h_anim.SetBool("isUp", false);
			h_anim.SetBool("isHorizontal", true);
		}
		// Idle ������ ��
		else
		{
			// ���� �� �ִϸ��̼� ����
			h_anim.SetBool("isUp", false);
			h_anim.SetBool("isHorizontal", false);
		}
	}

	// ��������Ʈ �¿� �ø� �Լ�
	void FlipX()
	{
		// �� �� �̵� ��ư�� ������ ��
		if (Input.GetButton("Horizontal"))
		{
			// ������ ���� ���� ���� ��
			if (!isFliped && head.hAxis == -1 && head.hAxis != 0)
			{
				// ��������Ʈ �ø�
				h_renderer.flipX = true;
				isFliped = true;
			}
			// �������� ���� ���� ���� ��
			else if (isFliped && head.hAxis == 1 && head.hAxis != 0)
			{
				// ��������Ʈ �ø�
				h_renderer.flipX = false;
				isFliped = false;
			}
		}
	}
}
