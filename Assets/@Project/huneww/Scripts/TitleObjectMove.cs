using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleObjectMove : MonoBehaviour
{
    // ������ �ӵ�
    public float Speed;
    // ������ �Ÿ�
    public float MoveLength;
    // ���� ��ġ
    private Vector3 curVec;
    // ���� ��ġ
    private Vector3 nextVec;
    // Ÿ��Ʋ�� �ִ��� Ȯ��
    private bool inTitle = true;

    private void Awake()
    {
        // ���� ��ġ ���� ����
        curVec = transform.position;
    }

    private void Update()
    {
        if (inTitle)
        {
            if (transform.position.y <= curVec.y)
            {
                nextVec.y = 1f * Speed * Time.deltaTime;
            }
            else if (transform.position.y >= curVec.y + MoveLength)
            {
                nextVec.y = (-1f) * Speed * Time.deltaTime;
            }

            transform.position += nextVec;
        }
    }
}
