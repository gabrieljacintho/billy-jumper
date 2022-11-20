using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Space]
    public Camera mainCamera;
    [NonSerialized]
    public float cameraWidth;

    [Header("Controllers")]
    public WaterController waterController;
    public PlayerController playerController;
    public CameraController cameraController;

    [NonSerialized]
    public bool loadingScene = false;
#if UNITY_EDITOR
    [Header("DevVariables")]
    public bool useJoystick = false;
    public bool devMode = false;
#endif
    [NonSerialized]
    public int sceneIndexSelected;
    [Space]
    public int previousSceneBuildIndex;

    [NonSerialized]
    public Vector2 initialPosition = new Vector2(-7, -0.65f);
    [NonSerialized]
    public Vector2 defaultInitialPosition = new Vector2(-7, -0.65f);

    [NonSerialized]
    public MaceSphereController maceSphereController;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
#if UNITY_EDITOR
            PlayerPrefs.DeleteAll();
#endif
        }
    }

    private void Start()
    {
#if !UNITY_EDITOR
        previousSceneBuildIndex = PlayerPrefs.GetInt("LastSceneBuildIndex", 0);
#endif
        cameraWidth = Vector3.Distance(mainCamera.ScreenToWorldPoint(new Vector2(mainCamera.pixelWidth, 0)), new Vector2(mainCamera.transform.position.x, 0)) * 2;
        StartCoroutine(LoadScene(previousSceneBuildIndex));
    }

    public void GameWins()
    {
#if UNITY_EDITOR
        print("Essa partida durou: " + (Time.time - TaskManager.instance.startTime) + " segundos");
#endif
        SongsManager.instance.PlayFinishedLevelAudioClip();

        Time.timeScale = 0;
        LevelManager.instance.SaveLevelStars(SceneManager.GetActiveScene().buildIndex, TaskManager.instance.pendingStars);
        TaskManager.instance.LoadGameWinStars();
        UIManager.instance.SetActiveTaskPanel(false);
        UIManager.instance.SetActiveGameWinPanel(true);
        UIManager.instance.SetActiveControllerPanel(false);
    }

    public void RestartLevel()
    {
        Time.timeScale = 1;
        LoadInitialPosition();
        if (maceSphereController != null) maceSphereController.ResetPosition();
        TaskManager.instance.RestartTasks();
        UIManager.instance.SetActivePausePanel(false);
        UIManager.instance.SetActiveGameWinPanel(false);
        UIManager.instance.SetActiveTaskPanel(true);
        AdsManager.instance.ShowAd();
    }

    private void LoadInitialPosition()
    {
        playerController.thisRigidbody2D.velocity = Vector2.zero;
        playerController.transform.position = initialPosition;
        cameraController.targetPosition = initialPosition;
    }

    public IEnumerator LoadScene(int sceneBuildIndex)
    {
        Time.timeScale = 1;

        previousSceneBuildIndex = SceneManager.GetActiveScene().buildIndex;
        bool load = previousSceneBuildIndex != sceneBuildIndex;

        if (sceneBuildIndex != 0) initialPosition = defaultInitialPosition;

        float timeLoading = Time.time + 1;
        AsyncOperation op = null;

        UIManager.instance.SetActiveControllerPanel(true);

        if (load)
        {
            loadingScene = true;

            UIManager.instance.SetActivePausePanel(false);
            UIManager.instance.SetActiveGameWinPanel(false);
            UIManager.instance.SetActiveLoadingObjects(true);

            UIManager.instance.ChangeBackgrounds(sceneBuildIndex);
            UIManager.instance.SetActiveHomeAndRestartButton(sceneBuildIndex != 0);

            op = SceneManager.LoadSceneAsync(sceneBuildIndex);
            op.allowSceneActivation = false;

            while (!op.isDone)
            {
                if (op.progress / 0.9f == 1) break;
                yield return new WaitForEndOfFrame();
            }
        }

        while (Time.time < timeLoading) yield return new WaitForEndOfFrame();

        if (load)
        {
            op.allowSceneActivation = true;
            yield return new WaitForEndOfFrame();
        }

        TaskManager.instance.LoadTasks();
        TaskManager.instance.RestartTasks();
        waterController.UpdateWaterYPosition();

        UIManager.instance.SetActiveTaskPanel(sceneBuildIndex != 0);
        UIManager.instance.SetActiveLoadingObjects(false);

        SongsManager.instance.PlayLoadedSceneAudioClip();

        if (load)
        {
            loadingScene = false;

            PlayerPrefs.SetInt("LastSceneBuildIndex", sceneBuildIndex);
            PlayerPrefs.Save();
        }

        if (sceneBuildIndex == 0) initialPosition = GameObject.Find("Level" + Mathf.Clamp(previousSceneBuildIndex, 1, Mathf.Infinity)).transform.position;
        LoadInitialPosition();
        cameraController.transform.position = initialPosition;
    }
}