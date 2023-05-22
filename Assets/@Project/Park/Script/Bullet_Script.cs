using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet_Script : MonoBehaviour
{
     GameObject _Player;//Ÿ���̵� �÷��̾� ������Ʈ
     Player PlayerStat; //(�ӽ�)�÷��̾��� ����
    public LayerMask Player_Layer;//�÷��̾� ���̾�

     Collider2D col; //�浹 Ȯ�ο�

    public GameObject DestroyAnim;//�Ѿ� �浹�� ������� �ִϸ��̼� ���

    public int damage;

    // ���� �߷�
    public float gravity;
    // �ٴ� ������ ����� ����
    float tearHigh;

    public Rigidbody2D rigid;
    public Vector2 Speed;//�Ѿ��� �����̴� ����

    public float DestroyTime =-2.0f;

    public LayerMask Block;

    bool isFall;

    private void Start()
    {
        _Player = GameObject.FindGameObjectWithTag("Player");
        rigid = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
      
        if(_Player)
		{
        PlayerStat = _Player.GetComponent<Player>();//�÷��̾� ����Ȯ��

		}
        
        InGameSFXManager.instance.MonsterTearFire(Random.Range(0, 3));

        rigid.AddForce(Speed,ForceMode2D.Impulse);

        // ���� �߷� ���ӵ�
        tearHigh = -0.007f;

    }

    private void Update()
    {
        High();
        onGround();
    }

    void High()
    {
        if (!gameObject.name.Contains("Monstro"))
        {
            // �߷� ���ӵ��� ���� õõ�� ������ ����
            rigid.AddForce(Vector3.down * gravity, ForceMode2D.Impulse);
            tearHigh += -0.007f;
        }
        else
        {
            // �߷� ���ӵ��� ���� õõ�� ������ ����
            rigid.AddForce(Vector3.down * gravity * 2, ForceMode2D.Impulse);
            tearHigh += -0.007f * 2;
        }
    }

    public void bulletDestroy()
    {
        // ������Ʈ ����
        Destroy(gameObject);

        // ���� ����? �ִϸ��̼� ���
        GameObject tearpoofa = Instantiate(DestroyAnim, transform.position, Quaternion.identity);
    }

    void onGround()
    {
        if (!gameObject.name.Contains("Monstro"))
        {
            // �߷� ���ӵ��� 2�� �Ǿ��� �� �ٴڿ� ��Ҵٰ� ����
            if (tearHigh < DestroyTime)
            {
                // ������Ʈ ����
                Destroy(gameObject);

                // ���� ����? �ִϸ��̼� ���
                GameObject tearpoofa = Instantiate(DestroyAnim, transform.position, Quaternion.identity);
            }
        }
        else
        {
            // �߷� ���ӵ��� 2�� �Ǿ��� �� �ٴڿ� ��Ҵٰ� ����
            if (tearHigh < -1f)
            {
                isFall = true;
            }
            if (tearHigh < DestroyTime)
            {
                // ������Ʈ ����
                Destroy(gameObject);

                // ���� ����? �ִϸ��̼� ���
                GameObject tearpoofa = Instantiate(DestroyAnim, transform.position, Quaternion.identity);
            }
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            DestroyAnim.transform.localScale = transform.localScale;
            // ������Ʈ ����
            Destroy(gameObject);

            // ���� ����? �ִϸ��̼� ���
            GameObject tearpoofa = Instantiate(DestroyAnim, transform.position, Quaternion.identity);

            // ���� �ǰݽ� Hit�Լ� ȣ��
            PlayerStat.Hit(damage);
        }
        else if (collision.tag == "PlayerHead")
        {
            // ������Ʈ ����
            Destroy(gameObject);

            // ���� ����? �ִϸ��̼� ���
            GameObject tearpoofa = Instantiate(DestroyAnim, transform.position, Quaternion.identity);

            // �Ӹ� �ǰݽ� PlayerHead ���� �ڵ带 �̿��� ������ Hit�Լ� ȣ��
            PlayerStat.Hit(damage);
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Block"))
        {
            if (gameObject.name.Contains("Monstro") && isFall)
            {
				DestroyAnim.transform.localScale = transform.localScale;
				// ������Ʈ ����
				Destroy(gameObject);

				// ���� ����? �ִϸ��̼� ���
				GameObject tearpoofa = Instantiate(DestroyAnim, transform.position, Quaternion.identity);
			}
            else if (!gameObject.name.Contains("Monstro"))
            {
				DestroyAnim.transform.localScale = transform.localScale;
				// ������Ʈ ����
				Destroy(gameObject);

				// ���� ����? �ִϸ��̼� ���
				GameObject tearpoofa = Instantiate(DestroyAnim, transform.position, Quaternion.identity);
			}
            
        }
    }
   
     
   


}
