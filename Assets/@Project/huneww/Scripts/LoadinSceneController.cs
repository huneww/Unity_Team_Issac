using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using System;
using System.IO;

public class LoadinSceneController : MonoBehaviour
{
    private AudioSource ad;

    // ���� ���� instance;
    private static LoadinSceneController instance;
    // instance ���� Ȯ���� �ν��Ͻ� ����
    public static LoadinSceneController Instance
    {
        get
        {
            if (instance == null)
            {
                var obj = FindObjectOfType<LoadinSceneController>();
                if (obj != null)
                {
                    instance = obj;
                }
                else
                {
                    instance = Create();
                }
            }

            return instance;
        }
    }

    // �ν�����â�� LoadingUI�� ���ٸ� �ν��Ͻ� ����
    private static LoadinSceneController Create()
    {
        return Instantiate(Resources.Load<LoadinSceneController>("LoadingUI"));
    }

    private void Awake()
    {
        // Instance�� �ٸ��� �� ������Ʈ�� �ı�
        if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        // ���� �ִ� Instance�� �ε��� �ı����� �ȵ��ϼ���
        DontDestroyOnLoad(gameObject);
    }

    // �ε������� ����� UI�׷�
    [SerializeField]
    private CanvasGroup canvasGroup;

    // �ε��� �̸� ���� ����
    private string loadSceneName;
        
    /// <summary>
    /// �ε��� ���� �޼���
    /// </summary>
    /// <param name="sceneName">�ҷ��� ���� �̸�</param>
    public void LoadScene(string sceneName)
    {
        // Activ�� true�� ����
        gameObject.SetActive(true);

        // �� �ε��� ������ OnSceneLoaded�� �ݻ��Լ��� ����
        SceneManager.sceneLoaded += OnSceneLoaded;

        // �ҷ��� �� �̸��� ����
        loadSceneName = sceneName;

        // ����� ���۳�Ʈ �޾ƿ���
        ad = GetComponent<AudioSource>();

        // ���� ����
        ad.volume = VolumeSaveLoad.BGMLoad();

        // �ε��� ���� ����
        if (loadSceneName != "MainMenu")
            ad.Play();

        // ���̵� �� �ε��� ����
        StartCoroutine(LoadSceneProcess());
    }

    private IEnumerator LoadSceneProcess()
    {
        // ���̵� �� ������ �ڵ����
        yield return StartCoroutine(Fade(true));

        // �ε����� �񵿱������ ��� �ε�
        var op = SceneManager.LoadSceneAsync(loadSceneName);
        
        // �ε��� ���� 90%���� ����
        op.allowSceneActivation = false;

        // �ε����� ���εƴٸ� ������ 10% �ε�
        if (op.progress < 0.9f)
        {
            op.allowSceneActivation = true;
        }
    }

    // ���̵� ��,�ƿ� �޼���
    private IEnumerator Fade(bool isFadeIn)
    {
        // ���̵� �ð� ����
        float time = 0f;

        while (time <= 2f)
        {
            yield return null;
            // time������ ��� �ð� ���ϱ�
            time += Time.unscaledDeltaTime;

            // FadeIn�� true�� ���̵� ��
            // FadeIn�� false�� ���̵� �ƿ�
            canvasGroup.alpha = isFadeIn ? Mathf.Lerp(0f, 1f, time) : Mathf.Lerp(1f, 0f, time);
        }

        // FadeIn�� false��� UI Active false�� ����
        if (!isFadeIn)
        {
            gameObject.SetActive(false);
        }
    }

    // �ε��� ���� �Ϸ�Ǹ� ����� �ݹ� �Լ�
    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        // �ҷ��� ���� �ҷ��÷����� ���� �̸��� ���ٸ�
        if (arg0.name == loadSceneName)
        {
            // ���̵� �ƿ� �ڷ�ƾ ����
            StartCoroutine(Fade(false));

            // �ݹ� �Լ� ����
            SceneManager.sceneLoaded -= OnSceneLoaded;

            // �����ð��� UI ����
            Destroy(gameObject, 1f);
        }
    }
}
