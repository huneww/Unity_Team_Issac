using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class InGameSFXManager : MonoBehaviour
{
    // ������ ���� ���� ����
    public static InGameSFXManager instance;

    // ����� Ŭ�� ���� List
    public List<AudioClip> audioClips;

    // ����� Ŭ�� �ε���
    public enum SFX
    {
        BossIntro,
        BossOutro,
        MonsterTearFire_1,
        MonsterTearFire_2,
        MonsterTearFire_3,
        MonsterTearFire_4,
        PlayerHit_1,
        PlayerHit_2,
        PlayerHit_3,
        KeyDrop,
        KeyGet,
        MenuOpen,
        MenuClose,
        CoinDrop,
        CoinGet,
        Plop,
        ItemGet,
        RockDestroy_1,
        RockDestroy_2,
        RockDestroy_3,
        CampFireOff,
        PlayerTearFire,
        TearDestroy,
        Unlock,
        MonsterDead_1,
        MonsterDead_2,
        MonsterDead_3,
        MonsterDead_4,
        MonsterDead_5,
        NestVoice,
        Boom,
        PillsDown,
        PillsUp,
        Castleport,
        Monstro_Atk_1,
        Monstro_Atk_2,
        Monstro_Atk_3,
        DoorOpen,
        DoorClose,
        FlySwam,
        GetBoom,
        BoosStomp,
        Chubber_Atk,
        Charger_Atk,
        NestRun,
        GloBinReSpawn,
        LowJump
    }

    // ������ ���� ����
    private InGameSFXManager() { }

    // ����� �ҽ� ���۳�Ʈ
    private AudioSource AudioManager;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        // ����� �ҽ� ���۳�Ʈ �߰�
        AudioManager = GetComponent<AudioSource>();

        // �о�� ������ �� ������ ����
        AudioManager.volume = VolumeSaveLoad.SFXLoad();
    }

    /// <summary>
    /// ���� ��Ʈ�� ����
    /// </summary>
    public void BossIntro()
    {
        AudioManager.PlayOneShot(audioClips[(int)SFX.BossIntro]);
    }

    /// <summary>
    /// ���� �ƿ�Ʈ�� ����
    /// </summary>
    public void BossOuTro()
    {
        AudioManager.PlayOneShot(audioClips[(int)SFX.BossOutro]);
    }

    /// <summary>
    /// ���� ���� �߻� �Ҹ�
    /// </summary>
    /// <param name="index">0 ~ 3�� ������ ���� ���� ���</param>
    public void MonsterTearFire(int index)
    {
        switch (index)
        {
            case 0:
                AudioManager.PlayOneShot(audioClips[(int)SFX.MonsterTearFire_1]);
                break;
            case 1:
                AudioManager.PlayOneShot(audioClips[(int)SFX.MonsterTearFire_2]);
                break;
            case 2:
                AudioManager.PlayOneShot(audioClips[(int)SFX.MonsterTearFire_3]);
                break;
            case 3:
                AudioManager.PlayOneShot(audioClips[(int)SFX.MonsterTearFire_4]);
                break;
            default:
                Console.WriteLine("�ε��� ���� �̻��մϴ�. MonsterTearFrie�� 0 ~ 3 ������ ���� �����ؾ��մϴ�.");
                break;
        }
    }

    /// <summary>
    /// �÷��̾� ��Ʈ ����
    /// </summary>
    /// <param name="index">0 ~ 2�� ������ ���� ���� ���</param>
    public void PlayerHit(int index)
    {
        switch (index)
        {
            case 0:
                AudioManager.PlayOneShot(audioClips[(int)SFX.PlayerHit_1]);
                break;
            case 1:
                AudioManager.PlayOneShot(audioClips[(int)SFX.PlayerHit_1]);
                break;
            case 2:
                AudioManager.PlayOneShot(audioClips[(int)SFX.PlayerHit_1]);
                break;
            default:
                Debug.Log("�ε��� ���� �̻��մϴ�. PlayerHit�� 0 ~ 2 ������ ���� �����ؾ��մϴ�.");
                break;
        }
    }

    /// <summary>
    /// ���� ȹ�� ����
    /// </summary>
    public void KeyGet()
    {
        AudioManager.PlayOneShot(audioClips[(int)SFX.KeyGet]);
    }

    /// <summary>
    /// ���� ��� ����
    /// </summary>
    public void KeyDrop()
    {
        AudioManager.PlayOneShot(audioClips[(int)SFX.KeyDrop]);
    }

    /// <summary>
    /// �Ͻ� ���� �޴� ���� ����
    /// </summary>
    public void PauseMenuOpen()
    {
        AudioManager.PlayOneShot(audioClips[(int)SFX.MenuOpen]);
    }

    /// <summary>
    /// �Ͻ� ���� �޴� �ݴ� ����
    /// </summary>
    public void PauseMenuClose()
    {
        AudioManager.PlayOneShot(audioClips[(int)SFX.MenuClose]);
    }

    /// <summary>
    /// ���� ȹ�� ����
    /// </summary>
    public void CoinGet()
    {
        AudioManager.PlayOneShot(audioClips[(int)SFX.CoinGet]);
    }

    /// <summary>
    /// ���� ��� ����
    /// </summary>
    public void CoinDrop()
    {
        AudioManager.PlayOneShot(audioClips[(int)SFX.CoinDrop]);
    }

    /// <summary>
    /// �� ������Ʈ �ı� ����
    /// </summary>
    public void Poop()
    {
        AudioManager.PlayOneShot(audioClips[(int)SFX.Plop]);
    }

    /// <summary>
    /// ������ ȹ�� ����
    /// </summary>
    public void ItemGet()
    {
        AudioManager.PlayOneShot(audioClips[(int)SFX.ItemGet]);
    }

    /// <summary>
    /// �� ������Ʈ �ı� ����
    /// </summary>
    /// <param name="index"> 0 ~ 2�� ������ ���� ���� ���</param>
    public void RockDestroy(int index)
    {
        switch (index)
        {
            case 0:
                AudioManager.PlayOneShot(audioClips[(int)SFX.RockDestroy_1]);
                break;
            case 1:
                AudioManager.PlayOneShot(audioClips[(int)SFX.RockDestroy_1]);
                break;
            case 2:
                AudioManager.PlayOneShot(audioClips[(int)SFX.RockDestroy_1]);
                break;
            default:
                Debug.Log("�ε��� ���� �̻��մϴ�. RockDestroy�� 0 ~ 2 ������ ���� �����ؾ��մϴ�.");
                break;
        }
    }

    /// <summary>
    /// ķ�����̾� �� ������ ����
    /// </summary>
    public void CampFireOff()
    {
        AudioManager.PlayOneShot(audioClips[(int)SFX.CampFireOff]);
    }

    /// <summary>
    /// �÷��̾� ���� ����
    /// </summary>
    public void PlayerTearFire()
    {
        AudioManager.PlayOneShot(audioClips[(int)SFX.PlayerTearFire]);
    }

    /// <summary>
    /// ���� �ı� ����
    /// </summary>
    public void TearDestroy()
    {
        AudioManager.PlayOneShot(audioClips[(int)SFX.TearDestroy]);
    }

    /// <summary>
    /// ���� ��� ����
    /// </summary>
    public void Unlock()
    {
        AudioManager.PlayOneShot(audioClips[(int)SFX.Unlock]);
    }

    /// <summary>
    /// ���� �״� ����
    /// </summary>
    /// <param name="index">0 ~ 4�� ������ ���� ���� ���</param>
    public void MonsterDead(int index)
    {
        switch (index)
        {
            case 0:
                AudioManager.PlayOneShot(audioClips[(int)SFX.MonsterDead_1]);
                break;
            case 1:
                AudioManager.PlayOneShot(audioClips[(int)SFX.MonsterDead_2]);
                break;
            case 2:
                AudioManager.PlayOneShot(audioClips[(int)SFX.MonsterDead_3]);
                break;
            case 3:
                AudioManager.PlayOneShot(audioClips[(int)SFX.MonsterDead_4]);
                break;
            case 4:
                AudioManager.PlayOneShot(audioClips[(int)SFX.MonsterDead_5]);
                break;
            default:
                Debug.Log("�ε��� ���� �̻��մϴ�. MonsterDead�� 0 ~ 4 ������ ���� �����ؾ��մϴ�.");
                break;
        }
    }

    /// <summary>
    /// Nest ���̽�
    /// </summary>
    public void NextVoice()
    {
        AudioManager.PlayOneShot(audioClips[(int)SFX.NestVoice]);
    }

    /// <summary>
    /// ��ź ������ ����
    /// </summary>
    public void Boom()
    {
        AudioManager.PlayOneShot(audioClips[(int)SFX.Boom]);
    }

    /// <summary>
    /// �˾��� �ɷ�ġ �ٿ��� ���
    /// </summary>
    public void PillsDown()
    {
        AudioManager.PlayOneShot(audioClips[(int)SFX.PillsDown]);
    }

    /// <summary>
    /// �˾��� �ɷ�ġ ���� ���
    /// </summary>
    public void PillsUp()
    {
        AudioManager.PlayOneShot(audioClips[(int)SFX.PillsUp]);
    }

    /// <summary>
    /// ���� �� ����� �������� �Ҹ�
    /// </summary>
    public void Castleport()
    {
        AudioManager.PlayOneShot(audioClips[(int)SFX.Castleport]);
    }

    /// <summary>
    /// ���� ��Ʈ�� ���� ����
    /// </summary>
    /// <param name="index">0 ~ 3�� ������ ���� ���� ���</param>
    public void MonstroAtack(int index)
    {
        switch (index)
        {
            case 0:
                AudioManager.PlayOneShot(audioClips[(int)SFX.Monstro_Atk_1]);
                break;
            case 1:
                AudioManager.PlayOneShot(audioClips[(int)SFX.Monstro_Atk_2]);
                break;
            case 2:
                AudioManager.PlayOneShot(audioClips[(int)SFX.Monstro_Atk_3]);
                break;
            default:
                Debug.Log("�ε��� ���� �̻��մϴ�. MonstroAttack�� 0 ~ 3������ ���� �����ؾ��մϴ�.");
                break;
        }
    }

    /// <summary>
    /// �� ������ ����
    /// </summary>
    public void DoorOpen()
    {
        AudioManager.PlayOneShot(audioClips[(int)SFX.DoorOpen]);
    }

    /// <summary>
    /// �� ������ ����
    /// </summary>
    public void DoorClose()
    {
        AudioManager.PlayOneShot(audioClips[(int)SFX.DoorClose]);
    }

    /// <summary>
    /// �ĸ� ����
    /// </summary>
    public void FlySwam()
    {
        AudioManager.PlayOneShot(audioClips[(int)SFX.FlySwam]);
    }

    /// <summary>
    /// ��ź ȹ�� ����
    /// </summary>
    public void GetBoom()
    {
        AudioManager.PlayOneShot(audioClips[(int)SFX.GetBoom]);
    }

    /// <summary>
    /// ���� ������� ����
    /// </summary>
    public void BoosStomp()
    {
        AudioManager.PlayOneShot(audioClips[(int)SFX.BoosStomp]);
    }

    /// <summary>
    /// Chubber ���� ����
    /// </summary>``
    public void ChubberAttack()
    {
        AudioManager.PlayOneShot(audioClips[(int)SFX.Chubber_Atk]);
    }

    /// <summary>
    /// Charger ���� ����
    /// </summary>
    public void ChargerAttack()
    {
        AudioManager.PlayOneShot(audioClips[(int)SFX.Charger_Atk]);
    }

    /// <summary>
    /// Nest�� ���ٽ� ������ ����
    /// </summary>
    public void NestRun()
    {
        AudioManager.PlayOneShot(audioClips[(int)SFX.NestRun]);
    }

    /// <summary>
    /// �۷κ� ��ȯ ����
    /// </summary>
    public void GloBinSpawn()
    {
        AudioManager.PlayOneShot(audioClips[(int)SFX.GloBinReSpawn]);
    }

    /// <summary>
    /// �ο� ���� ����
    /// </summary>
    public void LowJump()
    {
        AudioManager.PlayOneShot(audioClips[(int)SFX.LowJump]);
    }

}
