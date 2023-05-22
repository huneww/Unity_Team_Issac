using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Boss_Monstro : MonoBehaviour
{
    public GameObject _Camera;//���� ī�޶�
    public GameObject _Shadow;//��Ʈ�� �׸���
    public GameObject _Bullet;//�Ѿ�
    public GameObject _Destory;//�ı��ɶ� �����Ǵ� ������Ʈ

    public float high;//���� �����ϴ� ����
    public float low;//���� �����ϴ� ����

    //=========================================
     GameObject _Player;//�÷��̾� ������Ʈ
     Player _Player_Stat;//�÷��̾� ��ũ��Ʈ
     Animator _Anim;//�ִϸ�����
     _Stat _stat;//����

     SpriteRenderer _Shadow_Sprite;//�׸��� �̵��� ��Ʈ���ϱ����� ��ũ��Ʈ
     Bullet_Script _BulletStat;//�Ѿ˿��� �����͸� �־��ֱ� ���� ��ũ��Ʈ
     SpriteRenderer _Sprite;//��������Ʈ ������


     Vector2 Dir;//��Ʈ�ΰ� ���� ����

     Vector2 Jumping1_Position;//���������
     Vector2 Jumping2_Position;//���������
                                   

     Vector2 Jump_Position;//������ ��ġ �����
     Vector2 Drop_Position;//�������� ��ġ �����(�÷��̾ �ִ� ��ġ)


    //���������� ���θ� ����� ���� �Լ�
     Vector3 StartPoint;
     Vector3 EndPoint;

    
     Vector3 _Shadow_SavePoint;

    bool Moving;//���� �ൿ�� �ϰ�����
    bool AttackTime;
    bool camerashaker;
    bool HighJumpbool;

    GameObject map;
    GameObject doorObj;
    NextStageDoor nextDoor;

    int destroyanim_num = 0;//�׾����� �Ǹ� Ƣ��� �׼�Ƚ���� ���� ����



    private void Start()
    {
        _Camera = GameObject.FindGameObjectWithTag("MainCamera");
        _Shadow_Sprite = _Shadow.GetComponent<SpriteRenderer>();

        _Player = GameObject.FindGameObjectWithTag("Player");
        _Player_Stat = _Player.GetComponent<Player>();

        _Anim = GetComponent<Animator>();
        _stat = GetComponent<_Stat>();

        _BulletStat = _Bullet.GetComponent<Bullet_Script>();
        _Sprite = GetComponent<SpriteRenderer>();

        StartCoroutine(Choice());
        StartCoroutine(DeathCheck());

        map = FindObjectOfType<GameManager>().map;
        Debug.Log(map.name);
        //nextDoor = map.GetComponentInChildren<NextStageDoor>();
        //nextDoor.DoorUnAtive();

        Transform[] obj = map.GetComponentsInChildren<Transform>();

        foreach (var n in obj)
            if (n.name == "NextStageDoor")
            {
                nextDoor = n.GetComponent<NextStageDoor>();
                nextDoor.DoorUnAtive();
            }
        Debug.Log(nextDoor.name);
    }

    private void Update()
    {
        _Shadow_SavePoint = new Vector2(transform.position.x + 0.1f, transform.position.y - 0.5f);
        if (transform.position.x < _Player.transform.position.x)
        {
            _Sprite.flipX = true;
            Dir= Vector2.right;
        }
        else
        {
            Dir = Vector2.left;
            _Sprite.flipX = false;

        }

        
    }

    IEnumerator DeathCheck()
    {
        while (true)
        {
            if (_stat.Hp <= 0)
            {
                StopAllCoroutines();
                nextDoor.DoorAtive();
                _Anim.Play("Boss_Monstro_Dead");
                StartCoroutine(DeathAnim());
                
                yield break;
            }
            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(AttackTime)
        {

            if (collision.tag == "Player")
             {
            _Player_Stat.Hit(_stat.Atk);

            }
             if (collision.tag == "PlayerHead")
             {
            _Player_Stat.Hit(_stat.Atk);

             }
        }
    }
    IEnumerator DeathAnim()
    {
        while (true)
        {

            Instantiate(_Destory, new Vector2(Random.Range(transform.position.x - 1, transform.position.x + 1), Random.Range(transform.position.y - 0.5f, transform.position.y + 0.5f)), Quaternion.identity);
            
            if (destroyanim_num == 20)
            {
                _Destory.transform.localScale = new Vector3(3, 3, 3);
                Instantiate(_Destory, transform.position, Quaternion.identity);



                _Destory.transform.localScale = new Vector3(1, 1,1);
                Destroy(gameObject);
            }


            destroyanim_num++;
            


            yield return new WaitForSecondsRealtime(Random.Range(0.1f,0.3f));


        }
    }

    IEnumerator Crushing()
	{

        float crush = 1;
        float timer = 0;
        float crushing = -0.05f;
        Vector3 shake;
        while (true)
        {

            if (Time.timeScale != 0)
            {
                timer += Time.deltaTime;
                crush += crushing;
                shake = new Vector3(1, crush, 1);
            }

            else
            {
                shake = new Vector3(1, crush, 1);
            }

            if(crush <0.3)
			{
                crushing = 0.05f;

               
			}

            if(timer >= 0.1f)
			{
                transform.localScale = Vector3.one;
                _Anim.SetBool("Think", true);
                yield break;

            }

            transform.localScale = shake;
            yield return null;


        }
    }
    IEnumerator Choice()
    {
        while (true)
        {
            if(!Moving)
            {
                StartCoroutine(Crushing());
                yield return new WaitForSecondsRealtime(1.0f);


                _Anim.SetBool("Think", false);

                yield return new WaitForSecondsRealtime(1.0f);

                 int a = Random.Range(0, 100);

                 if(a < 10)
                  {
                    StartCoroutine(ShotBullet());

                }
                 else if(a < 40)
                 {
                    Drop_Position = _Player.transform.position; //�����ϱ��� �÷��̾� ��ġ Ȯ��


                    StartCoroutine(HighJump());

                }
                else
                {
                    Drop_Position = _Player.transform.position; //�����ϱ��� �÷��̾� ��ġ Ȯ��

                    StartCoroutine(lowJump());

                }

            }

            yield return new WaitForSecondsRealtime(Random.Range(0.5f,3.0f));
        }
    }

    //�Ѿ˽�� �Ѹ��� ��� �ڷ�ƾ
    IEnumerator ShotBullet()
    {
        while(true)
        {

            Moving = true;

            StartCoroutine(BodyShake());

            yield return new WaitForSecondsRealtime(1.5f);

            _Anim.SetBool("ShotBullet", true);
            InGameSFXManager.instance.MonstroAtack(Random.Range(0, 2));

            var savegravity = _BulletStat.gravity;

            for (int i = 0; i <_stat.Bullet_Num;i++)
            {

                _BulletStat.damage = _stat.Atk;
                _BulletStat.Speed = new Vector2(Random.Range(8, 14), Random.Range(10, 20));

                _BulletStat.Speed.x *= Dir.x;
                _BulletStat.gravity = 22f*Time.deltaTime;
                _BulletStat.DestroyTime = Random.Range(-1.7f ,-1.5f);
                _BulletStat.transform.localScale = Vector3.one * Random.Range(0.5f, 1.5f);

                Instantiate(_Bullet, new Vector2(transform.position.x,transform.position.y+0.5f), Quaternion.identity);

                _BulletStat.gravity = savegravity;
                _BulletStat.transform.localScale = Vector3.one;
                _BulletStat.DestroyTime = -1.0f;



            }
            yield return new WaitForSecondsRealtime(2.0f);

            _Anim.SetBool("ShotBullet", false);
            Moving = false;
            if(_Anim.GetBool("ShotBullet") ==false)
            {
            yield break;

            }


            yield return null;
        }
       

    }

    //���� ������ �Ѿ��� ��Ѹ��� �ڷ�ƾ
    IEnumerator BulletShower()
    {
        while (true)
        {

            var savegravity = _BulletStat.gravity;

            for (int i = 0; i < _stat.Bullet_Num*2; i++)
            {

                _BulletStat.damage = _stat.Atk;
                _BulletStat.Speed = new Vector2(Random.Range(-10, 10), Random.Range(10, 20));

                _BulletStat.Speed.x *= Dir.x;
                _BulletStat.gravity = 22f * Time.deltaTime;
                _BulletStat.DestroyTime = Random.Range(-1.7f, -1.5f);
                _BulletStat.transform.localScale = Vector3.one * Random.Range(0.5f, 1.5f);

                Instantiate(_Bullet, new Vector2(transform.position.x, transform.position.y + 0.5f), Quaternion.identity);

                _BulletStat.gravity = savegravity;
                _BulletStat.transform.localScale = Vector3.one;
                _BulletStat.DestroyTime = -1.0f;



            }
            yield return new WaitForSecondsRealtime(2.0f);

            if (_Anim.GetBool("ShotBullet") == false)
            {
                yield break;

            }


            yield return null;
        }


    }

    //��������
    IEnumerator HighJump()
    {
        Jumping1_Position = new Vector2(transform.position.x, transform.position.y + high);
        Moving = true;
        float h = 0;
        HighJumpbool = true;
        while (true)
        {
            //�ڷ�ƾ�� �����ϸ� ������ ��
            _Anim.SetBool("Jump", true);

            //���������϶��� ����
            camerashaker = true;


            if (transform.position.y < Jumping1_Position.y)
            {
                transform.position = new Vector2(transform.position.x, transform.position.y + h * Time.deltaTime);
                _Shadow.transform.position = new Vector2(_Shadow.transform.position.x, _Shadow.transform.position.y - h * Time.deltaTime);
            }
        
            else
            {
                transform.position = Jumping1_Position;
               
               yield return new WaitForSecondsRealtime(0.1f);
                _Anim.SetBool("Jump", false);

                yield return new WaitForSecondsRealtime(0.5f);

        
                StartCoroutine(JumpMove(high));

                yield break;

            }

            h+=2;
            yield return null;
        }
    }

    IEnumerator JumpMove(float _high)//high�� (droppoint + high or low)��
    {
        float a = transform.position.x;
        float b = Drop_Position.x;

        while (true)
        {
            //AttackTime Ȱ��ȭ
            AttackTime = true;

            //��������, �������� �ӽú����� ���
            StartPoint = transform.position;
            EndPoint = new Vector3(Drop_Position.x, Drop_Position.y + _high);


            Debug.DrawLine(StartPoint, EndPoint, Color.green);


            //�������� �߾Ӱ��� ����
            Vector3 Center = (StartPoint + EndPoint) * 0.5f;


            //�������� ���� �׷������� �߾Ӱ��� �Ʒ��� ����
            /*
            if(a-b >0)
            Center.y -=(a -b)*5 /3;
            else
            Center.y -=(b -a)*5 /3;
             */
            Center.y -=3;

            //���������� �������� ������ �߾Ӱ��� �������� ����
            StartPoint = StartPoint - Center;
            EndPoint = EndPoint - Center;


            transform.position = Vector3.Slerp(StartPoint, EndPoint, 2.0f*Time.deltaTime);




            _Shadow.transform.position = new Vector2(_Shadow.transform.position.x, _Shadow.transform.position.y);

            /*
            if (_Shadow.transform.position.y <Drop_Position.y)
            {
                _Shadow.transform.position = new Vector2(_Shadow.transform.position.x, _Shadow.transform.position.y - (transform.position.y - StartPoint.y)+1*Time.deltaTime);

            }
            else
            {
                _Shadow.transform.position = new Vector2(_Shadow.transform.position.x, _Shadow.transform.position.y - (transform.position.y - StartPoint.y) - 1 * Time.deltaTime);
            }
             */


            Debug.DrawLine(StartPoint, Center,Color.red);
            Debug.DrawLine(Center, EndPoint, Color.blue);
            Debug.DrawLine(StartPoint, EndPoint, Color.white);

            transform.position += Center;


            if (transform.position.x >= Drop_Position.x - 0.5 && transform.position.x <= Drop_Position.x + 0.5)
            {
            if (transform.position.y >= Drop_Position.y + _high - 0.5 && transform.position.y <= Drop_Position.y + _high + 0.5)
                {


                    _Shadow.transform.position = new Vector2(Drop_Position.x+0.1f,Drop_Position.y - 0.5f);//�̰� ��Ȯ�� ���鼭 �� ������ ��Ȯ�� �Ȱ��µ�?

                    yield return new WaitForSecondsRealtime(0.1f);
                    _Anim.SetBool("Drop", true);

                    StartCoroutine(Drop());

                    yield break;
                
                }

            }


            yield return null;
        }
    }

    IEnumerator Drop()
    {

        float h2 = 0;

        while (true)
        {
            if (transform.position.y >= Drop_Position.y)
            {
                transform.position = new Vector2(transform.position.x , transform.position.y - h2 * Time.deltaTime);
                _Shadow.transform.position = new Vector2(_Shadow.transform.position.x, _Shadow.transform.position.y + h2 * Time.deltaTime);

            }



            //�������°� ������ ��� �ִϸ��̼��� ������ �Ѿ� �߻�
            else
            {
                transform.position = Drop_Position;
                if (HighJumpbool)
                {
                    InGameSFXManager.instance.BoosStomp();
                }
                else
                {
                    InGameSFXManager.instance.LowJump();
                }
                _Shadow_Sprite.color = new Color(0, 0, 0, 200);
                _Shadow.transform.position = _Shadow_SavePoint;
                if (camerashaker)
                {

                    StartCoroutine(CameraShake());
                    camerashaker = false;
                }
                yield return new WaitForSecondsRealtime(0.1f);

                if (HighJumpbool)
                {
                    StartCoroutine(BulletShower());
                    HighJumpbool = false;
                }

                _Anim.SetBool("Drop", false);

                yield return new WaitForSecondsRealtime(0.5f);
                _Anim.Play("Boss_Monstro_Idle");

                    Moving = false;
                AttackTime = false;
                yield break;

            }

            h2++;
            yield return null;
        }
    }

    IEnumerator lowJump()
    {
        Jumping2_Position = new Vector2(transform.position.x, transform.position.y + low);
        Moving = true;


         float h = 0;
  
        while (true)
        {
                //�ڷ�ƾ�� �����ϸ� ������ ��
                _Anim.SetBool("Jump", true);


                if (transform.position.y < Jumping2_Position.y)
                {
                    transform.position = new Vector2(transform.position.x, transform.position.y + h * Time.deltaTime);
                    _Shadow.transform.position = new Vector2(_Shadow.transform.position.x, _Shadow.transform.position.y - h * Time.deltaTime);
                  }

                else
                {
                _Shadow_Sprite.color = new Color(0,0,0,0);
                transform.position = Jumping2_Position;
                _Shadow.transform.position = new Vector2(Jumping2_Position.x + 0.1f, Jumping2_Position.y - low - 0.5f);
                yield return new WaitForSecondsRealtime(0.1f);
                    _Anim.SetBool("Jump", false);

                    StartCoroutine(JumpMove(low));
                yield break;

                }

                h++;

            yield return null;


            

        }
    }

    IEnumerator CameraShake()
    {
        float angle = 1;
        float timer = 0;
        while (true)
        {

            timer += Time.deltaTime;
            angle *= -1;
            Vector3 shake = new Vector3(0, 0, angle);
            _Camera.transform.eulerAngles = shake;

            if (timer >= 0.5f)
            {
                _Camera.transform.eulerAngles = Vector3.zero;

                yield break;
            }
            yield return null;


        }
    }

    IEnumerator BodyShake()
    {
        float angle = 3;
        float timer = 0;
        while (true)
        {

            timer += Time.deltaTime;
            angle *= -1;
            Vector3 shake = new Vector3(0, 0, angle);
            transform.eulerAngles = shake;

            if (timer >= 1.0f)
            {
                transform.eulerAngles = Vector3.zero;

                yield break;
            }
            yield return null;


        }
    }
}
