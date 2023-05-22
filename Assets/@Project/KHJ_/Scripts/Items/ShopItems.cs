using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ������ �� 
public class ShopItems : MonoBehaviour
{
	// �Ǹ� �� ������ �̹���
	public List<Sprite> ItemSprite;
	
	// �Ǹ��� ������ �̹����� �´� ������
	public List<GameObject> Items;

	// ���� ������Ʈ
	public List<GameObject> NumberList;

	// �������� ����
	[SerializeField]
	private int Price;

	// �Ǹ��� ������ ���� ����
	private int rand;

	// ������ �̹��� �����ϱ� ���� ����
	private SpriteRenderer m_itemSprite;

	// ����ǥ�ÿ�(1�� �ڸ�) ������
	private Transform UnitsPosition;
	// ����ǥ�ÿ�(10�� �ڸ�) ������
	private Transform TensPosition;
	// �޷�ǥ�ÿ� ������Ʈ
	private GameObject DollarObject;

	// ����ǥ�ÿ����� ��ȯ�� ������Ʈ(1�� �ڸ�)
	private GameObject m_unitsClone;
	// ����ǥ�ÿ����� ��ȯ�� ������Ʈ(10�� �ڸ�)
	private GameObject m_tensClone;

	// �Ǹ��� ������Ʈ ���� ����
	private GameObject m_itemClone;
	

	// ���� ��
	private void Start()
	{
		// �Ǹ��� ������Ʈ ����
		rand = Random.Range(0, ItemSprite.Count);
		// ���� ����(���� 1~ 15)
		Price = Random.Range(0, 8) + 1;

		// ������ �������� �̹��� ����
		m_itemSprite = transform.GetChild(0).transform.GetComponent<SpriteRenderer>();
		m_itemSprite.sprite = ItemSprite[rand];
		
		// ������ ������ �̸� ���� �� ��Ȱ��ȭ
		m_itemClone = Instantiate(Items[rand], transform.GetChild(0).position, Quaternion.identity);
		m_itemClone.SetActive(false);

		// ���� ǥ����ġ ����
		UnitsPosition = transform.GetChild(2);
		TensPosition = transform.GetChild(3);
		DollarObject = transform.GetChild(1).gameObject;

		// ������ ���� �̹����� ��ȯ
		m_unitsClone = Instantiate(NumberList[Price % 10],UnitsPosition);
		if(Price >=10)
			m_tensClone = Instantiate(NumberList[Price / 10],TensPosition);

	}

	// �浹�˻�
	private void OnTriggerEnter2D(Collider2D collision)
	{
		// �̹����� ���ٸ� ��ȯ
		if (m_itemSprite == null)
			return; 

		// �÷��̾� �Ǵ� �÷��̾��� �Ӹ��� �浹 ��
		if(collision.CompareTag("Player")||collision.CompareTag("PlayerHead"))
		{
			// �÷��̾� �� �޾ƿ���
			Player player;
			if (collision.CompareTag("Player"))
				player = collision.GetComponent<Player>();
			else
				player = collision.transform.parent.GetComponent<Player>();

			// �÷��̾ ���� �����ݰ� ���� ��
			if(player.Coins >= Price)
			{
				// �����ݿ��� ���ݸ�ŭ ����
				player.Coins -= Price;

				// ��Ȱ��ȭ �� ������ Ȱ��ȭ
				m_itemClone.SetActive(true);

				// Ȯ�ο� Debug.Log
				Debug.Log("Buy Item");

				// �ݶ��̴� ��Ȱ��ȭ
				this.transform.GetComponent<BoxCollider2D>().enabled = false;

				// ������ �������� ������ �׿� ������Ʈ�� �ı�
				Destroy(m_itemSprite);
				DollarObject.SetActive(false);
				Destroy(m_tensClone);
				Destroy(m_unitsClone);

			}
		}
	}

}
