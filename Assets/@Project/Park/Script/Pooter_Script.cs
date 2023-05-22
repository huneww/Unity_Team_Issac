using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Pooter_Script : MonoBehaviour
{
     GameObject _Player;//�÷��̾� ������Ʈ
    public LayerMask player_layer; //�÷��̾��� ���̾ ������
     Player Player_stat; //�÷��̾� ����

    //====================================================
    public GameObject _Death;//�׾����� �����Ǵ� ������Ʈ
    public GameObject Spawn_Effect;//���� �����ɶ� ������ ����Ʈ
     _Stat stat; //���� ����
     Rigidbody2D rigid;//���� ������
    public float moveTimer;//�������� ���ʿ� �ѹ� �ٲ��� ���Ѵ�.

    public GameObject Bullet;//�Ѿ� ������Ʈ
     Bullet_Script Shoot_Bullet;//�Ѿ��� �߻��ϱ����� ��ũ��Ʈ

     Animator anim;//���� �ִϸ�����
     Vector2 Direction; //������ �̵� ����
    public float _Distance;//�ĺ��Ÿ�
     RaycastHit2D Find_player; //�÷��̾ �ν��ϴ� ����ĳ��Ʈ


    public float AttackSpeed;//������ �� �÷��̾�� �̵��ϴ� �ӵ�
     Collider2D col;//�� ������ �ݶ��̴��� �ε�

    public Transform Now_Position;//������ ���� ��ġ
    private Vector2 _Position;//������ġ�� ��ȯ��Ű������ ����


     SpriteRenderer spriteRenderer;//��������Ʈ ����

    public float Attack_Timer;//������ ���ݼӵ�

    private bool Attack = false;

    private void Start()
    {
        _Player = GameObject.FindGameObjectWithTag("Player");
        //�÷��̾� ����
        Player_stat = _Player.GetComponent<Player>();

        //����
        stat = GetComponent<_Stat>(); //���� ����
        col = GetComponent<Collider2D>();//���� �ݶ��̴�
        Now_Position = GetComponent<Transform>();//���� ��ġ
        rigid = GetComponent<Rigidbody2D>();//������ ������

        anim = GetComponent<Animator>();//������ �ִϸ��̼� ����

        //�Ѿ�
        Shoot_Bullet = Bullet.GetComponent<Bullet_Script>();//�Ѿ˿� ����� ��ũ��Ʈ�� ������

        Instantiate(Spawn_Effect, transform.position, Quaternion.identity);//��������Ʈ �ִϸ��̼� ��ȯ

        _Position = transform.position;//���� ������
        spriteRenderer = GetComponent<SpriteRenderer>();//������ ��������Ʈ

        //��� �ݺ��� �ڷ�ƾ
        StartCoroutine(Monster_Move());//���Ͱ� �������� ������ �����ϴ� �ڷ�ƾ
        StartCoroutine(Shooting_Bullet());//�Ѿ��� ��� �ڷ�ƾ
        StartCoroutine(Hit_Col());//�ĸ´� �ڷ�ƾ
        
        
        
        
    }

    private void Update()
    {
        //ü���� ������ ����
        if (stat.Hp <= 0)
        {
            Instantiate(_Death,transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    //Ǫ���� �������� ����
    private IEnumerator Monster_Move()
    {
        while (true)
        {
            int ChoiceMove = Random.Range(0, 10);//�������� ���� ������

            //������������ ���ݰ� �����̵��� ��������
            if (ChoiceMove < 2)
            {
                Attack = false;

                rigid.AddForce(Vector2.zero);

                rigid.AddForce(new Vector2(Random.Range(-1, 1), Random.Range(-1, 1)) * AttackSpeed, ForceMode2D.Impulse);

            }

            else
            {
                Attack = true;

                //���ݻ��°� �Ǹ� �÷��̾�� �̵�

                if (Attack)
                {
                    rigid.AddForce(Vector2.zero);

                    float r = Random.Range(0, 1f);

                    if (transform.position.x < _Player.transform.position.x)
                    {
                        spriteRenderer.flipX = false;
                        Direction = Vector2.right;


                        rigid.AddForce(Vector2.right * AttackSpeed * r, ForceMode2D.Impulse);
                    }
                    else
                    {
                        spriteRenderer.flipX = true;
                        Direction = Vector2.left;

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
            }
            //�������� �Ǵ��ϴ� �ð�.
            yield return new WaitForSecondsRealtime(moveTimer * Random.Range(0, 1.0f));
        }

    }

    
    //�Ѿ˽�� �ڷ�ƾ
    private IEnumerator Shooting_Bullet()
    {
        while (true)
        {

            //�÷��̾ �ν��ϴ� ����ĳ��Ʈ
            Find_player = Physics2D.Raycast(Now_Position.position, Direction, _Distance, player_layer);
            Debug.DrawRay(Now_Position.position, Direction * _Distance, Color.red);
            int haa = 2;

            //�÷��̾ �ν�������
            if (Find_player)
            {
            
                for(int i = 0; i < stat.Bullet_Num; i++)
                {
                    haa *= -1;
                    Shoot_Bullet.damage = stat.Atk;
                    Shoot_Bullet.Speed = new Vector2(Direction.x * 7, haa);

                    Instantiate(Bullet, Now_Position.position, Quaternion.identity);
                }

            anim.Play("Pooter_Attack");

            Debug.DrawRay(Now_Position.position, Direction * _Distance, Color.yellow);

            yield return new WaitForSecondsRealtime(Attack_Timer);


            }
        yield return null;
        }
    }


    private IEnumerator Hit_Col()
    {
        while (true)
        {
            if (col.tag =="PlayerBullet")
            {
                spriteRenderer.color = Color.red;


                yield return new WaitForSecondsRealtime(0.2f);
                spriteRenderer.color = Color.white;

                yield return new WaitForSecondsRealtime(0.2f);//����Ÿ��
            }
            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerBullet")
        {
            spriteRenderer.color = Color.red;


            Damage(Player_stat.curPower);

        }
        if ((collision.tag == "Wall"))
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

        spriteRenderer.color = Color.white;
    }










}
