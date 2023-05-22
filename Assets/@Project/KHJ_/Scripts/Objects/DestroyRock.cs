using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DestroyRock : MonoBehaviour
{
	// �ı��� �� �̹���
	[SerializeField]
	GameObject m_DestroyRock_Prefab;

	//GameObject m_ChildCollider;

	private void Start()
	{
		//m_ChildCollider = this.transform.GetChild(0).gameObject;
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		// ���� ������ ���� ���
		if(collision.CompareTag("BombRange"))
		{
			// �ı� ���� �߰�
            InGameSFXManager.instance.RockDestroy(Random.Range(0, 2));

            // �ı��� �� �̹��� ����
            Instantiate(m_DestroyRock_Prefab,this.transform.position,Quaternion.identity);
			Debug.Log("Boom");

			// �� ����
			Destroy(this.gameObject);
			//this.GetComponent<BoxCollider2D>().enabled = false;
			//this.GetComponent<SpriteRenderer>().enabled = false;
		}
	}
}
