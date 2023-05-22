using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Globin_Script : MonoBehaviour
{
    public GameObject _Player;//�÷��̾� ������Ʈ
    public LayerMask player_layer; //�÷��̾��� ���̾ ������
    public Player Player_stat; //�÷��̾� ����


    public GameObject _Death;//�׾����� �����Ǵ� ������Ʈ
    public GameObject _Death2;//�׾����� �����Ǵ� ������Ʈ(����)

    public GameObject Spawn_Effect;//���� �����ɶ� ������ ����Ʈ
    public _Stat stat; //���� ����
    public Rigidbody2D rigid;//���� ������
    public float moveTimer;
    public Animator anim;
    public SpriteRenderer HeadSprite;//�۷κ�����ӿ����� �Ӹ��� �¿�� ����



    public float AttackSpeed;//������ �� �÷��̾�� �̵��ϴ� �ӵ�
    public float notAttack;//�������� ������ ���� �̵�
    public Collider2D col;//�� ������ �ݶ��̴��� �ε�

    public Transform Now_Position;//������ ���� ��ġ
    private Vector2 _Position;//������ġ�� ��ȯ��Ű������ ����


    public SpriteRenderer spriteRenderer;//��������Ʈ ����
    DropBloodEffect_Monster DropBlood;


    public float Attack_Timer;//������ ���ݼӵ�

   
    private void Start()
    {
        //�÷��̾� ����
        _Player = GameObject.FindGameObjectWithTag("Player");

        Player_stat = _Player.GetComponent<Player>();

        //����
        stat = GetComponent<_Stat>(); //���� ����
        col = GetComponent<Collider2D>();//���� �ݶ��̴�
        Now_Position = GetComponent<Transform>();//���� ��ġ
        rigid = GetComponent<Rigidbody2D>();//������ ������
        anim = GetComponent<Animator>();//���� �ִϸ�����
        spriteRenderer = GetComponent<SpriteRenderer>();
        DropBlood = GetComponent<DropBloodEffect_Monster>();




        if (Spawn_Effect != null )//���� �ִϸ��̼��� ������������
        {

        Instantiate(Spawn_Effect, transform.position, Quaternion.identity);//��������Ʈ �ִϸ��̼� ��ȯ
        }

        _Position = transform.position;//���� ������
        spriteRenderer = GetComponent<SpriteRenderer>();//������ ��������Ʈ

        //��� �ݺ��� �ڷ�ƾ
        StartCoroutine(Monster_Move());//���Ͱ� �������� ������ �����ϴ� �ڷ�ƾ

        InGameSFXManager.instance.GloBinSpawn();
    }

    private void Update()
    {
        
        if (stat.Hp <= 0)
        {
            Instantiate(_Death,transform.position, Quaternion.identity);
            Instantiate(_Death2,transform.position, Quaternion.identity);
            Destroy(gameObject);
        }


    }

    //�ĸ��� �������� ������ ���ϴ� �ڷ�ƾ
    private IEnumerator Monster_Move()
    {
        while (true)
        {
            anim.SetFloat("Horizontal", rigid.velocity.x);
            anim.SetFloat("Vertical", rigid.velocity.y);
            anim.SetBool("UpDown", rigid.velocity.x < rigid.velocity.y);

            


            int ChoiceMove = Random.Range(0, 100);//�������� ���� ������

            //������������ ���ݰ� �����̵��� ��������
            if (ChoiceMove == 0)
            {
                rigid.AddForce(new Vector2(Random.Range(-1, 1), Random.Range(-1, 1)) * AttackSpeed, ForceMode2D.Impulse);
               
            }

            else
            {
                float r = Random.Range(0.7f, 1f);

                if (transform.position.x < _Player.transform.position.x)
                {
                    HeadSprite.flipX = false;

                    rigid.AddForce(Vector2.right * AttackSpeed * r, ForceMode2D.Impulse);
                }
                else
                {
                    HeadSprite.flipX = true;

                    rigid.AddForce(Vector2.left * AttackSpeed * r, ForceMode2D.Impulse);
                }

                if (transform.position.y < _Player.transform.position.y)
                {


                    rigid.AddForce(Vector2.up * AttackSpeed * r, ForceMode2D.Impulse);


                }
                else
                {
                    rigid.AddForce(Vector2.down * AttackSpeed * r, ForceMode2D.Impulse);

                }

                
            }

             //�������� �Ǵ��ϴ� �ð�.
             yield return new WaitForSecondsRealtime(moveTimer*Random.Range(0,1.0f));
        }
  
    }



    private void OnTriggerEnter2D(Collider2D collision)//�ӽ�
    {
        if (collision.tag == "Player")
        {
            Player_stat.Hit(stat.Atk);
        }
        else if (collision.tag == "PlayerHead")
        {
            Player_stat.Hit(stat.Atk);
        }

        if (collision.tag == "PlayerBullet")
        {
            spriteRenderer.color = Color.red;
            HeadSprite.color = Color.red;


            Damage(Player_stat.curPower);

        }
        if ((collision.tag == "Wall"))
        {
            rigid.velocity = Vector2.Reflect(rigid.velocity.normalized, rigid.velocity.normalized);
        }
        if ((collision.tag == "Rock"))
        {
            rigid.velocity = Vector2.Reflect(rigid.velocity.normalized, rigid.velocity.normalized);
        }
        if (collision.tag == "BombRange")
        {
            spriteRenderer.color = Color.red;

            Damage(20);
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if ((collision.tag == "Wall"))
        {
            if ((collision.tag == "Wall"))
            {
                rigid.AddForce(_Player.transform.position - transform.position);
            }
        }
    }

    void Damage(float Damage)
    {
        stat.Hp -= Damage;
        Invoke("colorreturn", 0.2f);

    }


    private void colorreturn()
    {
        DropBlood.DropEffect();

        spriteRenderer.color = Color.white;
        HeadSprite.color = Color.white;
    }



}
