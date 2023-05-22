using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charger_Script : MonoBehaviour
{
    GameObject _Player;//�÷��̾� ������Ʈ
    Player Player_stat; //�÷��̾� ����

    public LayerMask _Player_Layer; //�÷��̾� ���̾�
    public GameObject Spawn_Effect;//���� �����ɶ� ������ ����Ʈ
    public GameObject _Death;//�׾����� �����Ǵ� ������Ʈ

     _Stat stat; //���� ����

     Animator anim;//���� �ִϸ�����
    Vector2 Direction; //������ �̵� ����
    public float _Distance;//�ĺ��Ÿ�

    public LayerMask _Wall_Layer;//�� ���̾�
    public LayerMask _Pit_Layer;//�� ���̾�

    public Collider2D col;

    private RaycastHit2D Find_Player;//�÷��̾ �ĺ��ϴ� ����ĳ��Ʈ


    private RaycastHit2D Find_Wall;//���� �ε������� �ĺ��ϴ� ����ĳ��Ʈ
    private RaycastHit2D Find_Pit;//���� �ε������� �ĺ��ϴ� ����ĳ��Ʈ

     Transform Now_Position;//������ ���� ��ġ
    private Vector2 _Position;//������ġ�� ��ȯ��Ű������ ����


     SpriteRenderer spriteRenderer;//��������Ʈ ����

    private bool Attack = false; //���ݻ������� �ƴ��� Ȯ���ϴ� ����
    public Vector2 AttackSpeed;//������ �� �÷��̾�� �̵��ϴ� �ӵ�

    DropBloodEffect DropBlood;



    //������ �÷��̾ ��ô�ϴ°��� �ƴ� �����¿츦 �����̴ٰ� ���鿡 �÷��̾ �߰��ϸ� �������� �����ؼ� �����Ѵ�.

    private void Start()
    {
        stat = GetComponent<_Stat>();
        _Player = GameObject.FindGameObjectWithTag("Player");
        Player_stat = _Player.GetComponent<Player>();

       


        Instantiate(Spawn_Effect,transform.position,Quaternion.identity);

        Now_Position = GetComponent<Transform>();//������ġ ������

        _Position = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();//��������Ʈ ����, �¿� ������ �ٲܶ� ���
        anim = GetComponent<Animator>();//������ �ִϸ����� ����
        col = GetComponent<Collider2D>();
        DropBlood =gameObject.AddComponent<DropBloodEffect>();



        StartCoroutine(Monster_Move());//���Ͱ� �������� ������ �����ϴ� �ڷ�ƾ
        StartCoroutine(Hit_Col());//�Ѿ��� �¾����� ���� �浹���� �ڷ�ƾ
        

    }

    private void Update()
    {
        if(!Attack)
        {
            //�÷��̾ ã�� ����ĳ��Ʈ
            Find_Player = Physics2D.Raycast(Now_Position.position, Direction, _Distance, _Player_Layer);
             Debug.DrawRay(Now_Position.position, Direction * _Distance, Color.red);
            if(Find_Player)
            {
                AttackSound();
            }
        }

       //�÷��̾ ã�� ����ĳ��Ʈ
        if (Find_Player)
        {
            Attack = Find_Player; //�÷��̾ �߰��ϸ�  Attack���¸� true�� ����\
            Debug.DrawRay(Now_Position.position, Direction* _Distance, Color.yellow);
        }


        //���� �浹�ߴ��� Ȯ���ϴ� ����ĳ��Ʈ
        Find_Wall = Physics2D.Raycast(Now_Position.position, Direction, 0.5f, _Wall_Layer);
        Debug.DrawRay(Now_Position.position, Direction * 0.5f, Color.green);

		Find_Pit = Physics2D.Raycast(Now_Position.position, Direction, 0.5f, _Pit_Layer);
        
		Debug.DrawRay(Now_Position.position, Direction * 0.5f, Color.green);

        if (Find_Wall)
        {

            Attack = false; //���� �浹�ϸ�  Attack���¸� false�� ����
            Direction *= -1;

            if (Direction.x > 0)
            {
                spriteRenderer.flipX = false;
            }
            else if (Direction.x < 0)
            {
                spriteRenderer.flipX = true;
            }
            Debug.DrawRay(Now_Position.position, Direction * 0.5f, Color.blue);

        }
        if (Find_Pit)
        {

            Attack = false; //���� �浹�ϸ�  Attack���¸� false�� ����
            Direction *= -1;

            if (Direction.x > 0)
            {
                spriteRenderer.flipX = false;
            }
            else if (Direction.x < 0)
            {
                spriteRenderer.flipX = true;
            }
            Debug.DrawRay(Now_Position.position, Direction * 0.5f, Color.blue);

        }

        //���ݻ����϶�
        if (Attack)
        {

            if (Direction == new Vector2(0, 1))
            {
                anim.SetBool("Up", true);
                anim.SetBool("Down", false);
                anim.SetBool("Attack", true);

                _Position.y += AttackSpeed.y * Time.deltaTime * 5;
                spriteRenderer.flipX = true;



            }
            if (Direction == new Vector2(0, -1))
            {
                anim.SetBool("Down", true);
                anim.SetBool("Up", false);
                anim.SetBool("Attack", true);

                _Position.y -= AttackSpeed.y * Time.deltaTime * 5;
                spriteRenderer.flipX = true;




            }
            if (Direction == new Vector2(1, 0))
            {

                anim.SetBool("Down", false);
                anim.SetBool("Up", false);
                anim.SetBool("Attack", true);

                _Position.x += AttackSpeed.x * Time.deltaTime * 5;
                spriteRenderer.flipX = false;



            }
            if (Direction == new Vector2(-1, 0))
            {

                anim.SetBool("Down", false);
                anim.SetBool("Up", false);
                anim.SetBool("Attack", true);
                _Position.x -= AttackSpeed.x * Time.deltaTime * 5;
                spriteRenderer.flipX = true;


            }

        }

        //���ݻ��°� �ƴҶ�
        else
        {
            anim.SetBool("Attack", false);

            if (Direction ==new Vector2(0,1))
            {
                anim.SetBool("Up", true);
                anim.SetBool("Down", false);
                _Position.y += AttackSpeed.y * Time.deltaTime;


            }
            if (Direction == new Vector2(0, -1))
            {
                anim.SetBool("Down", true);
                anim.SetBool("Up", false);
                _Position.y -= AttackSpeed.y * Time.deltaTime;

            }
            if (Direction == new Vector2(1,0))
            {
                anim.SetBool("Down", false);
                anim.SetBool("Up", false);
                _Position.x += AttackSpeed.x * Time.deltaTime;

            }
            if (Direction == new Vector2(-1,0))
            {
                anim.SetBool("Down", false);
                anim.SetBool("Up", false);
                _Position.x -= AttackSpeed.x * Time.deltaTime;
            }
   

            

        }

        if(stat.Hp <=0)
        {
            Instantiate(_Death, transform.position, Quaternion.identity);

            Destroy(gameObject);
        }    

        Now_Position.position = _Position;
    }

    void AttackSound()
    {
        InGameSFXManager.instance.ChargerAttack();
    }

  
    private IEnumerator Monster_Move()
    {
        while (true)
        {
            int ChoiceMove = UnityEngine.Random.Range(1, 5);//1~4������ ���� �������� ���Ѵ�.

            if (Attack ==false) 
            {
                //������������ ���ݰ� �����̵��� ��������
                if (ChoiceMove == 1)
                {
                    Direction = new Vector2(0, 1);


                    spriteRenderer.flipX = true;
                }
                else if (ChoiceMove == 2)
                {


                    Direction = new Vector2(0, -1);


                    spriteRenderer.flipX = true;
                }
                else if (ChoiceMove == 3)
                {


                    Direction = new Vector2(-1, 0);

                    spriteRenderer.flipX = true;

                }
                else
                {
                    Direction = new Vector2(1, 0);
                    spriteRenderer.flipX = false;
                }
            }



            //�������� �Ǵ��ϴ� �ð�.
            yield return new WaitForSecondsRealtime(2.5f);
        }

    }


    private void OnTriggerEnter2D(Collider2D collision)//�ӽ�
    {
        if (collision.tag == "Player")
        {
            Direction *= -1;
            if (Direction.x > 0)
            {
                spriteRenderer.flipX = false;
            }
            else if (Direction.x < 0)
            {
                spriteRenderer.flipX = true;

            }

            Player_stat.Hit(stat.Atk);

            Invoke("AttackFalse", 2.0f);
        }
        else if (collision.tag == "PlayerHead")
        {
            Direction *= -1;
            if (Direction.x > 0)
            {
                spriteRenderer.flipX = false;
            }
            else if (Direction.x < 0)
            {
                spriteRenderer.flipX = true;

            }
            Player_stat.Hit(stat.Atk);
            Invoke("AttackFalse", 2.0f);
        }

        if (collision.tag == "PlayerBullet")
        {
            spriteRenderer.color = Color.red;


            Damage(Player_stat.curPower);

        }
        if (collision.tag == "BombRange")
        {
            spriteRenderer.color = Color.red;

            Damage(20);
        }



    }

    private void AttackFalse()
    {
            Attack = false; //�÷��̾ �浹�ϸ�  Attack���¸� false�� �����ϰ� ������ȯ


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


    private IEnumerator Hit_Col()
    {
        while (true)
        {
            if (col.tag == "PlayerBullet")
            {
                spriteRenderer.color = Color.red;


                yield return new WaitForSecondsRealtime(0.2f);
                spriteRenderer.color = Color.white;

                yield return new WaitForSecondsRealtime(0.2f);//����Ÿ��
            }
            yield return null;
        }
    }
}

