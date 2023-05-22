using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyNum : MonoBehaviour
{
	// �÷��̾� UI (���� ����)
	public Text keyText;

	// �÷��̾� ��ü
	Player player;

	private void Start()
	{
		// ���� Hierarchy�� �����ϴ� �÷��̾� ������Ʈ ã��
		GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
		// ���� Active ���� �÷��̾� ������Ʈ�� Player ��ũ��Ʈ �������� 
		player = playerObj.GetComponent<Player>();
	}

	// ���� ���� ���� ���� ���� UI text ����
	private void Update()
	{
		keyText.text = player.Keys.ToString();
	}
}
