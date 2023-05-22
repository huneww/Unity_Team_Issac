using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ������ ����
public class Alter : MonoBehaviour
{
	// ���� ������ ���
	public enum Collection
	{
		None,
		TheSadOnion,
		BloodOfTheMartyr,
		MagicMushroom,
		Stigmata,
		OneUp,
		NineVolt,
		Abaddon,
		Brimstone,
		LessThanThree
	}

	float _tear = 0.1f;    // ���� �ӵ�
	float _speed = 0.5f;    // �̵� �ӵ�
	float _power = 0.5f;    // ������
	int _heart = 2;     // �ִ� ü��

	// ������ ���� ����
	int m_randomCode;

	// ������ ������
	public Collection collection;

	// ������ ��� List
	[SerializeField]
	List<GameObject> m_collection;

	// ������ ������ ������ 
	private GameObject m_childClone;

	public Collider2D a_col;

	// ���� ���� ��
	private void Start()
	{
		// ��� ����
		//	m_collection = new List<GameObject>();


		// ���� ������ ����
		m_randomCode = Random.Range(1, m_collection.Count);

		// ���� ������ ������ Ÿ��
		collection = (Collection)m_randomCode;

		// ������ ������ �����ϱ����� ���
		m_childClone = Instantiate(m_collection[m_randomCode], transform.GetChild(0).transform);

	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		// �÷��̾� �浹 ��
		if (collision.CompareTag("Player") || collision.CompareTag("PlayerHead"))
		{
			// �����ۿ� ���� ó��
			CollectionEffect();
		}
	}

	// �����ۿ� ���� ȿ�� �ο�
	private void CollectionEffect()
	{
		var player = FindObjectOfType<Player>();

		switch (collection)
		{
			case Collection.TheSadOnion:
				if (player.haveItems[0] != Collection.TheSadOnion)
				{
					// �÷��̾�� ������ �ο�
					player.haveItems[0] = Collection.TheSadOnion;

					// �÷��̾� �ִϸ��̼� ���
					player.GetItem();
					player.StartCoroutine(player.getEff(Collection.TheSadOnion, a_col));

					// ������ ���� �޼ҵ�
					DestroyCollection();
				}
				break;
			case Collection.BloodOfTheMartyr:
				if (player.haveItems[1] != Collection.BloodOfTheMartyr)
				{
					// �÷��̾�� ������ �ο�
					player.haveItems[1] = Collection.BloodOfTheMartyr;

					// �÷��̾�� ȿ�� ����
					player.curPower += _power;
					player.curPower += _power;

					// �÷��̾� �ִϸ��̼� ���
					player.GetItem();
					player.StartCoroutine(player.getEff(Collection.BloodOfTheMartyr, a_col));

					// ������ ���� �޼ҵ�
					DestroyCollection();
				}
				break;
			case Collection.MagicMushroom:
				if (player.haveItems[2] != Collection.MagicMushroom)
				{
					// �÷��̾�� ������ �ο�
					player.haveItems[2] = Collection.MagicMushroom;

					// �÷��̾�� ȿ�� ����
					player.curTears -= _tear;
					player.curPower += _power;
					player.speed += _speed;

					// �÷��̾� �ִϸ��̼� ���
					player.GetItem();
					player.StartCoroutine(player.getEff(Collection.MagicMushroom, a_col));

					// ������ ���� �޼ҵ�
					DestroyCollection();
				}
				break;
			case Collection.Stigmata:
				if (player.haveItems[3] != Collection.Stigmata)
				{
					// �÷��̾�� ������ �ο�
					player.haveItems[3] = Collection.Stigmata;

					// �÷��̾� �ִϸ��̼� ���
					player.GetItem();
					player.StartCoroutine(player.getEff(Collection.Stigmata, a_col));

					// ������ ���� �޼ҵ�
					DestroyCollection();
				}
				break;

			case Collection.OneUp:
				if (player.haveItems[4] != Collection.OneUp)
				{
					// �÷��̾�� ������ �ο�
					player.haveItems[4] = Collection.OneUp;

					// �÷��̾� �ִϸ��̼� ���
					player.GetItem();
					player.StartCoroutine(player.getEff(Collection.OneUp, a_col));

					// ������ ���� �޼ҵ�
					DestroyCollection();
				}
				break;

			case Collection.NineVolt:
				if (player.haveItems[5] != Collection.NineVolt)
				{
					// �÷��̾�� ������ �ο�
					player.haveItems[5] = Collection.NineVolt;

					// �÷��̾� �ִϸ��̼� ���
					player.GetItem();
					player.StartCoroutine(player.getEff(Collection.NineVolt, a_col));

					// ������ ���� �޼ҵ�
					DestroyCollection();
				}
				break;

			case Collection.Abaddon:
				if (player.haveItems[6] != Collection.Abaddon)
				{
					// �÷��̾�� ������ �ο�
					player.haveItems[6] = Collection.Abaddon;

					// �÷��̾�� ȿ�� ����
					player.speed -= _speed;
					player.speed -= _speed;
					player.curTears -= _tear;
					player.curTears -= _tear;

					// �÷��̾� �ִϸ��̼� ���
					player.GetItem();
					player.StartCoroutine(player.getEff(Collection.Abaddon, a_col));

					// ������ ���� �޼ҵ�
					DestroyCollection();
				}
				break;

			case Collection.Brimstone:
				if (player.haveItems[7] != Collection.Brimstone)
				{
					// �÷��̾�� ������ �ο�
					player.haveItems[7] = Collection.Brimstone;

					// �÷��̾� �ִϸ��̼� ���
					player.GetItem();
					player.StartCoroutine(player.getEff(Collection.Brimstone, a_col));

					// ������ ���� �޼ҵ�
					DestroyCollection();
				}
				break;

			case Collection.LessThanThree:
				if (player.haveItems[8] != Collection.LessThanThree)
				{
					// �÷��̾�� ������ �ο�
					player.haveItems[8] = Collection.LessThanThree;

					// �÷��̾�� ȿ�� ����
					player.curHeart += _heart;
					player.maxRealHeart += _heart;
					player.curHeart += _heart;
					player.maxRealHeart += _heart;


					// �÷��̾� �ִϸ��̼� ���
					player.GetItem();
					player.StartCoroutine(player.getEff(Collection.LessThanThree, a_col));

					// ������ ���� �޼ҵ�
					DestroyCollection();
				}
				break;
		}
	}

	// ������ ���� �޼ҵ�
	private void DestroyCollection()
	{
		// �����ۿ� ����� �ִϸ����� ��Ȱ��ȣ
		m_childClone.GetComponent<Animator>().enabled = false;

		// n�� �� ����
		// �÷��̾ ������ ���� �� �տ� ����ִ� �ִϸ��̼� ��� ��
		// �������⿡ Destroy�� 2���� �μ��� �� ���� �ؾߵ�
		Destroy(m_childClone);
	}
}
