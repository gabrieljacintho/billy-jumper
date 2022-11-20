using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header("LoadingObjects")]
    [SerializeField]
    private GameObject springLoadingBackgroudObject;
    [SerializeField]
    private GameObject autumnLoadingBackgroudObject;
    [SerializeField]
    private GameObject winterLoadingBackgroudObject;
    [SerializeField]
    private GameObject loadingImageObject;

    [Header("ForegroundCanvas")]
    [SerializeField]
    private GameObject controllerPanelObject = null;
    [SerializeField]
    private GameObject pausePanelObject = null;
    [SerializeField]
    private GameObject taskPanelObject;
    [SerializeField]
    private GameObject gameWinPanelObject;

    [Header("BackgroundCanvas")]
    [SerializeField]
    private GameObject springBackgroudObject;
    [SerializeField]
    private GameObject autumnBackgroudObject;
    [SerializeField]
    private GameObject winterBackgroudObject;

    [Header("Buttons")]
    [SerializeField]
    private GameObject homeButtonObject;
    [SerializeField]
    private GameObject restartButtonObject;


    private void Awake()
    {
        if (instance == null) instance = this;
    }

    public void SetActivePausePanel(bool value)
    {
        pausePanelObject.SetActive(value);
    }

    public void SetActiveControllerPanel(bool value)
    {
        controllerPanelObject.SetActive(value);
        GameManager.instance.playerController.toLeft = false;
        GameManager.instance.playerController.toRight = false;
        GameManager.instance.playerController.jump = false;
    }

    public void SetActiveTaskPanel(bool value)
    {
        taskPanelObject.SetActive(value && SceneManager.GetActiveScene().buildIndex != 0);
    }

    public void SetActiveGameWinPanel(bool value)
    {
        gameWinPanelObject.SetActive(value);
    }

    public void SetActiveLoadingObjects(bool value)
    {
        loadingImageObject.SetActive(value);

        if (!value)
        {
            if (springLoadingBackgroudObject.activeSelf) springLoadingBackgroudObject.SetActive(false);
            else if (autumnLoadingBackgroudObject.activeSelf) autumnLoadingBackgroudObject.SetActive(false);
            else if (winterLoadingBackgroudObject.activeSelf) winterLoadingBackgroudObject.SetActive(false);
        }
    }

    public void SetActiveHomeAndRestartButton(bool value)
    {
        if (homeButtonObject.activeSelf != value) homeButtonObject.SetActive(value);
        if (restartButtonObject.activeSelf != value) restartButtonObject.SetActive(value);
    }

    public void ChangeBackgrounds(int currentSceneBuildIndex)
    {
        if (springLoadingBackgroudObject.activeSelf) springLoadingBackgroudObject.SetActive(false);
        else if (autumnLoadingBackgroudObject.activeSelf) autumnLoadingBackgroudObject.SetActive(false);

        if (springBackgroudObject.activeSelf) springBackgroudObject.SetActive(false);
        else if (autumnBackgroudObject.activeSelf) autumnBackgroudObject.SetActive(false);

        if (currentSceneBuildIndex > 24)
        {
            winterBackgroudObject.SetActive(true);
            winterLoadingBackgroudObject.SetActive(true);
        }
        else
        {
            if (winterLoadingBackgroudObject.activeSelf) winterLoadingBackgroudObject.SetActive(false);
            if (winterBackgroudObject.activeSelf) winterBackgroudObject.SetActive(false);

            if (Random.Range(0, 2) == 0)
            {
                springLoadingBackgroudObject.SetActive(true);
                springBackgroudObject.SetActive(true);
            }
            else
            {
                autumnLoadingBackgroudObject.SetActive(true);
                autumnBackgroudObject.SetActive(true);
            }
        }
    }

}
