using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDead : MonoBehaviour
{
	// �ִϸ��̼� ���� ������Ʈ
	Animator anim;

	private void Awake()
	{
		anim = GetComponent<Animator>();

		StartCoroutine(rotate());
	}

	IEnumerator rotate()
	{
		// ��Ʈ �� ȿ��
		gameObject.transform.localScale = new Vector3(1, 0.75f, 0);
		yield return new WaitForSeconds(0.1f);
		gameObject.transform.localScale = new Vector3(1, 1.5f, 0);
		yield return new WaitForSeconds(0.1f);
		gameObject.transform.localScale = new Vector3(1, 1, 0);

		yield return new WaitForSeconds(0.4f);

		// �������� ȿ��

		Vector3 rotaVec = new Vector3(0, 0, -15);
		transform.Rotate(rotaVec);

		yield return new WaitForSeconds(0.075f);

		transform.Rotate(rotaVec);

		yield return new WaitForSeconds(0.05f);

		transform.Rotate(rotaVec);

		yield return new WaitForSeconds(0.015f);

		transform.Rotate(rotaVec);

		yield return new WaitForSeconds(0.01f);

		transform.Rotate(rotaVec);

		yield return new WaitForSeconds(0.025f);

		transform.Rotate(rotaVec);

		Invoke("Dead", 0.1f);
	}

	void Dead()
	{
		// ����� ���� ������ ũ��� ����
		Vector3 rotaVec = new Vector3(0, 0, 90);
		transform.Rotate(rotaVec);

		transform.position += Vector3.down * 0.1f;

		// ���� ��� ��������Ʈ ���
		anim.SetBool("isEnd", true);

		Invoke("backToMenu", 1.5f);
	}

	// �÷��̾� ��� �� ���� �޴��� ����
	void backToMenu()
	{
		LoadinSceneController.Instance.LoadScene("MainMenu");
	}
}
