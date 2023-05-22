using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
	// �÷��̾� ������Ʈ�� �����ϱ� ���� ���� ������Ʈ
	public GameObject parentObj;

    // �÷��̾� �̵� �ӵ�
    public float speed;

	// �÷��̾� �̵� ���� int �ʵ�
	float hAxis;
	float vAxis;

	// �÷��̾� ��ź ���� int �ʵ�
	float hBomb;
	float vBomb;

	// �÷��̾��� �̵� ���� ��ǥ
	Vector3 moveVec;

	// ��������Ʈ �ø��� ���� bool �ʵ�
	bool isFliped;

	// ��ź ��� bool�ʵ�
	bool bDown;
	// ��ź ������
	public GameObject Bomb;

	// ��Ƽ�� ������ ��� bool �ʵ�
	bool iDown;
	// ������ ��ٿ�
	public float i_coolDown;
	// ������ ���� ��ٿ� ����
	public float maxCool;
	// ���� ������ �ִ� ��Ƽ�� ������
	public Alter.Collection activeItem;

	// �ִ� �� ü��
	public int maxHeart;
	// ���� �� ü��
	public int curHeart;
	// �ִ� ü��
	public int maxRealHeart;
	// ���� ü��
	public int curRealHeart;
	// ���� ü��
	public int curSoulHeart;

	// �Ŀ�
	public float curPower;
	// �߻� �ӵ�
	public float curTears;
	// �߻� ������
	public float fireRange;

	// �ǰ� �� ����
	bool nowHit;

	// �÷��̾� ���� �Ҹ� �����۵�
	public int Bombs;
	public int Keys;
	public int Coins;

	// �˾� ȹ�� �̹���
	public GameObject[] pillsImages;
	// ������ ȹ�� �̹���
	public GameObject[] itmeImages;

	// ������ ���� ��Ȳ
	public Alter.Collection[] haveItems;

	// ���� ��ȭ ������ ���� bool����
	bool oneUpActive;
	bool brimstoneActive;
	bool LLLActive;

	// ���� ������Ʈ
	public GameObject[] itemObj;

	// �������� �̵�
	bool nextStage;
	// �̵� ��ǥ �� collider
	Collider2D nextCol;
	// �������� �ִϸ��̼� ���� ������
	float d_time = 0;
	// 1up ������ �ߵ��� ���� �ִϸ��̼� ������
	float a_time = 0;

	// ���� ��������
	public int nowStage;

	// �÷��̾� ������Ʈ �ִϸ�����
	Animator p_anim;
	// �÷��̾� ������Ʈ ��������Ʈ ������
	SpriteRenderer p_renderer;
	// �پ��� ��� ��½� ����� �Ӹ� ������Ʈ
	public GameObject p_head;
	// ��� ������
	public GameObject p_die;
	// �÷��̾� UI ������
	public GameObject p_UI;

	// ����ü ������Ʈ
	public GameObject x1Tear;
	public GameObject x2Tear;
	// ���� ����ü ������Ʈ
	public GameObject x1Blood;
	public GameObject x2Blood;

	// Ray ������ ���� ������ Vec
	Vector3 upOffset;
	Vector3 downOffset;
	Vector3 leftOffset;
	Vector3 rightOffset;

	// ����ĳ��Ʈ
	RaycastHit2D rayUp;
	RaycastHit2D rayDown;
	RaycastHit2D rayLeft;
	RaycastHit2D rayRight;

	private void Start()
	{
		// �� ������Ʈ ��������
		p_anim = GetComponent<Animator>();
		p_renderer = GetComponent<SpriteRenderer>();

		// �¿� ��������Ʈ �⺻��
		p_renderer.flipX = false;

		// �������� ��� �ִϸ��̼� �⺻ ������ ��
		d_time = 0;
	}

	private void Update()
	{
		GetInput();
		RayShot();
		FlipX();
		Walk();
		UseBomb();
		heartCheck();
		useActiveItem();
		nowActive();
		plusThings();
	}

	// ��ư �Է� ���� �Լ�
	void GetInput()
	{
		if (!nextStage)
		{
			// �̵� ���� ��ư ����
			hAxis = Input.GetAxisRaw("Horizontal");
			vAxis = Input.GetAxisRaw("Vertical");
		}
		
		// ��ź ��� ��ư ����
		hBomb = Input.GetAxisRaw("FireHorizontal");
		vBomb = Input.GetAxisRaw("FireVertical");

		// ��ź ���
		bDown = Input.GetButtonDown("UseBoom");
		// ��Ƽ�� ������ ���
		iDown = Input.GetButtonDown("UseItem");
	}

	// ����ĳ��Ʈ �ߵ� �Լ�
	void RayShot()
	{
		// ray ������
		upOffset = transform.position + Vector3.left * 0.1f + Vector3.up * 0.2f;
		downOffset = transform.position + Vector3.left * 0.1f + Vector3.down * 0.35f;
		leftOffset = transform.position + Vector3.left * 0.2f + Vector3.up * 0.15f;
		rightOffset = transform.position + Vector3.right * 0.2f + Vector3.up * 0.15f;

		// ����ĳ��Ʈ
		rayUp = Physics2D.Raycast(upOffset, Vector3.right, 0.2f, LayerMask.GetMask("Block"));
		Debug.DrawRay(upOffset, Vector3.right * 0.2f, Color.green);
		rayDown = Physics2D.Raycast(downOffset, Vector3.right, 0.2f, LayerMask.GetMask("Block"));
		Debug.DrawRay(downOffset, Vector3.right * 0.2f, Color.green);
		rayLeft = Physics2D.Raycast(leftOffset, Vector3.down, 0.45f, LayerMask.GetMask("Block"));
		Debug.DrawRay(leftOffset, Vector3.down * 0.45f, Color.green);
		rayRight = Physics2D.Raycast(rightOffset, Vector3.down, 0.45f, LayerMask.GetMask("Block"));
		Debug.DrawRay(rightOffset, Vector3.down * 0.45f, Color.green);
	}

	// �̵� �Լ�
	void Walk()
	{
		// �����̸� �Ǻ��ϱ� ���� ����ĳ��Ʈ
		RaycastHit2D pitUp = Physics2D.Raycast(upOffset, Vector3.right, 0.2f, LayerMask.GetMask("Pit"));
		RaycastHit2D pitDown = Physics2D.Raycast(downOffset, Vector3.right, 0.2f, LayerMask.GetMask("Pit"));
		RaycastHit2D pitLeft = Physics2D.Raycast(leftOffset, Vector3.down, 0.45f, LayerMask.GetMask("Pit"));
		RaycastHit2D pitRight = Physics2D.Raycast(rightOffset, Vector3.down, 0.45f, LayerMask.GetMask("Pit"));

		// ���� ��, �����̿� ����� ��
		if ((rayUp || pitUp) && vAxis == 1)
		{
			// �� �����ڸ����� �� ������ ������ �ʵ��� ���� ó��
			if ((rayLeft || pitLeft) && hAxis == -1)
			{
				// �̵������� Vector3�� �Է��� �� �׻� ������ ���� �־������� .normalize�ο�(�밢 �̵��� ����)
				moveVec = new Vector3(0, 0, 0).normalized;
			}
			else if ((rayRight || pitRight) && hAxis == 1)
			{
				// �̵������� Vector3�� �Է��� �� �׻� ������ ���� �־������� .normalize�ο�(�밢 �̵��� ����)
				moveVec = new Vector3(0, 0, 0).normalized;
			}
			else
			{
				// �̵������� Vector3�� �Է��� �� �׻� ������ ���� �־������� .normalize�ο�(�밢 �̵��� ����)
				moveVec = new Vector3(hAxis, 0, 0).normalized;
			}
		}
		// �Ʒ��� ��, �����̿� ����� ��
		else if ((rayDown || pitDown) && vAxis == -1)
		{
			// �� �����ڸ����� �� ������ ������ �ʵ��� ���� ó��
			if ((rayLeft || pitLeft) && hAxis == -1)
			{
				// �̵������� Vector3�� �Է��� �� �׻� ������ ���� �־������� .normalize�ο�(�밢 �̵��� ����)
				moveVec = new Vector3(0, 0, 0).normalized;
			}
			else if ((rayRight || pitRight) && hAxis == 1)
			{
				// �̵������� Vector3�� �Է��� �� �׻� ������ ���� �־������� .normalize�ο�(�밢 �̵��� ����)
				moveVec = new Vector3(0, 0, 0).normalized;
			}
			else
			{
				// �̵������� Vector3�� �Է��� �� �׻� ������ ���� �־������� .normalize�ο�(�밢 �̵��� ����)
				moveVec = new Vector3(hAxis, 0, 0).normalized;
			}
		}
		// ���� ��, �����̿� ����� ��
		else if ((rayLeft || pitLeft) && hAxis == -1)
		{
			// �̵������� Vector3�� �Է��� �� �׻� ������ ���� �־������� .normalize�ο�(�밢 �̵��� ����)
			moveVec = new Vector3(0, vAxis, 0).normalized;
		}
		// ������ ��, �����̿� ����� ��
		else if ((rayRight || pitRight) && hAxis == 1)
		{
			// �̵������� Vector3�� �Է��� �� �׻� ������ ���� �־������� .normalize�ο�(�밢 �̵��� ����)
			moveVec = new Vector3(0, vAxis, 0).normalized;
		}
		// ���ع��� ���� ��
		else
		{
			// �̵������� Vector3�� �Է��� �� �׻� ������ ���� �־������� .normalize�ο�(�밢 �̵��� ����)
			moveVec = new Vector3(hAxis, vAxis, 0).normalized;
		}

		// ���������� �Ѿ�� ���� �ƴ� ��
		if (!nextStage)
		{
			// �̵� ����
			transform.position += moveVec * speed * Time.deltaTime;
		}

		// �ִϸ��̼� ����
		// ���� �̵��� ��
		if (vAxis != 0)
		{
			// �밢 �̵��� �ƴ� ��
			if (hAxis == 0)
			{
				// �� �Ʒ� �̵� �ִϸ��̼�
				p_anim.SetBool("isHorizontal", false);
				p_anim.SetBool("isVertical", true);
			}
			// �밢 �̵��� ��
			else
			{
				// �� �� �̵� �ִϸ��̼�
				p_anim.SetBool("isVertical", false);
				p_anim.SetBool("isHorizontal", true);
			}
		}
		// �¿� �̵��� ��
		else if (hAxis != 0)
		{
			// �� �� �̵� �ִϸ��̼�
			p_anim.SetBool("isHorizontal", true);
		}
		// Idle ������ ��
		else
		{
			// �̵� �ִϸ��̼� ����
			p_anim.SetBool("isVertical", false);
			p_anim.SetBool("isHorizontal", false);
		}
	}

	// ��������Ʈ �¿� �ø� �Լ�
	void FlipX()
	{
		// �� �� �̵� ��ư�� ������ ��
		if (Input.GetButton("Horizontal"))
		{
			// ������ ���� ���� ���� ��
			if (!isFliped && hAxis == -1 && hAxis != 0)
			{
				// ��������Ʈ �ø�
				p_renderer.flipX = true;
				isFliped = true;
			}
			// �������� ���� ���� ���� ��
			else if (isFliped && hAxis == 1 && hAxis != 0)
			{
				// ��������Ʈ �ø�
				p_renderer.flipX = false;
				isFliped = false;
			}
		}
	}

	// ��ź ���
	void UseBomb()
	{
		// ��ź�� �ְ� ��� ��ư�� ������ ���
		if (Bombs > 0 && bDown)
		{
			// ���� ���� ������� ��
			if (rayLeft)
			{
				// ��ź ���� ����
				GameObject bomb = Instantiate(Bomb, transform.position + Vector3.right * 0.5f, transform.localRotation);
				Bomb bombTrigger = bomb.GetComponent<Bomb>();
				// ��ź ������ ����
				bombTrigger.setDelay(1.5f);
				// ��ź �Ҹ�
				Bombs--;
			}
			// ���� ���� ������� ��
			else if (rayRight)
			{
				// ��ź ���� ����
				GameObject bomb = Instantiate(Bomb, transform.position + Vector3.left * 0.5f, transform.localRotation);
				Bomb bombTrigger = bomb.GetComponent<Bomb>();
				// ��ź ������ ����
				bombTrigger.setDelay(1.5f);
				// ��ź �Ҹ�
				Bombs--;
			}
			// �� ���� ������� ��
			else if (rayUp)
			{
				// ��ź �Ʒ� ����
				GameObject bomb = Instantiate(Bomb, transform.position + Vector3.down * 0.5f, transform.localRotation);
				Bomb bombTrigger = bomb.GetComponent<Bomb>();
				// ��ź ������ ����
				bombTrigger.setDelay(1.5f);
				// ��ź �Ҹ�
				Bombs--;
			}
			// �Ʒ� ���� ������� ��
			else if (rayDown)
			{
				// ��ź �� ����
				GameObject bomb = Instantiate(Bomb, transform.position + Vector3.up * 0.5f, transform.localRotation);
				Bomb bombTrigger = bomb.GetComponent<Bomb>();
				// ��ź ������ ����
				bombTrigger.setDelay(1.5f);
				// ��ź �Ҹ�
				Bombs--;
			}
			// �� ��ư�� ������ ���� ���
			else if (hBomb != 0 || vBomb != 0)
			{
				// ����
				if (hBomb < 0 && vBomb == 0)
				{
					// ��ź ���� ����
					GameObject bomb = Instantiate(Bomb, transform.position + Vector3.left * 0.5f, transform.localRotation);
					Bomb bombTrigger = bomb.GetComponent<Bomb>();
					// ��ź ������ ����
					bombTrigger.setDelay(1.5f);
					// ��ź �Ҹ�
					Bombs--;
				}
				// ������
				else if (hBomb > 0 && vBomb == 0)
				{
					// ��ź ���� ����
					GameObject bomb = Instantiate(Bomb, transform.position + Vector3.right * 0.5f, transform.localRotation);
					Bomb bombTrigger = bomb.GetComponent<Bomb>();
					// ��ź ������ ����
					bombTrigger.setDelay(1.5f);
					// ��ź �Ҹ�
					Bombs--;
				}
				// �Ʒ���
				else if (hBomb == 0 && vBomb < 0)
				{
					// ��ź �Ʒ� ����
					GameObject bomb = Instantiate(Bomb, transform.position + Vector3.down * 0.5f, transform.localRotation);
					Bomb bombTrigger = bomb.GetComponent<Bomb>();
					// ��ź ������ ����
					bombTrigger.setDelay(1.5f);
					// ��ź �Ҹ�
					Bombs--;
				}
				// ����
				else if (hBomb == 0 && vBomb > 0)
				{
					// ��ź �� ����
					GameObject bomb = Instantiate(Bomb, transform.position + Vector3.up * 0.5f, transform.localRotation);
					Bomb bombTrigger = bomb.GetComponent<Bomb>();
					// ��ź ������ ����
					bombTrigger.setDelay(1.5f);
					// ��ź �Ҹ�
					Bombs--;
				}
			}
			// �ƹ���ư�� ������ �ʰ� ������ �ȴ�� ���� ��
			else
			{
				// ��ź �Ʒ� ����
				GameObject bomb = Instantiate(Bomb, transform.position + Vector3.down * 0.5f, transform.localRotation);
				Bomb bombTrigger = bomb.GetComponent<Bomb>();
				// ��ź ������ ����
				bombTrigger.setDelay(1.5f);
				// ��ź �Ҹ�
				Bombs--;
			}
		}
	}

	// ü���� Ȯ���ϰ� ���ܸ� ���ִ� �Լ�
	void heartCheck()
	{
		// �ִ� ü�º��� ����ü���� ���� �� ����
		if (maxRealHeart < curRealHeart)
		{
			curRealHeart = maxRealHeart;
		}
	}

	// ���� ������ �ִ� ��Ƽ�� ������
	void nowActive()
	{
		// �����۵��� �˻��� �� ��Ƽ�� ������ ĭ�� ����
		if (haveItems[5] != Alter.Collection.None)
		{
			activeItem = Alter.Collection.NineVolt;
		}
	}

	// ��Ƽ�� ������ ������� ��
	void useActiveItem()
	{
		// ��Ƽ�� ������ ��Ÿ�� ����
		i_coolDown -= Time.deltaTime;

		// ��Ƽ�� ������ ��Ÿ���� 0���� �� ��
		if (i_coolDown < 0 && iDown)
		{
			// ��Ƽ�� ������ ������ ����
			switch(activeItem)
			{
				// ������ �ߵ�
				case Alter.Collection.NineVolt:
					useNineVolt();
					break;
			}

			// ��Ƽ�� ������ ��Ÿ�� Ȱ��ȭ
			i_coolDown = maxCool;
		}
	}

	// ���� ��Ʈ ������ ���
	void useNineVolt()
	{
		// ������ ��� �� �������� �ִϸ��̼� ����
		StartCoroutine(scaleEff());

		// ������ ȹ�� �ִϸ��̼� ���
		p_anim.SetBool("isGet", true);
		// �Ӹ� ��Ȱ��ȭ
		p_head.SetActive(false);
		transform.position = transform.position + Vector3.up * 0.1f;

		// ���� ��Ʈ ������ ���
		itmeImages[5].SetActive(true);
		itmeImages[5].transform.position = Vector3.Lerp(pillsImages[5].transform.position, pillsImages[5].transform.position + Vector3.up * 0.4f, 1f);

		Invoke("UseItemOut", 1);

		// ���� ���� �� ��
		if (haveItems[0] == Alter.Collection.TheSadOnion)
		{
			// �� ���� ���� �߻�
			// 12
			{
				// ����ü �߻�
				GameObject tear = Instantiate(x2Tear, transform.position + Vector3.up, transform.rotation);
				Rigidbody2D rigid = tear.GetComponent<Rigidbody2D>();
				rigid.AddForce(Vector3.up * fireRange, ForceMode2D.Impulse);
			}

			// 1
			{
				// ����ü �߻�
				GameObject tear = Instantiate(x2Tear, transform.position + new Vector3(0.5f, 1, 0), transform.rotation);
				Rigidbody2D rigid = tear.GetComponent<Rigidbody2D>();
				rigid.AddForce(new Vector3(0.5f, 1, 0) * fireRange, ForceMode2D.Impulse);
			}

			// 3
			{
				// ����ü �߻�
				GameObject tear = Instantiate(x2Tear, transform.position + Vector3.right, transform.rotation);
				Rigidbody2D rigid = tear.GetComponent<Rigidbody2D>();
				rigid.AddForce(Vector3.right * fireRange, ForceMode2D.Impulse);
			}

			// 5
			{
				// ����ü �߻�
				GameObject tear = Instantiate(x2Tear, transform.position + new Vector3(0.5f, -1, 0), transform.rotation);
				Rigidbody2D rigid = tear.GetComponent<Rigidbody2D>();
				rigid.AddForce(new Vector3(0.5f, -1, 0) * fireRange, ForceMode2D.Impulse);
			}

			// 6
			{
				// ����ü �߻�
				GameObject tear = Instantiate(x2Tear, transform.position + Vector3.down, transform.rotation);
				Rigidbody2D rigid = tear.GetComponent<Rigidbody2D>();
				rigid.AddForce(Vector3.down * fireRange, ForceMode2D.Impulse);
			}

			// 7
			{
				// ����ü �߻�
				GameObject tear = Instantiate(x2Tear, transform.position + new Vector3(-0.5f, -1, 0), transform.rotation);
				Rigidbody2D rigid = tear.GetComponent<Rigidbody2D>();
				rigid.AddForce(new Vector3(-0.5f, -1, 0) * fireRange, ForceMode2D.Impulse);
			}

			// 9
			{
				// ����ü �߻�
				GameObject tear = Instantiate(x2Tear, transform.position + Vector3.left, transform.rotation);
				Rigidbody2D rigid = tear.GetComponent<Rigidbody2D>();
				rigid.AddForce(Vector3.left * fireRange, ForceMode2D.Impulse);
			}

			// 11
			{
				// ����ü �߻�
				GameObject tear = Instantiate(x2Tear, transform.position + new Vector3(-0.5f, 1, 0), transform.rotation);
				Rigidbody2D rigid = tear.GetComponent<Rigidbody2D>();
				rigid.AddForce(new Vector3(-0.5f, 1, 0) * fireRange, ForceMode2D.Impulse);
			}
		}
		// ū ������ ��
		else if (haveItems[0] == Alter.Collection.None)
		{
			// ������ ���� �߻�
			// 12
			{
				// ����ü �߻�
				GameObject tear = Instantiate(x1Tear, transform.position + Vector3.up, transform.rotation);
				Rigidbody2D rigid = tear.GetComponent<Rigidbody2D>();
				rigid.AddForce(Vector3.up * fireRange, ForceMode2D.Impulse);
			}

			// 1
			{
				// ����ü �߻�
				GameObject tear = Instantiate(x1Tear, transform.position + new Vector3(0.5f, 1, 0), transform.rotation);
				Rigidbody2D rigid = tear.GetComponent<Rigidbody2D>();
				rigid.AddForce(new Vector3(0.5f, 1, 0) * fireRange, ForceMode2D.Impulse);
			}

			// 3
			{
				// ����ü �߻�
				GameObject tear = Instantiate(x1Tear, transform.position + Vector3.right, transform.rotation);
				Rigidbody2D rigid = tear.GetComponent<Rigidbody2D>();
				rigid.AddForce(Vector3.right * fireRange, ForceMode2D.Impulse);
			}

			// 5
			{
				// ����ü �߻�
				GameObject tear = Instantiate(x1Tear, transform.position + new Vector3(0.5f, -1, 0), transform.rotation);
				Rigidbody2D rigid = tear.GetComponent<Rigidbody2D>();
				rigid.AddForce(new Vector3(0.5f, -1, 0) * fireRange, ForceMode2D.Impulse);
			}

			// 6
			{
				// ����ü �߻�
				GameObject tear = Instantiate(x1Tear, transform.position + Vector3.down, transform.rotation);
				Rigidbody2D rigid = tear.GetComponent<Rigidbody2D>();
				rigid.AddForce(Vector3.down * fireRange, ForceMode2D.Impulse);
			}

			// 7
			{
				// ����ü �߻�
				GameObject tear = Instantiate(x1Tear, transform.position + new Vector3(-0.5f, -1, 0), transform.rotation);
				Rigidbody2D rigid = tear.GetComponent<Rigidbody2D>();
				rigid.AddForce(new Vector3(-0.5f, -1, 0) * fireRange, ForceMode2D.Impulse);
			}

			// 9
			{
				// ����ü �߻�
				GameObject tear = Instantiate(x1Tear, transform.position + Vector3.left, transform.rotation);
				Rigidbody2D rigid = tear.GetComponent<Rigidbody2D>();
				rigid.AddForce(Vector3.left * fireRange, ForceMode2D.Impulse);
			}

			// 11
			{
				// ����ü �߻�
				GameObject tear = Instantiate(x1Tear, transform.position + new Vector3(-0.5f, 1, 0), transform.rotation);
				Rigidbody2D rigid = tear.GetComponent<Rigidbody2D>();
				rigid.AddForce(new Vector3(-0.5f, 1, 0) * fireRange, ForceMode2D.Impulse);
			}
		}
	}

	// ���� ��ȭ �˻� �Լ�
	void plusThings()
	{
		// 1up ȹ�� ��
		if ((haveItems[4] == Alter.Collection.OneUp) && !oneUpActive)
		{
			// 1���� �� �� ������ ���� �߰�
			oneUpActive = true;
			itemObj[0].SetActive(true);
		}
		// Brimstone ȹ�� ��
		if ((haveItems[7] == Alter.Collection.Brimstone) && !brimstoneActive)
		{
			// 1���� �� �� ������ ���� �߰�
			brimstoneActive = true;

			// ���� ����
			x1Tear = x1Blood;
			x2Tear = x2Blood;
		}
		// LessThanThree ȹ�� ��
		if ((haveItems[8] == Alter.Collection.LessThanThree) && !LLLActive)
		{
			// 1���� �� �� ������ ���� �߰�
			LLLActive = true;
			itemObj[1].SetActive(true);
		}
	}

	// �ǰ� ��
	public void Hit(int damage)
	{
		// �ǰ� �� ����
		if (nowHit) return;

		// �ǰ���
		int r = Random.Range(0, 2);
		InGameSFXManager.instance.PlayerHit(r);

		// �ҿ� ��Ʈ�� ������ ���
		if (curSoulHeart > 0)
		{
			// �ҿ� ��Ʈ ����
			curSoulHeart -= damage;
			curHeart -= damage;
			// �������� �ҿ���Ʈ�� ���ҽ�Ű�� ���� ���
			if (curSoulHeart < 0)
			{
				curHeart -= curSoulHeart;
				// ��Ʈ ����
				curRealHeart += curSoulHeart;
				// �ҿ� ��Ʈ 0
				curSoulHeart = 0;
			}
		}
		// �ҿ� ��Ʈ�� ���� ���
		else if (curSoulHeart <= 0)
		{
			// ��Ʈ ����
			curRealHeart -= damage;
			if (curRealHeart < 0)
			{
				curRealHeart = 0;
			}
		}

		// ������� ��
		if (curRealHeart > 0)
		{
			// �Ӹ� ��Ȱ��ȭ
			p_head.SetActive(false);

			// �ǰ� �� ���� ��Ȱ��ȭ
			if (haveItems[8] != Alter.Collection.None)
			{
				itemObj[1].SetActive(false);
			}
			
			// ��Ʈ �ִϸ��̼� ���
			p_anim.SetBool("isHit", true);
			transform.position = transform.position + Vector3.up * 0.3f;
			// �ڷ�ƾ ����
			StartCoroutine(hitOut());
		}
		// ����� ��
		else if (curRealHeart <= 0)
		{
			// 1up ������ ������
			if (itemObj[0].activeInHierarchy && haveItems[4] == Alter.Collection.OneUp)
			{
				// �Ӹ� ��Ȱ��ȭ
				p_head.SetActive(false);
				// ��Ʈ �ִϸ��̼� ���
				p_anim.SetBool("isHit", true);
				transform.position = transform.position + Vector3.up * 0.3f;
				// �ڷ�ƾ ����
				StartCoroutine(hitOut());

				// ������ �Ҹ�
				upAngel();
				// 1ȸ ��Ȱ
				curRealHeart = maxRealHeart;
				curHeart = maxRealHeart;
			}
			// 1up �̼��� ��
			else
			{
				// ��� ó��
				Dead();
			}
		}
	}

	// �ǰ� ���� Ż��
	IEnumerator hitOut()
	{
		nowHit = true;
		// ���� ȿ��
		p_renderer.enabled = true;
		yield return new WaitForSeconds(0.1f);
		p_renderer.enabled = false;
		yield return new WaitForSeconds(0.1f);
		p_renderer.enabled = true;
		yield return new WaitForSeconds(0.1f);
		p_renderer.enabled = false;
		yield return new WaitForSeconds(0.1f);
		// �⺻ ��������Ʈ�� ����
		p_renderer.enabled = true;
		transform.position = transform.position + Vector3.down * 0.3f;
		p_anim.SetBool("isHit", false);
		p_head.SetActive(true);
		yield return new WaitForSeconds(0.1f);
		p_renderer.enabled = false;
		p_head.SetActive(false);

		// ��Ʈ ����
		yield return new WaitForSeconds(0.1f);
		p_renderer.enabled = true;
		// �Ӹ� Ȱ��ȭ
		p_head.SetActive(true);
		// ���� ��Ȱ��ȭ
		if (haveItems[8] != Alter.Collection.None)
		{
			itemObj[1].SetActive(true);
		}
		nowHit = false;
	}

	// ���� ũ�� ȿ��
	IEnumerator scaleEff()
	{
		gameObject.transform.localScale = new Vector3(1, 0.75f, 0);
		yield return new WaitForSeconds(0.1f);
		gameObject.transform.localScale = new Vector3(1, 1.5f, 0);
		yield return new WaitForSeconds(0.1f);
		gameObject.transform.localScale = new Vector3(1, 1, 0);
	}

	// �����
	public void Dead()
	{
        // ��� ������Ʈ ����p_next
        GameObject Dead = Instantiate(p_die, transform.position, transform.rotation);
        
		// �ֻ��� ������Ʈ ������Ʈ ã��
		TossScene dead = parentObj.GetComponent<TossScene>();

        // �÷��̾� ������Ʈ ����
        dead.DestroySelf();
	}

	// ������ ȹ�� ȿ��
	IEnumerator getPillEff(Collider2D collision)
	{
		InGameSFXManager.instance.ItemGet();

		// ȹ���� �˾��� ������Ʈ ��������
		Pills pill = collision.GetComponent<Pills>();

		// ������ ��� �˻�
		for (int i = 0; i < pillsImages.Length; i++)
		{
			// �˾� ������ �ľ��ϱ� ���� ���� ������Ʈ ã��
			PillType type = pillsImages[i].GetComponent<PillType>();

			// �˾� ���� ����
			if (pill.Type == type.pillType)
			{
				// �˾� ȿ�� �ؽ�Ʈ
				PlayerUI ui = p_UI.GetComponent<PlayerUI>();

				// ������ �̹��� on
				ui.onPillsText(i);
				pillsImages[i].SetActive(true);
				yield return new WaitForSeconds(0.1f);
				pillsImages[i].transform.position = transform.position;
				pillsImages[i].transform.position = pillsImages[i].transform.position + Vector3.up * 0.4f;
				yield return new WaitForSeconds(0.8f);
				pillsImages[i].transform.position = pillsImages[i].transform.position + Vector3.down * 0.4f;
				// ������ �̹��� off
				yield return new WaitForSeconds(0.1f);
				ui.offPillsText(i);
				pillsImages[i].SetActive(false);
			}
		}
	}

	// ������ ȹ�� ȿ��
	public IEnumerator getEff(Alter.Collection collection, Collider2D a_col)
	{
		InGameSFXManager.instance.ItemGet();

		// ������ ��� �˻�
		for (int i = 0; i < itmeImages.Length; i++)
		{
			// ȹ���� ������ ������ �Ǻ�
			if (collection == haveItems[i])
			{
				// ������ ȿ�� �ؽ�Ʈ
				PlayerUI ui = p_UI.GetComponent<PlayerUI>();

				// ������ �̹��� on
				ui.onItemsText(i);
				itmeImages[i].SetActive(true);
				yield return new WaitForSeconds(0.1f);
				itmeImages[i].transform.position = transform.position;
				itmeImages[i].transform.position = itmeImages[i].transform.position + Vector3.up * 0.4f;
				yield return new WaitForSeconds(0.8f);
				itmeImages[i].transform.position = itmeImages[i].transform.position + Vector3.down * 0.4f;
				// ������ �̹��� off
				yield return new WaitForSeconds(0.1f);
				ui.offItemsText(i);
				itmeImages[i].SetActive(false);

				// ���� collider �����Ͽ� �ߺ� ȹ�� ����ó��
				a_col.enabled = false;
			}
		}
	}

	// ������ ȹ�� �� �÷��� �� �ִϸ��̼� �Լ�
	public void GetItem()
	{
		StartCoroutine(scaleEff());

		// ���� ��Ų ��Ȱ��ȭ
		if (haveItems[8] != Alter.Collection.None)
		{
			itemObj[1].SetActive(false);
		}

		// ������ ȹ�� �ִϸ��̼� ���
		p_anim.SetBool("isGet", true);
		// �Ӹ� ��Ȱ��ȭ
		p_head.SetActive(false);
		transform.position = transform.position + Vector3.up * 0.1f;

		Invoke("GetItemOut", 1);
	}

	// �˾� ȹ�� �� �÷��� �� �ִϸ��̼� �Լ�
	public void GetPill(Collider2D collision)
	{
		StartCoroutine(scaleEff());
		StartCoroutine(getPillEff(collision));

		// ������ ȹ�� �ִϸ��̼� ���
		p_anim.SetBool("isGet", true);
		// �Ӹ� ��Ȱ��ȭ
		p_head.SetActive(false);
		transform.position = transform.position + Vector3.up * 0.1f;

		Invoke("GetItemOut", 1);
	}

	// ������ ȹ�� �ִϸ��̼� Ż�� �Լ�
	void GetItemOut()
	{
		transform.position = transform.position + Vector3.down * 0.1f;
		// �Ӹ� Ȱ��ȭ
		p_head.SetActive(true);

		// ���� ��Ų Ȱ��ȭ
		if (haveItems[8] != Alter.Collection.None)
		{
			itemObj[1].SetActive(true);
		}

		// �⺻ �ִϸ��̼� ���
		p_anim.SetBool("isGet", false);
	}

	// ��Ƽ�� ������ ��� �ִϸ��̼� Ż�� �Լ�
	void UseItemOut()
	{
		itmeImages[5].transform.position = Vector3.Lerp(itmeImages[5].transform.position, itmeImages[5].transform.position + Vector3.down * 0.4f, 1f);
		// ������ �̹��� off
		itmeImages[5].SetActive(false);

		transform.position = transform.position + Vector3.down * 0.1f;
		// �Ӹ� Ȱ��ȭ
		p_head.SetActive(true);

		// �⺻ �ִϸ��̼� ���
		p_anim.SetBool("isGet", false);
	}

	// �������� ��� �ִϸ��̼� ����
	public void Next(Collider2D collision)
	{
		p_anim.SetBool("goNext", true);
		// �Ӹ� ��Ȱ��ȭ
		p_head.SetActive(false);
		nextStage = true;

		// ���� ��Ų ��Ȱ��ȭ
		if (haveItems[8] != Alter.Collection.None)
		{
			itemObj[1].SetActive(false);
		}

		StartCoroutine(NextStage(collision));
	}

	// �������� ���
	IEnumerator NextStage(Collider2D collision)
	{
		// �������� ��� �ִϸ��̼�
		upMove();

		yield return new WaitForSeconds(0.6f);

		// �������� ��� �ִϸ��̼� 2
		transform.localScale = new Vector3(0.8f, 1, 1);
		while (transform.position != collision.transform.position)
		{
			transform.position = Vector3.MoveTowards(transform.position, collision.transform.position, 18 * Time.deltaTime);

			yield return new WaitForSeconds(0.01f);
		}

		yield return new WaitForSeconds(0.05f);

		// �������� ���
		switch (nowStage)
		{
			// ���� 1�������� �� ��
			case 1:
				// BGM ���� ����
				InGameSoundManager.instance.StopBGM();

                // �ε� UI ����, 2 ����������
                LoadinSceneController.Instance.LoadScene("Cave");
                break;
			// ���� 2�������� �� ��
			case 2:
                // BGM ���� ����
                InGameSoundManager.instance.StopBGM();

                TossScene dead = parentObj.GetComponent<TossScene>();

                // �÷��̾� ������Ʈ ����
                dead.DestroySelf();

                // �ε� UI ����, ���� �޴���
                LoadinSceneController.Instance.LoadScene("MainMenu");
                break;
		}

		// ���� ����ȭ
		transform.localScale = new Vector3(1, 1, 1);

		// �������� �̵� ���� �Ǻ��� bool �� ����
		nextStage = false;
		// Idle �ִϸ��̼� ���
		p_anim.SetBool("goNext", false);

		// �Ӹ� Ȱ��ȭ
		p_head.SetActive(true);

		// ���� ��Ų Ȱ��ȭ
		if (haveItems[8] != Alter.Collection.None)
		{
			itemObj[1].SetActive(true);
		}

		// �������� �̵��� ����ߴ� �ʵ� �� �ʱ�ȭ
		d_time = 0;

		// ��� �÷��̾� ������Ʈ ��Ȱ��ȭ
		gameObject.SetActive(false);
	}

	// �������� ��� �ִϸ��̼� �Լ�
	void upMove()
	{
		// Ʈ������ ���� �̵�
		transform.position = Vector3.Lerp(transform.position, nextCol.transform.position + Vector3.up, 18 * Time.deltaTime);
		d_time += Time.deltaTime;

		// ���� �ݺ��� ���� (����Լ�)
		if (d_time < 0.6f)
		{
			Invoke("upMove", Time.deltaTime);
		}
	}

	// 1up ������ ��� �� �Ҹ� �ִϸ��̼� �Լ�
	void upAngel()
	{
		itemObj[0].transform.position = Vector3.Lerp(itemObj[0].transform.position, itemObj[0].transform.position + Vector3.up, 15 * Time.deltaTime);
		a_time += Time.deltaTime;

		if (a_time < 2f)
		{
			Invoke("upAngel", Time.deltaTime);
		}
		else if (a_time >= 2f)
		{
			itemObj[0].SetActive(false);
		}
	}

	// �÷��̾� ���� �Լ�
	public void DestroyObj()
	{
		Destroy(gameObject);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		// �˾�� ����� ���
		if (collision.tag == "Pills")
		{
			// ������ ȹ�� �ִϸ��̼� ���
			GetPill(collision);
			
		}

		// ������ ���ܰ� ����� ���
		if (collision.tag == "Item")
		{
			Alter item = collision.GetComponent<Alter>();
			
			// ������ �˻�
			for (int i = 0; i < haveItems.Length; i++)
			{
				// ������ ������ ������ �ľ�
				if (haveItems[i] == item.collection)
				{
					haveItem ishave = itmeImages[i].GetComponent<haveItem>();

					// �������� ������ ���� ���� ���� ������ ȹ�� ���
					if (!ishave.isHave)
					{
						// ������ ȹ�� ����
						ishave.nowHave();

						// ������ ȹ�� �ִϸ��̼� ���
						GetItem();
						//getEff(collision);
					}
				}
			}
		}

		// ���� �������� ���۰� ����� ���
		if (collision.tag == "Next")
		{
			// ���� �������� ��(Ʈ������) collider
			nextCol = collision;

			// �������� ��� �ִϸ��̼� ���
			Next(collision);
		}
	}
}
