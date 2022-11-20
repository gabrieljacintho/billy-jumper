using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TaskManager : MonoBehaviour
{
    public static TaskManager instance;

    [Header("TaskImages")]
    [SerializeField]
    private Image timerTaskImage;
    [SerializeField]
    private Image coinTaskImage;
    
    private float targetCoinFillAmount;
    private float coinsNumber;

    [Header("StarImages")]
    [SerializeField]
    private Image[] starImages = new Image[3];
    [SerializeField]
    private Image[] gameWinStarImages = new Image[3];

    private int currentBuildIndex;
    private float sceneTime;
    private float time;

    private float t0 = 0;
    private float t1 = 0;
    private float t2 = 0;

    private List<GameObject> coinObjects = new List<GameObject>();
    private int pendingCollectedCoins = 0;

    [NonSerialized]
    public int pendingStars = 2;
    private bool once = false;
    private bool roundStarted;

    [Space]
    [SerializeField]
    private string[] coinTags = new string[4];
#if UNITY_EDITOR
    [NonSerialized]
    public float startTime = 0.0f;
#endif


    private void Awake()
    {
        if (instance == null) instance = this;
    }

    private void Update()
    {
        if (currentBuildIndex != 0 && roundStarted)
        {
            timerTaskImage.fillAmount = 1 / sceneTime * Mathf.Clamp(time - Time.time, 0, Mathf.Infinity);

            if (timerTaskImage.fillAmount < 0.2f && t0 < 1)
            {
                if (!once)
                {
                    pendingStars--;
                    once = true;
                }

                starImages[0].color = Color.Lerp(Color.white, Color.gray, Mathf.Lerp(0, 1, t0));
                t0 += 2 * Time.deltaTime;

                if (t0 >= 1) once = false;
            }
            else if (timerTaskImage.fillAmount < 0.6f && t1 < 1)
            {
                if (!once)
                {
                    pendingStars--;
                    once = true;
                }

                starImages[1].color = Color.Lerp(Color.white, Color.gray, Mathf.Lerp(0, 1, t1));
                t1 += 2 * Time.deltaTime;

                if (t1 >= 1) once = false;
            }

            if (coinTaskImage.fillAmount != targetCoinFillAmount) coinTaskImage.fillAmount = Mathf.Lerp(coinTaskImage.fillAmount, targetCoinFillAmount, 2 * Time.deltaTime);

            if (coinTaskImage.fillAmount > 0.6f && t2 < 1)
            {
                if (!once)
                {
                    pendingStars++;
                    once = true;
                }

                starImages[2].color = Color.Lerp(Color.gray, Color.white, Mathf.Lerp(0, 1, t2));
                t2 += 2 * Time.deltaTime;

                if (t2 >= 1) once = false;
            }
        }
    }

    public void LoadGameWinStars()
    {
        foreach (Image gameWinStarImage in gameWinStarImages)
        {
            if (pendingStars > 0)
            {
                gameWinStarImage.color = Color.white;
                pendingStars--;
            }
        }
    }

    public void LoadTasks()
    {
        currentBuildIndex = SceneManager.GetActiveScene().buildIndex;
        coinObjects.Clear();
        coinsNumber = 0;
        if (currentBuildIndex != 0)
        {
            foreach (string coinTag in coinTags)
            {
                GameObject[] coinObjectsInScene = GameObject.FindGameObjectsWithTag(coinTag);
                foreach (GameObject coinObject in coinObjectsInScene)
                {
                    if (coinTag == "Coin") coinsNumber++;
                    else if (coinTag == "Chest") coinsNumber += 5;
                    else if (coinTag == "BigChest") coinsNumber += 10;
                    else if (coinTag == "ToyChest") coinsNumber += 20;
                    coinObjects.Add(coinObject);
                }
            }

            sceneTime = LevelManager.instance.GetLevelTime(currentBuildIndex);
        }
    }

    public void RestartTasks()
    {
        if (currentBuildIndex != 0)
        {
#if UNITY_EDITOR
            startTime = Time.time;
#endif
            roundStarted = false;
            targetCoinFillAmount = 0;
            timerTaskImage.fillAmount = 1;
            coinTaskImage.fillAmount = 0;
            pendingCollectedCoins = 0;
            pendingStars = 2;
            once = false;
            starImages[0].color = Color.white;
            starImages[1].color = Color.white;
            starImages[2].color = Color.gray;
            gameWinStarImages[0].color = Color.gray;
            gameWinStarImages[1].color = Color.gray;
            gameWinStarImages[2].color = Color.gray;
            t0 = 0;
            t1 = 0;
            t2 = 0;
            EnableCoinObjects();
        }
    }

    public bool GetRoundStarted()
    {
        return roundStarted;
    }

    public void SetRoundStarted(bool value)
    {
        roundStarted = value;
        time = Time.time + sceneTime;
    }

    public void AddCoin()
    {
        AddCoinsTask(1);
    }

    public void AddChest()
    {
        AddCoinsTask(5);
    }

    public void AddBigChest()
    {
        AddCoinsTask(10);
    }

    public void AddToyChest()
    {
        AddCoinsTask(20);
    }

    private void AddCoinsTask(int amount)
    {
        SongsManager.instance.PlayCollectedCoinAudioClip();
        pendingCollectedCoins += amount;
        targetCoinFillAmount = 1.0f / coinsNumber * pendingCollectedCoins;
    }

    private void EnableCoinObjects()
    {
        foreach (GameObject coinObject in coinObjects)
        {
            coinObject.SetActive(true);
        }
    }
}
