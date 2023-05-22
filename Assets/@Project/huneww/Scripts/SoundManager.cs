using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class SoundManager : MonoBehaviour
{
    // SFX ���� �Ŵ���
    public AudioSource SFXPlayer;
    // BGM ���� �Ŵ���
    public AudioSource BGMPlayer;

    // SFX ���� Ŭ��
    public AudioClip Open;
    public AudioClip Close;
    // �ɼǿ��� ���� ������ ���� ����
    public AudioClip Poop;

    // �ٸ� ��ũ��Ʈ���� �޼��� ������ ���� ����
    public static Action OpenSound;
    public static Action CloseSound;
    public static Action PoopSound;
    public static Action BGMUp;
    public static Action BGMDown;
    public static Action SFXUp;
    public static Action SFXDown;
    public static Action BgmBar;
    public static Action SfxBar;
    public static Action BgmStop;

    // ���� BGM ũ��
    public static int bgmvolume;

    // ���� SFX ũ��
    public static int sfxvolume;

    // BGMbar ��������Ʈ
    public GameObject[] BGMBar;

    // SFXbar ��������Ʈ
    public GameObject[] SFXBar;

    private void Awake()
    {
        // �ٸ� ��ũ��Ʈ���� �޼��� ������ ���� ���ٽ�
        OpenSound = () => { PlayOpen(); };
        CloseSound = () => { PlayClose(); };
        PoopSound = () => { PlayPoop(); };
        BGMUp = () => { BGMVolumeUp(); };
        BGMDown = () => { BGMVolumeDown(); };
        BgmBar = () => { BGMBAR(); };

        SFXUp = () => {  SFXVolumeUp(); };
        SFXDown = () => { SFXVolumeDown(); };
        SfxBar = () => { SFXBAR(); };

        BgmStop = () => { BGMStop(); };

        // BGM ���� �Ŵ��� ���� ����
        BGMPlayer.volume = VolumeSaveLoad.BGMLoad();
        // bgm ���� ����
        bgmvolume = (int)(BGMPlayer.volume * 100);
        // ��������Ʈ Active ����
        BGMBAR();

        // SFX ���� �Ŵ��� ���� ����
        SFXPlayer.volume = VolumeSaveLoad.SFXLoad();
        // sfx ���� ����
        sfxvolume = (int)(SFXPlayer.volume * 10);

        // ��������Ʈ Active ����
        SFXBAR();
    }

    // � �޴��� ���� ���� �Ҹ�
    private void PlayOpen()
    {
        SFXPlayer.PlayOneShot(Open);
    }

    // � �޴����� ���ö� ���� �Ҹ�
    private void PlayClose()
    {
        SFXPlayer.PlayOneShot(Close);
    }

    // ���� ������ ������ �Ҹ�
    private void PlayPoop()
    {
        SFXPlayer.PlayOneShot(Poop);
    }

    // BGM ���� ����
    private void BGMVolumeUp()
    {
        bgmvolume++;

        if (bgmvolume > 10)
            bgmvolume = 10;

        BGMPlayer.volume += 0.01f;

        if (BGMPlayer.volume > 0.1f)
            BGMPlayer.volume = 0.1f;

        BGMBAR();

        // ���� ������ ����
        VolumeSaveLoad.BGMSave(BGMPlayer.volume);
    }

    // BGM ���� ����
    private void BGMVolumeDown()
    {
        bgmvolume--;

        if (bgmvolume < 0)
            bgmvolume = 0;

        BGMPlayer.volume -= 0.01f;

        if (BGMPlayer.volume < 0)
            BGMPlayer.volume = 0;

        BGMBAR();

        // ���� ������ ����
        VolumeSaveLoad.BGMSave(BGMPlayer.volume);
    }

    // BAR ��������Ʈ ����
    private void BGMBAR()
    {
        foreach (var bar in BGMBar)
        {
            bar.SetActive(false);
        }

        BGMBar[bgmvolume].SetActive(true);

        Debug.Log(BGMPlayer.volume);
    }

    // SFX ���� ����
    private void SFXVolumeUp()
    {
        sfxvolume++;

        if (sfxvolume > 10)
            sfxvolume = 10;

        SFXPlayer.volume += 0.1f;

        if (SFXPlayer.volume > 1f)
            SFXPlayer.volume = 1f;

        SFXBAR();

        // ���� ������ ����
        VolumeSaveLoad.SFXSave(SFXPlayer.volume);
    }

    // SFX ���� ����
    private void SFXVolumeDown()
    {
        sfxvolume--;

        if (sfxvolume < 0)
            sfxvolume = 0;

        SFXPlayer.volume -= 0.1f;

        if (SFXPlayer.volume < 0)
            SFXPlayer.volume = 0f;

        SFXBAR();

        // ���� ������ ����
        VolumeSaveLoad.SFXSave(SFXPlayer.volume);
    }

    // BAR ��������Ʈ ����
    private void SFXBAR()
    {
        foreach (var bar in SFXBar)
        {
            bar.SetActive(false);
        }
        SFXBar[sfxvolume].SetActive(true);

        Debug.Log(SFXPlayer.volume);
    }

    // BGM ����
    private void BGMStop()
    {
        BGMPlayer.Stop();
    }

}
