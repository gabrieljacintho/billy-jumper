using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonsManager : MonoBehaviour
{
    public static ButtonsManager instance;

    [Header("HomeButtons")]
    [SerializeField]
    private Button homeButton;
    [SerializeField]
    private GameObject homeConfirmationButtonObject;
    [SerializeField]
    private GameObject homeCancelButtonObject;

    [Header("RestartButtons")]
    [SerializeField]
    private GameObject restartConfirmationButtonObject;
    [SerializeField]
    private GameObject restartCancelButtonObject;

    [Header("MuteButton")]
    [SerializeField]
    private Image muteButtonImage;
    [SerializeField]
    private Sprite withAudioSprite;
    [SerializeField]
    private Sprite withoutAudioSprite;

    [Space]
    [SerializeField]
    private GameObject songButtonObject;

    [Space]
    public RectTransform enterButtonCanvasRectTransform;

    [Space]
    public Sprite[] levelTopSprites = new Sprite[3];


    private void Awake()
    {
        if (instance == null) instance = this;
    }

    public void PauseButton()
    {
        SongsManager.instance.PlayButtonClickAudioClip();

        UIManager.instance.SetActivePausePanel(true);
        UIManager.instance.SetActiveTaskPanel(false);
        UIManager.instance.SetActiveControllerPanel(false);

        Time.timeScale = 0;
    }

    public void ReturnButton()
    {
        SongsManager.instance.PlayButtonClickAudioClip();

        UIManager.instance.SetActivePausePanel(false);
        UIManager.instance.SetActiveTaskPanel(true);
        UIManager.instance.SetActiveControllerPanel(true);

        Time.timeScale = 1;
    }

    public void MuteButton()
    {
        SongsManager.instance.PlayButtonClickAudioClip();

        if (!SongsManager.instance.Mute())
        {
            muteButtonImage.sprite = withoutAudioSprite;
            songButtonObject.SetActive(false);
        }
        else
        {
            muteButtonImage.sprite = withAudioSprite;
            songButtonObject.SetActive(true);
        }
    }

    public void EnterButton()
    {
        SongsManager.instance.PlayButtonClickAudioClip();

        if (SceneManager.GetActiveScene().buildIndex == 0)
            StartCoroutine(GameManager.instance.LoadScene(GameManager.instance.sceneIndexSelected));
        else GameManager.instance.GameWins();
        SetActiveEnterButtonCanvas(false, Vector2.zero);
    }

    public void HomeButton()
    {
        SongsManager.instance.PlayButtonClickAudioClip();

        homeConfirmationButtonObject.SetActive(!homeConfirmationButtonObject.activeSelf);
        homeCancelButtonObject.SetActive(!homeCancelButtonObject.activeSelf);

        restartConfirmationButtonObject.SetActive(false);
        restartCancelButtonObject.SetActive(false);
    }

    public void HomeConfirmationButton(bool value)
    {
        SongsManager.instance.PlayButtonClickAudioClip();

        homeConfirmationButtonObject.SetActive(false);
        homeCancelButtonObject.SetActive(false);

        if (value) StartCoroutine(GameManager.instance.LoadScene(0));
    }

    public void RestartButton()
    {
        SongsManager.instance.PlayButtonClickAudioClip();

        restartConfirmationButtonObject.SetActive(!restartConfirmationButtonObject.activeSelf);
        restartCancelButtonObject.SetActive(!restartCancelButtonObject.activeSelf);

        homeConfirmationButtonObject.SetActive(false);
        homeCancelButtonObject.SetActive(false);
    }

    public void RestartConfirmationButton(bool value)
    {
        SongsManager.instance.PlayButtonClickAudioClip();

        restartConfirmationButtonObject.SetActive(false);
        restartCancelButtonObject.SetActive(false);

        if (value)
        {
            UIManager.instance.SetActiveControllerPanel(true);
            GameManager.instance.RestartLevel();
        }
    }

    public void NextLevelButton()
    {
        SongsManager.instance.PlayButtonClickAudioClip();

        if (SceneManager.sceneCountInBuildSettings > (SceneManager.GetActiveScene().buildIndex + 1))
            StartCoroutine(GameManager.instance.LoadScene(SceneManager.GetActiveScene().buildIndex + 1));
        else StartCoroutine(GameManager.instance.LoadScene(0));
    }

    public void SetActiveEnterButtonCanvas(bool value, Vector2 doorPosition)
    {
        enterButtonCanvasRectTransform.gameObject.SetActive(value);
        if (value) enterButtonCanvasRectTransform.anchoredPosition = new Vector2(doorPosition.x, doorPosition.y - 1.485f);
    }
}