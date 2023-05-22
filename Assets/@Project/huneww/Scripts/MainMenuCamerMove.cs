using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuCamerMove : MonoBehaviour
{
    // ī�޶� ������ �ӵ�
    public float MoveSpeed;

    // ���� Ÿ��Ʋȭ�鿡 �ִ��� Ȯ�� ����
    private bool isTitle = true;

    // ���� ���Ӹ޴��� �ִ��� Ȯ�� ����
    private bool isGameMenu = false;
    // ���� �޴� ������ ��ġ ����
    private int Gamepointer = 0;
    // ���� �޴� ������ ��������Ʈ
    public GameObject[] GamePointerSprite;

    // ���� �ɼǿ� �ִ��� Ȯ�� ����
    private bool isOption = false;
    // �ɼ� ������ ��ġ ����
    private int Optionpointer = 0;
    // �ɼ� ������ ��������Ʈ
    public GameObject[] OptionPointerSprite;

    // ���� ĳ���� ����â�� �ִ��� Ȯ�� ����
    private bool isCharacter = false;

    // ���� �޴� ��ġ ���� List
    private List<Vector3> MenuPos;
    // List �ε�����ȣ ����
    enum MenuPosition
    {
        TitleMenu,
        GameMenu,
        CharacterMenu,
        OptionMenu
    }

    private void Start()
    {

        // list �ʱ�ȭ
        MenuPos = new List<Vector3>();

        // list�� ��ġ ���� �߰�
        MenuPos.Add(new Vector3(0, 0, -10));      // Ÿ��Ʋ
        MenuPos.Add(new Vector3(0, -10, -10));    // ���� �޴�
        MenuPos.Add(new Vector3(0 - 20, -10));     // ĳ���� ����â
        MenuPos.Add(new Vector3(15, -10, -10));   // �ɼ�

        isTitle = true;
        isGameMenu = false;
        isOption = false;
        isCharacter = false;
    }

    private void Update()
    {
        // ���� �޴����� ī�޶� �̵� �޼���
        CamerMove();
    }

    private void CamerMove()
    {

        // Ÿ��Ʋ ȭ���� ��
        if (isTitle)
        {
            InTitle();
        }

        // ���� �޴��� ��
        else if (isGameMenu)
        {
            InGameMenu();
        }

        // �ɼ��� ��
        else if (isOption)
        {
            InOption();
        }

        // ĳ���� ����â�� ��
        else if (isCharacter)
        {
            InCharacter();
        }
    }

    private void InTitle()
    {

        // esc Ŭ���� ���� ����
        if (Input.GetButtonDown("esc"))
        {
            Debug.Log("���� ����");
            isTitle = !isTitle;
            Application.Quit();
        }

        // esc�� ������ Ű�� �Է��ϸ�
        else if (!Input.GetButtonDown("esc") && Input.inputString != "")
        {
            // isTitle �� ����
            isTitle = !isTitle;
            // isGameMenu �� ����
            isGameMenu = !isGameMenu;
            // �� �����Ӹ��� �����ϴ� �ڷ�ƾ �޼���
            StartCoroutine(TitleToGameMenu());
            Debug.Log("���� �޴��� �̵�");

            // ���� �Ŵ����� ���»��� �÷���
            SoundManager.OpenSound();
        }
    }

    private void InGameMenu()
    {
        // ���� �޴����� ESCŰ�� ������
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // isGameMenu �� ����
            isGameMenu = !isGameMenu;
            // isTitle �� ����
            isTitle = !isTitle;
            // �� �����Ӹ��� �����ϴ� �ڷ�ƾ �޼���
            StartCoroutine(GameMenuToTitle());

            Debug.Log("Ÿ��Ʋ ȭ������ �̵�");

            // ���� �Ŵ����� Ŭ������� �÷���
            SoundManager.CloseSound();
        }

        // ����Ű �Է�
        if (Input.GetButtonDown("UseBoom"))
        {
            if (Gamepointer == 0)
            {
                StartCoroutine(GameMenuToCharacter());
                Debug.Log("ĳ���� ����â���� �̵�");

                // ���� �Ŵ����� ���»��� �÷���
                SoundManager.OpenSound();

                // isGameMenu �� ����
                isGameMenu = !isGameMenu;
                // isCharacter �� ����
                isCharacter = !isCharacter;
            }
            else if (Gamepointer == 1)
            {
                StartCoroutine(GameMenuToOption());
                Debug.Log("�ɼ����� �̵�");

                // ���� �Ŵ����� ���»��� �÷���
                SoundManager.OpenSound();

                // isGameMenu �� ����
                isGameMenu = !isGameMenu;
                // isOption ��  ����
                isOption = !isOption;
            }

            // ���� ���� �÷���
            SoundManager.OpenSound();
        }

        // �� ���� Ŭ��
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Gamepointer++;
            if (Gamepointer > 1) Gamepointer = 0;

            // ���� �̵� ���� �÷���
            SoundManager.PoopSound();
        }
        // �Ʒ� ���� Ŭ��
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Gamepointer--;
            if (Gamepointer < 0) Gamepointer = 1;

            // ���� �̵� ���� �÷���
            SoundManager.PoopSound();
        }

        // GamePointerSprite�� SetActive ����
        GamePointerSprite[Gamepointer].SetActive(true);
        GamePointerSprite[Gamepointer == 0 ? 1 : 0].SetActive(false);
    }

    private void InOption()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // isGameMenu �� ����
            isGameMenu = !isGameMenu;
            // isOption ��  ����
            isOption = !isOption;

            // �� �����Ӹ��� �����ϴ� �ڷ�ƾ �޼���
            StartCoroutine(OptionToGameMenu());

            Debug.Log("���� �޴��� �̵�");

            // ���� �Ŵ����� Ŭ������� �÷���
            SoundManager.CloseSound();
        }

        // �� ���� Ŭ��
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Optionpointer++;
            if (Optionpointer > 1) Optionpointer = 0;

            SoundManager.PoopSound();
        }
        // �Ʒ� ���� Ŭ��
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Optionpointer--;
            if (Optionpointer < 0) Optionpointer = 1;

            SoundManager.PoopSound();
        }

        // OptionPointerSprite�� SetActive ����
        OptionPointerSprite[Optionpointer].SetActive(true);
        OptionPointerSprite[Optionpointer == 0 ? 1 : 0].SetActive(false);

        // ������ ���� Ŭ��
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            // BGM �϶�
            if (Optionpointer == 0)
            {
                // BGM ���� ���� �޼��� ȣ��
                SoundManager.BGMUp();

                // bar ��������Ʈ ����
                SoundManager.BgmBar();
            }
            // SFX �϶�
            else if (Optionpointer == 1)
            {
                // SFX ���� ���� �޼��� ȣ��
                SoundManager.SFXUp();

                // bar ��������Ʈ ����
                SoundManager.SfxBar();
            }

            // ������ ������ ���� ���
            SoundManager.PoopSound();
        }
        // ���� ���� Ŭ��
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            // BGM �϶�
            if (Optionpointer == 0)
            {
                // BGM ���� ���� �޼��� ȣ��
                SoundManager.BGMDown();

                // bar ��������Ʈ ����
                SoundManager.BgmBar();
            }
            // SFX �϶�
            else if (Optionpointer == 1)
            {
                // SFX ���� ���� �޼��� ȣ��
                SoundManager.SFXDown();

                // bar ��������Ʈ ����
                SoundManager.SfxBar();
            }
            // ������ ������ ���� ���
            SoundManager.PoopSound();
        }
    }

    private void InCharacter()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // isGameMenu �� ����
            isGameMenu = !isGameMenu;
            // isOption ��  ����
            isCharacter = !isCharacter;

            // �� �����Ӹ��� �����ϴ� �ڷ�ƾ �޼���
            StartCoroutine(CharacterToGameMenu());

            Debug.Log("���� �޴��� �̵�");

            // ���� �Ŵ����� Ŭ������� �÷���
            SoundManager.CloseSound();
        }

        else if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("���� ����");

            // BGM ���� ����
            SoundManager.BgmStop();

            // �ε� UI ����
            LoadinSceneController.Instance.LoadScene("Basement");
        }
    }

    /// <summary>
    /// Ÿ��Ʋ ȭ�鿡�� ���� �޴� ȭ������ ī�޶� �̵�
    /// </summary>
    /// <returns></returns>
    private IEnumerator TitleToGameMenu()
    {   
        // ���� ī�޶� ��ġ�� GameMenuPos���� ũ��
        while (transform.position.y > MenuPos[(int)MenuPosition.GameMenu].y)
        {
            // ī�޶� ���� ��ġ
            var curpos = transform.position;
            // ī�޶� �̵� ��ġ
            var nextpos = Vector3.down * MoveSpeed * Time.deltaTime;

            // ī�޶� ��ġ ����
            transform.position = curpos + nextpos;

            // �� �����Ӹ��� �ڷ�ƾ ����
            yield return null;
        }

        if (transform.position.y < MenuPos[(int)MenuPosition.GameMenu].y)
            transform.position = MenuPos[(int)MenuPosition.GameMenu];

        // while���� ������ �ڷ�ƾ ����
        yield break;
    }

    /// <summary>
    /// ���� �޴����� Ÿ��Ʋ ȭ������ ī�޶� �̵�
    /// </summary>
    /// <returns></returns>
    private IEnumerator GameMenuToTitle()
    {
        while (transform.position.y < MenuPos[(int)MenuPosition.TitleMenu].y)
        {
            // ī�޶� ���� ��ġ
            var curpos = transform.position;
            // ī�޶� �̵� ��ġ
            var nextpos = Vector3.up * MoveSpeed * Time.deltaTime;

            // ī�޶� ��ġ ����
            transform.position = curpos + nextpos;

            // �� �����Ӹ��� �ڷ�ƾ ����
            yield return null;
        }

        if (transform.position.y > MenuPos[(int)MenuPosition.TitleMenu].y)
            transform.position = MenuPos[(int)MenuPosition.TitleMenu];

        // while���� ������ �ڷ�ƾ ����
        yield break;
    }

    /// <summary>
    /// ���� �޴����� �ɼ����� ī�޶� �̵�
    /// </summary>
    /// <returns></returns>
    private IEnumerator GameMenuToOption()
    {
        while (transform.position.x < MenuPos[(int)MenuPosition.OptionMenu].x)
        {
            // ī�޶� ���� ��ġ
            var curpos = transform.position;
            // ī�޶� �̵� ��ġ
            var nextpos = Vector3.right * MoveSpeed * Time.deltaTime;

            // ī�޶� ��ġ ����
            transform.position = curpos + nextpos;

            // �� �����Ӹ��� �ڷ�ƾ ����
            yield return null;
        }

        if (transform.position.x > MenuPos[(int)MenuPosition.OptionMenu].x)
            transform.position = MenuPos[(int)MenuPosition.OptionMenu];

        // while���� ������ �ڷ�ƾ ����
        yield break;
    }

    /// <summary>
    /// �ɼǿ��� ���� �޴��� ī�޶� �̵�
    /// </summary>
    /// <returns></returns>
    private IEnumerator OptionToGameMenu()
    {
        while (transform.position.x > MenuPos[(int)MenuPosition.GameMenu].x)
        {
            // ī�޶� ���� ��ġ
            var curpos = transform.position;
            // ī�޶� �̵� ��ġ
            var nextpos = Vector3.left * MoveSpeed * Time.deltaTime;

            // ī�޶� ��ġ ����
            transform.position = curpos + nextpos;

            // �� �����Ӹ��� �ڷ�ƾ ����
            yield return null;
        }

        if (transform.position.x < MenuPos[(int)MenuPosition.GameMenu].x)
            transform.position = MenuPos[(int)MenuPosition.GameMenu];

        // while���� ������ �ڷ�ƾ ����
        yield break;
    }

    /// <summary>
    /// ���� �޴����� ĳ���� ����â���� ī�޶� �̵�
    /// </summary>
    /// <returns></returns>
    private IEnumerator GameMenuToCharacter()
    {
        while (transform.position.y > -20.0f)
        {
            // ī�޶� ���� ��ġ
            var curpos = transform.position;
            // ī�޶� �̵� ��ġ
            var nextpos = Vector3.down * MoveSpeed * Time.deltaTime;

            // ī�޶� ��ġ ����
            transform.position = curpos + nextpos;

            // �� �����Ӹ��� �ڷ�ƾ ����
            yield return null;
        }

        if (transform.position.y < -20.0f)
            transform.position = new Vector3(0, -20, -10);

        // while���� ������ �ڷ�ƾ ����
        yield break;
    }

    /// <summary>
    /// ĳ���� ����â���� ���� �޴��� �̵�
    /// </summary>
    /// <returns></returns>
    private IEnumerator CharacterToGameMenu()
    {
        while (transform.position.y < MenuPos[(int)MenuPosition.GameMenu].y)
        {
            // ī�޶� ���� ��ġ
            var curpos = transform.position;
            // ī�޶� �̵� ��ġ
            var nextpos = Vector3.up * MoveSpeed * Time.deltaTime;

            // ī�޶� ��ġ ����
            transform.position = curpos + nextpos;

            // �� �����Ӹ��� �ڷ�ƾ ����
            yield return null;
        }

        if (transform.position.y > MenuPos[(int)MenuPosition.GameMenu].y)
            transform.position = MenuPos[(int)MenuPosition.GameMenu];

        // while���� ������ �ڷ�ƾ ����
        yield break;
    }

}