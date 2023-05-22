using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//===================================

//		��� ���ϴ� ��ũ��Ʈ

//===================================

// ������ ��ũ��Ʈ
public class DoorAniScripts : MonoBehaviour
{
	// �� �ִϸ��̼� 
	private Animator[] m_doorAni;
	
	// ���� �������� ���� �ݶ��̴�
	private GameObject[] m_rigidBox;

	private void Start()
	{
		m_doorAni = new Animator[2];
		m_rigidBox = new GameObject[2];

		for (int i = 0; i < 2; i++)
		{
			m_doorAni[i] = this.transform.GetChild(i).GetChild(3).GetComponent<Animator>();
			m_rigidBox[i] = this.transform.GetChild(i).GetChild(5).gameObject;

		}

		// ���� ���� �� ���»��·� ����
		// �ٲ㵵 ��
		OpenTheDoor();
	}

	// ������ �ִϸ��̼�
	public void OpenTheDoor()
	{
		for (int i = 0; i < 2; i++)
		{
			m_doorAni[i].Play("OpenDoor");
			//if (m_rigidBox[i].active == true)
			m_rigidBox[i].SetActive(false);
		}
	}

	// ������ �ִϸ��̼�
	public void CloseTheDoor()
	{
		for (int i = 0; i < 2; i++)
		{
			m_doorAni[i].Play("CloseDoor");
			//if (m_rigidBox[i].active == false)
			m_rigidBox[i].SetActive(true);
		}

	}

	// ���� �� �Ǵ� ���� �� ��� �׼�(�����ؾߵɼ��� ����)
	public void IdleTheDoor()
	{
		for (int i = 0; i < 2; i++)
			m_doorAni[i].Play("IdleDoor");
	}

}
