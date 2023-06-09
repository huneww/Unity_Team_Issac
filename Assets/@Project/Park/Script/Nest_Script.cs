using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEngine;

public class Nest_Script : MonoBehaviour
{
     GameObject _Player;//플레이어 오브젝트
    public LayerMask player_layer; //플레이어의 레이어를 가져옴
     Player Player_stat; //플레이어 스텟


    public GameObject _Death;//죽었을때 생성되는 오브젝트
    public GameObject[] FlySpawn;//죽었을때 생성되는 Fly 오브젝트들

    public GameObject Spawn_Effect;//몬스터 생성될때 나오는 이펙트
     _Stat stat; //몬스터 스텟
     Rigidbody2D rigid;//몬스터 리지드
    public float moveTimer;
     Animator anim;
    public SpriteRenderer HeadSprite;//글로빈움직임에따라 머리가 좌우로 반전
    public float RunRange; // 도망을 판단하는 거리



    public float AttackSpeed;//공격할 때 플레이어에게 이동하는 속도
    public float notAttack;//공격하지 않을때 랜덤 이동
     Collider2D col;//현 몬스터의 콜라이더를 로드

    Transform Now_Position;//몬스터의 현재 위치
    private Vector2 _Position;//현재위치를 변환시키기위한 변수


     SpriteRenderer spriteRenderer;//스프라이트 렌더
    DropBloodEffect_Monster DropBlood;


    public float Attack_Timer;//몬스터의 공격속도

    private bool isClose = false; // 플레이어가 접근했는가

   
    private void Start()
    {
        //플레이어 스텟
        _Player = GameObject.FindGameObjectWithTag("Player");

        Player_stat = _Player.GetComponent<Player>();

        //몬스터
        stat = GetComponent<_Stat>(); //몬스터 스텟
        col = GetComponent<Collider2D>();//몬스터 콜라이더
        Now_Position = GetComponent<Transform>();//현재 위치
        rigid = GetComponent<Rigidbody2D>();//몬스터의 리지드
        anim = GetComponent<Animator>();//몬스터 애니메이터
        spriteRenderer = GetComponent<SpriteRenderer>();
        DropBlood = GetComponent<DropBloodEffect_Monster>();




        if (Spawn_Effect != null )//스폰 애니메이션을 적용시켰을경우
        {

        Instantiate(Spawn_Effect, transform.position, Quaternion.identity);//스폰이펙트 애니메이션 소환
        }

        _Position = transform.position;//현재 포지션
        spriteRenderer = GetComponent<SpriteRenderer>();//몬스터의 스프라이트

        //계속 반복될 코루틴
        StartCoroutine(Monster_Move());//몬스터가 공격할지 안할지 선택하는 코루틴
       // StartCoroutine(Hit_Col());//공격당했을때 코루틴
        
    }

    private void Update()
    {
        
        if (stat.Hp <= 0)
        {
            //플라이 오브젝트 갯수만큼 생성
            for(int i = 0; i < FlySpawn.Length; i++)
            {
                Instantiate(FlySpawn[i], transform.position, Quaternion.identity);

            }


            Instantiate(_Death,transform.position, Quaternion.identity);
            Destroy(gameObject);
        }


    }

    //파리가 공격할지 안할지 정하는 코루틴
    private IEnumerator Monster_Move()
    {
        while (true)
        {
            anim.SetFloat("Horizontal", rigid.velocity.x);
            anim.SetFloat("Vertical", rigid.velocity.y);
            anim.SetBool("UpDown", rigid.velocity.x < rigid.velocity.y);

            //플레이어가 가까이오면 도망감

            if (
                 ((_Player.transform.position.x - transform.position.x <= RunRange) &&(_Player.transform.position.y - transform.position.y <= RunRange) && (_Player.transform.position.x - transform.position.x >= -RunRange) && (_Player.transform.position.y - transform.position.y >= -RunRange))
                )
            {
                if (!isClose)
                {
                    InGameSFXManager.instance.NestRun();
                    isClose = true;
                }

                float r = Random.Range(0.7f, 1f);


                if (transform.position.x < _Player.transform.position.x)
                {
                    HeadSprite.flipX = true;
                    rigid.AddForce(Vector2.left * AttackSpeed * r, ForceMode2D.Impulse);

                }
                else
                {
                    HeadSprite.flipX = false;
                    rigid.AddForce(Vector2.right * AttackSpeed * r, ForceMode2D.Impulse);

                }

                if (transform.position.y < _Player.transform.position.y)
                {

                    rigid.AddForce(Vector2.down * AttackSpeed * r, ForceMode2D.Impulse);

                }
                else
                {
                    rigid.AddForce(Vector2.up * AttackSpeed * r, ForceMode2D.Impulse);

                }


            }
            //멀리있으면 랜덤이동

            else
            {

                isClose = false;
                rigid.AddForce(new Vector2(Random.Range(-1, 1), Random.Range(-1, 1)) * AttackSpeed, ForceMode2D.Impulse);
               
            }

             //움직임을 판단하는 시간.
             yield return new WaitForSecondsRealtime(moveTimer*Random.Range(0,1.0f));
        }
  
    }




    private void OnTriggerEnter2D(Collider2D collision)//임시
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
