using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGamePaused : MonoBehaviour
{
    // ������ �̹���
    public GameObject[] pointers;

    // ���� �޴� �������� Ȯ��
    public static bool isPause = false;

    [SerializeField]
    private CanvasGroup group;

    // ������ �ε���
    private int index = 0;

    private void Update()
    {
        if (Input.GetButtonDown("esc"))
        {
            if (!isPause)
            {
                InGameSFXManager.instance.PauseMenuOpen();
                Open();
            }
            else
            {
                InGameSFXManager.instance.PauseMenuClose();
                Close();
            }
        }

        if (isPause)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                InGameSFXManager.instance.Poop();
                pointers[index].SetActive(false);
                index++;
                if (index > 1) index = 0;
                pointers[index].SetActive(true);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                InGameSFXManager.instance.Poop();
                pointers[index].SetActive(false);
                index--;
                if (index < 0) index = 1;
                pointers[index].SetActive(true);
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                Close();
                if (index == 0)
                {
                    InGameSFXManager.instance.PauseMenuClose();
                }
                else if (index == 1)
                {
                    var player = FindObjectOfType<Player>();
                    player.Dead();
                    var gmanager = FindObjectOfType<GameManager>();
                    gmanager.Destroy();
                    Destroy(gameObject);
                    LoadinSceneController.Instance.LoadScene("MainMenu");
                }
            }
        }
    }

    /// <summary>
    /// ���� �޴� ����
    /// </summary>
    public void Open()
    {
        // ���� ����
        InGameSFXManager.instance.PauseMenuOpen();

        // �ð� ����
        Time.timeScale = 0f;
        isPause = !isPause;
        group.alpha = 1f;
    }

    /// <summary>
    /// ���� �޴� �ݱ�
    /// </summary>
    public void Close()
    {
        // Ŭ���� ����
        InGameSFXManager.instance.PauseMenuClose();

        // �ð� Ȱ��ȭ
        Time.timeScale = 1.0f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
        isPause = !isPause;
        group.alpha = 0f;
    }
}
