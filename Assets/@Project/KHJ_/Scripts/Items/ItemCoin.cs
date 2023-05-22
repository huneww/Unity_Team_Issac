using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� ������ 
public class ItemCoin : MonoBehaviour
{
	// �ִϸ�����
	private Animator animator;

	// ���� ȹ�� �ִϸ��̼�
	[SerializeField]
	GameObject m_getCoinAction;

	// ȹ�� �ִϸ��̼� ���� ����
	private GameObject m_clone;

	// ��¦�̴� ȿ���� �ֱ� ���� bool
	private bool idleAction = false;

	// ���� ���� ��
	private void Start()
	{
		// �ִϸ��̼� ���
		animator = GetComponent<Animator>();
		animator.Play("DropCoin");

		// ��¦�̴� ȿ���� �������ֱ�
		StartCoroutine(CoinActionDelay());
	}

	private void Update()
	{
		// ��¦�̴� ȿ�� �ֱ�
		if (idleAction)
		{
			idleAction = false;
			animator.Play("IdleCoin");
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		// �浹�� ��ü�� �±װ� �÷��̾��϶�
		if(collision.CompareTag("Player")|| collision.CompareTag("PlayerHead"))
		{
			InGameSFXManager.instance.CoinGet();

			// �÷��̾��� ������ +1
			Player player;
			if(collision.CompareTag("Player"))
				player = collision.GetComponent<Player>();
			else
				player = collision.transform.parent.GetComponent<Player>();
			player.Coins++;

			// �������� �ִϸ��̼� ���
			m_clone = Instantiate(m_getCoinAction,transform.position,Quaternion.identity);

			Destroy(m_clone, 0.3f);

			// �ڱ��ڽ� ����
			Destroy(this.gameObject);
		}
		
	}

	IEnumerator CoinActionDelay()
	{
		while (true)
		{
			// ��¦�̴� ȿ�� ������ �ð�
			yield return new WaitForSeconds(5f);
			idleAction = true;
		}
	}
}
