using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BombNum : MonoBehaviour
{
	// 플레이어 UI (폭탄 숫자)
	public Text bombText;

	// 플레이어 본체
	Player player;

	private void Start()
	{
		// 현재 Hierarchy에 존재하는 플레이어 오브젝트 찾기
		GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
		// 현재 Active 중인 플레이어 오브젝트의 Player 스크립트 가져오기 
		player = playerObj.GetComponent<Player>();
	}

	// 현재 폭탄 소지 수에 따라 UI text 수정
	private void Update()
	{
		bombText.text = player.Bombs.ToString();
	}
}
