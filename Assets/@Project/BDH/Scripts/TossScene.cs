using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TossScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
		// �� �ε�� �ı� �Ұ��� ����
		DontDestroyOnLoad(gameObject);
	}

    // �ֻ��� ������Ʈ �ı� �Լ�
    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
