using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTrigger : MonoBehaviour
{
	// ���� Ȱ��ȭ ����
    public bool mapActivate;
	// ���� Ȱ��ȭ �� �� 1ȸ �� Ʈ���� �ߵ�
    public bool mapTrigger;
	// �ʿ� ���Ͱ� ���� ��
	public bool isClear;

	// ���� ���� ���θ� Ȯ���� ����Ʈ
	List<GameObject> enemyList;
	// ���� ���� ���� ���θ� Ȯ���� ����Ʈ
	List<GameObject> Boss;

	// ������ ����� ��Ʈ��
	BossIntroMove bossIntro;

	// ������ ����
	bool nowBoss;

	private void Start()
	{
		// ����Ʈ ����
		enemyList = new List<GameObject>();
		Boss = new List<GameObject>();

		// ���� ��Ʈ�� ������Ʈ ã��
		bossIntro = FindObjectOfType<BossIntroMove>();

	}

	// ���� ����
	public enum roomType
	{
		Start,
		Enemy,
		Shop,
		Gold,
		Boss
	}
	
	// �� ������Ʈ�� ����
	public roomType r_type;

	// ������ ��ġ
	public GameObject[] spwanVec;

	// ������ ������Ʈ
	public GameObject[] spwanObj;
	

	private void Update()
	{
		// ���� Ȱ��ȭ ���� �ʾҰ� �� Ʈ���Ű� �ߵ����� �� ���� 1ȸ�� ���Ͽ�
		if (!mapActivate && mapTrigger)
		{
			// �� ������ ����
			switch(r_type)
			{
				// ���� ����
				case roomType.Enemy:
					MonsterSpawn();
					// �� �ݱ� ���� �� ����
					isClear = false;
					break;
				// ���� ������ ����
				case roomType.Shop:
					ProductSpawn();
					break;
				// ������ ���� ����
				case roomType.Gold:
					ItemSpawn();
					break;
				// ���� ����
				case roomType.Boss:
					InBoss();
					// �� �ݱ� ���� �ʵ� �� ����
					nowBoss = true;
					isClear = false;
					break;
			}

			// �� Ȱ��ȭ
			mapActivate = true;
		}

		// �� �ȿ� ���Ͱ� ���翩�� Ȯ��
		if (enemyList.Count > 0)
		{
			// ���� ���Ͱ� �׾��� ���
			for (int i = 0; i < enemyList.Count; i++)
			{
				// ���� ����Ʈ���� ���� ����
				if (enemyList[i] == null)
				{
					enemyList.RemoveAt(i);
				}
			}
		}
		
		// ���� ���Ͱ� �濡 �� ������ �������� ���� ���
		else if (enemyList.Count <= 0 && !nowBoss)
		{
			// �� Ŭ����
			isClear = true;
		}

		// �������̿��� ������ �׾��� ��
		if (nowBoss == true && Boss[0] == null)
		{
			// �� Ŭ����
			nowBoss = false;
			isClear = true;

			// ���� Ŭ���� ������ ����
            GameObject spawnObj = Instantiate(spwanObj[1], spwanVec[1].transform.position, spwanVec[1].transform.rotation);
        }
	}

	// ���� ���� �Լ�
	void MonsterSpawn()
	{
		// ���� ���� ��ŭ
		for (int i = 0; i < spwanVec.Length; i++)
		{
			// ���� ���͸�
			int j = Random.Range(0, spwanObj.Length);
			
			// ���� ��ġ�� ��ȯ
			GameObject spawnObj = Instantiate(spwanObj[j], spwanVec[i].transform.position, spwanVec[i].transform.rotation);

			// ���� ����Ʈ�� �߰�
			enemyList.Add(spawnObj);
		}
	}

	// ���� ��ǰ ���� �Լ�
	void ProductSpawn()
	{
		// ���� �� ��ǰ ���� ��ŭ
		for (int i = 0; i < spwanVec.Length; i++)
		{
			// ��ǰ ����
			GameObject spawnObj = Instantiate(spwanObj[0], spwanVec[i].transform.position, spwanVec[i].transform.rotation);
		}
	}

	// ������ ���� ���� �Լ�
	void ItemSpawn()
	{
		// ������ ����
		GameObject spawnObj = Instantiate(spwanObj[0], spwanVec[0].transform.position, spwanVec[0].transform.rotation);
	}

	// ���� ���� �Լ�
	void InBoss()
	{
		// ���� ���� ��ġ��
		GameObject spawnObj = Instantiate(spwanObj[0], spwanVec[0].transform.position, spwanVec[0].transform.rotation);

		// ���� ����
		Boss.Add(spawnObj);

		// ���� ��Ʈ�� BGM ����
		InGameSFXManager.instance.Castleport();
		Invoke("PlayBossBGM", 1.8f);
		// ���� ��Ʈ��
		bossIntro.StartIntro();
	}

	// ���� ��Ʈ�� BGM �Լ�
	void PlayBossBGM()
	{
		InGameSoundManager.instance.PlayBossBGM();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		// �濡 �÷��̾ ������ ��
		if (collision.tag == "Player")
		{
			// �� Ʈ���� �ߵ�
			mapTrigger = true;
		}

		// �濡 ���Ͱ� �߰� ����, ��Ȱ ���� ��
		if (collision.tag == "Enemy")
		{
			// ���� ����Ʈ�� �߰�
			enemyList.Add(collision.gameObject);
			isClear = false;
		}
	}
}
