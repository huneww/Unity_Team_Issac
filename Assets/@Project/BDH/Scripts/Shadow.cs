using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour
{
	// �׸��� ���� ���� Ȯ��
    SpriteRenderer s_renderer;

    void Start()
    {
		s_renderer = GetComponent<SpriteRenderer>();

		// �׸��� ������ ����
		s_renderer.color = new Color(1, 1, 1, 0.5f);
	}

}
