using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// blood������Ʈ���� ���� ȿ��
public class BloodGibs : MonoBehaviour
{
	// �ڽİ�ü���� ������ ���� �迭
	private Collider2D[] AllChilds;

	private void Awake()
	{
		// �ڽİ�ü ���� ����
		AllChilds = gameObject.GetComponentsInChildren<Collider2D>();
	}

	// ������ ���ÿ� ȿ�� ����
	private void Start()
	{
		StartCoroutine(DestroyDealy());	
	}

	IEnumerator DestroyDealy()
	{
		// 2���� ���ð� ��
		yield return new WaitForSeconds(2.0f);

		// ��� �ڽ� ��ȯ�ϸ� ����
		foreach(var child in AllChilds)
		{
			// rigidbody�� Constrains ���� Freeze
			child.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
			// �ڽİ�ü�� BoxCollider ��Ȱ��ȭ
			child.GetComponent<BoxCollider2D>().enabled = false;
		}
	}
}
