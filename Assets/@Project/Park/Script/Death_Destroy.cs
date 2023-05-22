using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death_Destroy : MonoBehaviour
{
    public float AnimationTime;//�״� �ִϸ��̼� �ð��� �����ϴ� �Լ�
    public Animator DeathAnim;

    public GameObject BulletObject;//�� ������Ʈ�� ������쿣 �Ѿ� �߻�
    public int num; //�߻�Ǵ� �Ѿ� ����

    public GameObject SpawnObject;//�ִϸ��̼��� ����ǰ� ������ ������Ʈ
    void Start()
    {
        DeathAnim = GetComponent<Animator>();

        AnimationTime = DeathAnim.GetCurrentAnimatorStateInfo(0).length;

        Invoke("DestroyObject", AnimationTime);

        InGameSFXManager.instance.TearDestroy();

    }

    void DestroyObject()
    {
        if (SpawnObject != null)
        {
            Instantiate(SpawnObject, transform.position,Quaternion.identity);
        }

        if(BulletObject != null)
        {
            for(int i = 0; i <num; i++)
            {
                Instantiate(BulletObject, transform.position, Quaternion.identity);
            }
        }

        Destroy(gameObject);
    }


}
