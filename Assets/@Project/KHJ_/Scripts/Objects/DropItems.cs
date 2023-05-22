using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ������ ��� ��ũ��Ʈ
public class DropItems : MonoBehaviour
{
	// ����� ������ ���
	public List<GameObject> items;

	// 1ȸ ����� bool
	private bool m_isDroped = false;

	// �ٸ� ��ũ��Ʈ���� ȣ��
	public void ItemDropEvent()
	{
		if (m_isDroped)
			return;

		// ����� ������ ����
		int rand = Random.Range(0, items.Count);

		// ��� ��ġ ����
		float posX = Random.Range(-1f, 1f);
		float posY = Random.Range(-1f, 1f);

		// �μ����(��� ������, ����� ��ġ)
		Instantiate(items[rand],
			new Vector3(transform.position.x + posX,transform.position.y + posY,0),
			Quaternion.identity);

		if (rand == 0)
		{
			InGameSFXManager.instance.CoinDrop();
		}
		else if (rand == 1)
		{
			InGameSFXManager.instance.KeyDrop();
		}

		m_isDroped = true;
	}


}
