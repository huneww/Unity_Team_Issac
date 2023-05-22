using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Mom_Body : MonoBehaviour
{
    GameObject _Player;
    Player _Player_Stat;

    GameObject _Camera;

    public GameObject _Destroy;

    SpriteRenderer spriteRenderer;//��������Ʈ ����

    _Stat stat;

    int destroyanim_num = 0;//�׾����� �Ǹ� Ƣ��� �׼�Ƚ���� ���� ����

    public GameObject Monster;
    public Vector2 spawnposition;


    private void Start()
    {
        _Player = GameObject.FindGameObjectWithTag("Player");
        _Camera = GameObject.FindGameObjectWithTag("MainCamera");

        _Player_Stat = _Player.GetComponent<Player>();

        spriteRenderer = GetComponent<SpriteRenderer>();

         stat = GetComponentInParent<_Stat>();



        gameObject.SetActive(false);



    }

    private void OnEnable()
    {
        StartCoroutine(DeathCheck());

        StartCoroutine(BodySizeBigger());
    }


    //������ �ڷ�ƾ ���� ����
    IEnumerator DeathCheck()
    {
        while (true)
        {
            if (stat.Hp <= 0)
            {
                
                StopAllCoroutines();
                StartCoroutine(DeathAnim());

                yield break;
            }
            yield return null;
        }
    }

    IEnumerator DeathAnim()
    {
        while (true)
        {
            if (spriteRenderer != null)
            {

                Instantiate(_Destroy, new Vector2(Random.Range(transform.position.x - 1, transform.position.x + 1), Random.Range(transform.position.y - 0.5f, transform.position.y + 0.5f)), Quaternion.identity);

            }

            if (destroyanim_num == 20)
            {
                if (spriteRenderer != null)
                {
                    _Destroy.transform.localScale = new Vector3(3, 3, 3);
                    Instantiate(_Destroy, transform.position, Quaternion.identity);
                    _Destroy.transform.localScale = new Vector3(1, 1, 1);
                }
            }


            destroyanim_num++;



            yield return new WaitForSecondsRealtime(Random.Range(0.1f, 0.3f));

        }
    }


    //���� ��ü�Ϻΰ� ���ö� ���� Ŀ���� ���
    IEnumerator BodySizeBigger()
    {
        float s = 0.0f;
        while (true)
        {
            if (transform.localScale.x<= 1.0f)
            {
                transform.localScale = new Vector3(s, s, s);


            }
            else
            {
                transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                Instantiate(Monster, new((transform.position.x - spawnposition.x), (transform.position.y - spawnposition.y)), Quaternion.identity);

                StartCoroutine(BodySizeBreath());
                yield break;

            }
            s += 2 * Time.deltaTime;
            yield return null;
        }
    }
    //���� ��ü�Ϻΰ� ������ �� ���� ���� ���
    IEnumerator BodySizeBreath()
    {
        float t = 0.0f;
        float breath = 0.05f;
        while (true)
        {
            if(t <=2.0f)
            {
                transform.localScale = new Vector3(1.0f +breath, 1.0f+ breath, 1.0f+ breath);

                
            }
            else
            {
                StartCoroutine(BodySizeSmaller());
                yield break;
            }
            breath *= -1;

            t += 0.5f;
            yield return new WaitForSecondsRealtime(0.5f);
        }
    }
    //���� ��ü�Ϻΰ� ���ö� ���� �۾����� ���
    IEnumerator BodySizeSmaller()
    {
        float s = 1.0f;

        while (true)
        {
            if (transform.localScale.x >= 0.0f)
            {
                transform.localScale = new Vector3(s, s, s);

            }
            else
            {
                transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);

                yield break;
            }
            s -= 1 * Time.deltaTime;

            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerBullet")
        {
            spriteRenderer.color = Color.red;

            Damage(_Player_Stat.curPower);
        }

        if (collision.tag == "BombRange")
        {
            spriteRenderer.color = Color.red;

            Damage(20);
        }
    }

    void Damage(float Damage)
    {
        stat.Hp -= _Player_Stat.curPower;
        Invoke("colorreturn", 0.2f);

    }


    private void colorreturn()
    {

        spriteRenderer.color = Color.white;
    }



    private void OnDestroy()
    {
        if (_Camera != null)
        {

            _Camera.transform.eulerAngles = Vector3.zero;
        }


    }

}
