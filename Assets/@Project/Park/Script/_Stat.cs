using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Stat : MonoBehaviour
{
    public float MaxHp;
    public float Hp;//������Ʈ�� ü��
    public int Atk; //������Ʈ ���ݷ�

    public int Bullet_Num; //������Ʈ�� �Ѿ��� ��� ������Ʈ�϶� �߻��� �Ѿ� ����
    public float Atk_Speed; //�����ϴ� �ӵ�

	public void Start()
	{
        Hp = MaxHp;
	}
}
