using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �˾� ȿ��
// �ִ��� ���� ȿ���� �־�� ������
// �˾� ��������Ʈ�� 9�� ���̰�
// ������ �ִ� �ͺ��� ȿ���� ������Ű�°�
// ������ �� ���ٰ� �Ǵ�
public class Pills : MonoBehaviour
{
    // �˾� ȿ���� ����
    public enum PillType
    {
        HealthUp,       // maxü�� 1ĭ ����
        HealthDown,     // maxü�� 1ĭ ����
        PowerUp,        // ��Ÿ� ����
        PowerDown,      // ��Ÿ� ����
        SpeedUp,        // �̵��ӵ� ����
        SpeedDown,      // �̵��ӵ� ����
        TearsUp,        // ���ݼӵ� ����
        TearsDown,      // ���ݼӵ� ����
        BombsAreKey,    // ��ź�� Ű�� ��ġ ��ȯ
    }

    public PillType Type;

    public GameObject playerObj;
    Player player;

	// �� ������ ����
	int MaxHp;
    float Power;
    float Speed;
    float Tears;
    // ���� �� Ÿ�Կ� ���� �˾��� ȿ�� �ݿ�
    private void Start()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");

		// �÷��̾� ������ ��������
		player = playerObj.GetComponent<Player>();

		MaxHp = 0;
        Power = 0;
        Speed = 0;
        Tears = 0;

        switch (Type)
        {
            case PillType.HealthUp:
                MaxHp = 2;
                break;
            case PillType.HealthDown:
                MaxHp = -2;
                break;
            case PillType.PowerUp:
                Power = 0.2f;
                break;
            case PillType.PowerDown:
                Power = -0.2f;
                break;
            case PillType.SpeedUp:
                Speed = 0.15f;
                break;
            case PillType.SpeedDown:
                Speed = -0.15f;
                break;
            case PillType.TearsUp:
                Tears = 0.1f;
                break;
            case PillType.TearsDown:
                Tears = -0.1f;
                break;
        }
    }

    // �浹�˻�
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �÷��̾� �浹 �� 
        if (collision.CompareTag("Player") || collision.CompareTag("PlayerHead"))
        {
            // ������ �ݿ�
            if (player.maxRealHeart + MaxHp > 2 && player.maxHeart + MaxHp < 20)
            {
                player.curHeart += MaxHp;
                player.curRealHeart += MaxHp;
                player.maxRealHeart += MaxHp;
            }
            if (player.curPower + Power > 0.2f && player.curPower + Power < 3f) player.curPower += Power;
            if (player.speed + Speed > 3f && player.speed + Speed < 9f) player.speed += Speed;
            if (player.curTears + Tears > 0.4f && player.curTears + Tears < 2f) player.curTears += Tears;

            // Ÿ���� ��ȯ�� ��� ��ź���� Ű�� ���� ��ȯ
            if (Type == PillType.BombsAreKey)
            {
                int tmp = player.Bombs;

                player.Bombs = player.Keys;
                player.Keys = tmp;
            }

            // �ش� ������Ʈ �ı�
            Destroy(this.gameObject);
        }
    }
}
