using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BombNum : MonoBehaviour
{
	// �÷��̾� UI (��ź ����)
	public Text bombText;

	// �÷��̾� ��ü
	Player player;

	private void Start()
	{
		// ���� Hierarchy�� �����ϴ� �÷��̾� ������Ʈ ã��
		GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
		// ���� Active ���� �÷��̾� ������Ʈ�� Player ��ũ��Ʈ �������� 
		player = playerObj.GetComponent<Player>();
	}

	// ���� ��ź ���� ���� ���� UI text ����
	private void Update()
	{
		bombText.text = player.Bombs.ToString();
	}
}
