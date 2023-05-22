using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class VolumeData
{
    public float BGM;
    public float SFX;
}

public class VolumeSaveLoad : MonoBehaviour
{
    private static VolumeData VD;

    private void Awake()
    {
        VD = new VolumeData();
    }

    /// <summary>
    /// BGM������ json���Ϸ� ����
    /// </summary>
    /// <param name="bgmvolume">������ BGM ����</param>
    public static void BGMSave(float bgmvolume)
    {
        // �μ��� ����
        VD.BGM = bgmvolume;

        // Json���Ͽ� �ؽ�Ʈ�� ����
        File.WriteAllText(Application.dataPath + "/Resources/JsonData/VolumeData.json", JsonUtility.ToJson(VD));
    }

    /// <summary>
    /// BGM������ Json���Ͽ��� �о�� ��ȯ
    /// </summary>
    /// <returns>���� BGM ����</returns>
    public static float BGMLoad()
    {
        // Json ���� �о����
        string str = File.ReadAllText(Application.dataPath + "/Resources/JsonData/VolumeData.json");

        // VD�� �о�� Json���� �ؽ�Ʈ ����
        VD = JsonUtility.FromJson<VolumeData>(str);

        // ���� BGM ���� ��ȯ
        return VD.BGM;
    }

    /// <summary>
    /// SFX������ json���Ϸ� ����
    /// </summary>
    /// <param name="sfxvolume">������ SFX����</param>
    public static void SFXSave(float sfxvolume)
    {
        // �μ��� ����
        VD.SFX = sfxvolume;

        // Json���Ͽ� �ؽ�Ʈ�� ����
        File.WriteAllText(Application.dataPath + "/Resources/JsonData/VolumeData.json", JsonUtility.ToJson(VD));
    }

    /// <summary>
    /// SFX������ Json ���Ͽ��� �о�� ��ȯ
    /// </summary>
    /// <returns>���� SFX ����</returns>
    public static float SFXLoad()
    {
        // Json ���� �о����
        string str = File.ReadAllText(Application.dataPath + "/Resources/JsonData/VolumeData.json");

        // VD�� �о�� Json���� �� ����
        VD = JsonUtility.FromJson<VolumeData>(str);

        // ���� SFX ���� ��ȯ
        return VD.SFX;
    }
}
