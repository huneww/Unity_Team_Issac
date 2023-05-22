using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee_Script : MonoBehaviour
{
    GameObject _Player;//�÷��̾� ������Ʈ
    Player Player_stat; //�÷��̾� ����


    public LayerMask player_layer; //�÷��̾��� ���̾ ������

    public GameObject _Death;//�׾����� �����Ǵ� ������Ʈ
    public GameObject Spawn_Effect;//���� �����ɶ� ������ ����Ʈ
    public float moveTimer;
    public float AttackSpeed;//������ �� �÷��̾�� �̵��ϴ� �ӵ�
    public float notAttack;//�������� ������ ���� �̵�


     _Stat stat; //���� ����
     Rigidbody2D rigid;//���� ������

    Collider2D col;//�� ������ �ݶ��̴��� �ε�

     Transform Now_Position;//������ ���� ��ġ
    private Vector2 _Position;//������ġ�� ��ȯ��Ű������ ����


     SpriteRenderer spriteRenderer;//��������Ʈ ����

    public float Attack_Timer;//������ ���ݼӵ�

    private bool Attack = false;

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
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (Spawn_Effect != null )//���� �ִϸ��̼��� ������������
        {

        Instantiate(Spawn_Effect, transform.position, Quaternion.identity);//��������Ʈ �ִϸ��̼� ��ȯ
        }

        _Position = transform.position;//���� ������
        spriteRenderer = GetComponent<SpriteRenderer>();//������ ��������Ʈ

        //��� �ݺ��� �ڷ�ƾ
        StartCoroutine(Monster_Move());//���Ͱ� �������� ������ �����ϴ� �ڷ�ƾ
        
        
    }

    private void Update()
    {
        
        if (stat.Hp <= 0)
        {
            if(_Death.name == "Bomb")
            {
                Bomb bomb = _Death.GetComponent<Bomb>();
                bomb.setDelay(0);
            }
            Instantiate(_Death,transform.position, Quaternion.identity);
            Destroy(gameObject);
        }


    }

    //�ĸ��� �������� ������ ���ϴ� �ڷ�ƾ
    private IEnumerator Monster_Move()
    {
        while (true)
        {
            int ChoiceMove = Random.Range(0, 10);//�������� ���� ������

            //������������ ���ݰ� �����̵��� ��������
            if (ChoiceMove < 2)
            {
                InGameSFXManager.instance.FlySwam();
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
                        spriteRenderer.flipX = true;

                        

                        rigid.AddForce(Vector2.right * AttackSpeed * r, ForceMode2D.Impulse);
                    }
                    else
                    {
                        spriteRenderer.flipX = false;

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
                yield return new WaitForSecondsRealtime(Random.Range(moveTimer, moveTimer+1.0f));
        }
  
    }

    private void OnTriggerEnter2D(Collider2D collision)
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
            var Knuck = collision.GetComponent<Rigidbody2D>();
            rigid.velocity = Knuck.velocity*0.2f; 


            spriteRenderer.color = Color.red;

            Damage(Player_stat.curPower);

        }
        if ((collision.tag == "Wall"))
        {
            rigid.velocity = Vector2.Reflect(rigid.velocity.normalized, rigid.velocity.normalized);
        }

        if(collision.tag == "BombRange")
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
