using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
	// ��Ʈ UI ������
	public GameObject[] hearts;
	public GameObject[] halfHearts;
	public GameObject[] blankHearts;
	public GameObject[] soulHearts;
	public GameObject[] halfSoules;

	// ��Ƽ�� ������ ������
	public GameObject[] activeItme;

	// �˾� �ؽ�Ʈ
	public GameObject[] PillsText;
	// ������ �ؽ�Ʈ
	public GameObject[] ItemsText;

	// �÷��̾� ��ü
	public GameObject playerObj;
	Player player;

	// ��Ƽ�� ������ �̹���
	Image i_image;

	// ��Ƽ�� ������ �̹��� ����
	bool voltUp;

	private void Start()
	{
		// �� ������Ʈ ã��
		playerObj = GameObject.FindGameObjectWithTag("Player");
		player = playerObj.GetComponent<Player>();

		// ��Ƽ�� ������ �̹��� ���� �ʱ�ȭ
		voltUp = false;

		// ��Ƽ�� ������ ���� ��������
		i_image = activeItme[0].GetComponent<Image>();
	}

	private void Update()
	{
		heartUI();
		nineVolt();
		voltCool();
	}

	// ü�� �˻� �� Ui ���� �Լ�
	void heartUI()
	{
		// �̹����� ��ġ�� �ʰ� ��� ��Ʈ ��Ȱ��ȭ
		for (int i = 0; i < 10; i++)
		{
			hearts[i].SetActive(false);
			halfHearts[i].SetActive(false);
			blankHearts[i].SetActive(false);
		}
		for (int i = 0; i < 9; i++)
		{
			soulHearts[i].SetActive(false);
			halfSoules[i].SetActive(false);
		}

		// ���� ��Ʈ�� ��Ȳ�� �ľ��ϱ� ���� int ������
		int heart = player.curRealHeart / 2;
		int halfHeart = player.curRealHeart % 2;
		int blankHeart = (player.maxRealHeart - player.curRealHeart) / 2;
		int soulHeart = player.curSoulHeart / 2;
		int HalfSoul = player.curSoulHeart % 2;

		// �� ü�� ����
		int count;

		// ���� ¥�� ��Ʈ�� �������� �ν��ϱ� ���� ���� ó��
		if ((player.curHeart % 2 != 1) && (halfHeart + HalfSoul == 0))
		{
			count = player.curHeart / 2;
		}
		else if ((player.curHeart % 2 != 1) && (halfHeart + HalfSoul != 0))
		{
			count = player.curHeart / 2 + 1;
		}
		else
		{
			count = player.curHeart / 2 + 1;
		}

		
		for (int i = 0; i < count; i++)
		{
			// ������ ���� ��Ʈ ���
			if (heart > 0)
			{
				hearts[i].SetActive(true);
				heart--;
			}
			// ����¥�� ���� ��Ʈ ���
			else if (halfHeart == 1)
			{
				halfHearts[i].SetActive(true);
				halfHeart--;
			}
			// �� ��Ʈ ���
			else if (blankHeart > 0)
			{
				blankHearts[i].SetActive(true);
				blankHeart--;
			}
			// ������ �ҿ� ��Ʈ ���
			else if (soulHeart > 0)
			{
				soulHearts[(i - 1)].SetActive(true);
				soulHeart--;
			}
			// ����¥�� �ҿ� ��Ʈ ���
			else if (HalfSoul == 1)
			{
				halfSoules[(i - 1)].SetActive(true);
				HalfSoul--;
			}
		}
	}

	// ��Ƽ�� ������ ȹ�� �� ��Ƽ�� ������ ui�� ǥ��
	void nineVolt()
	{
		// ��Ƽ�� ������ ���� ���̸� �ش� ������ �̹����� ���� ��
		if ((player.activeItem == Alter.Collection.NineVolt) && !voltUp)
		{
			// ��Ƽ�� ������ �̹��� Ȱ��ȭ
			activeItme[0].SetActive(true);
		}
	}

	// ��Ƽ�� ������ �� Ÿ�� ǥ��
	void voltCool()
	{
		// ��Ƽ�� �������� ��Ÿ���� ��
		if (player.i_coolDown > 0)
		{
			// ȸ�� ó��
			i_image.color = Color.gray;
		}
		// ��Ƽ�� ������ ��� ������ �� 
		else if (player.i_coolDown <= 0)
		{
			// �� ����ȭ
			i_image.color = Color.white;
		}
	}

	// ���� ȹ���� �˾� �̸�, ȿ�� ���
	public void onPillsText(int i)
	{
		PillsText[i].SetActive(true);
	}
	// ���� ȹ���� �˾� �̸�, ȿ�� Active false
	public void offPillsText(int i)
	{
		PillsText[i].SetActive(false);
	}
	// ���� ȹ���� ������ �̸�, ȿ�� ���
	public void onItemsText(int i)
	{
		ItemsText[i].SetActive(true);
	}
	// ���� ȹ���� ������ �̸�, ȿ�� Active false
	public void offItemsText(int i)
	{
		ItemsText[i].SetActive(false);
	}
}
