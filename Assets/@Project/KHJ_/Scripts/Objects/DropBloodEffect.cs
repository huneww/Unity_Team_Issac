using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

// ���� ����Ʈ
public class DropBloodEffect : MonoBehaviour
{
	// ����Ʈ�� ����Ʈ ������Ʈ ����
	public List<GameObject> Objects;

	// �ݹ�� �޼ҵ�
	public void DropEffect()
	{
		// ����Ʈ�� ����Ʈ ��������
		int rand = Random.Range(0, Objects.Count);

		// ����Ʈ ����
		var obj = Instantiate(Objects[rand],transform.position,Quaternion.identity);

		// ����Ʈ �����
		StartCoroutine(DelayObj(obj));

	}

	// n�ʰ� ���� �� ����� �׼�
	private IEnumerator DelayObj(GameObject _obj)
	{
		yield return new WaitForSeconds(5f);

		StartCoroutine(DestroyObj(_obj));

	}

	// ����� �׼�
	private IEnumerator DestroyObj(GameObject obj)
	{
		while(true)
		{
			// �ణ�� ���� ��
			yield return new WaitForSeconds(0.01f);
			
			// ������Ʈ�� ũ�Ⱑ ���������� ������ ���
			if(obj.transform.localScale.x < 0.3)
			{
				// �ı�
				Destroy(obj);
				break;
			}

			// ������Ʈ ũ�� ���̱�
			obj.transform.localScale += new Vector3(-0.1f, -0.1f, 0);
		}
	}

}
