using System;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    [Serializable]
    private class Level
    {
        [NonSerialized]
        public int buildIndex = 1;
        [NonSerialized]
        public bool unlocked = false;
        [NonSerialized]
        public int stars = 0;
        public float time = 230.0f;
        public float waterYPosition = -8.5f;
    }
    
    [SerializeField]
    private Level[] levels = new Level[30];

    [Space]
    [SerializeField]
    private float extraTime = 15.0f;


    private void Awake()
    {
        if (instance == null) instance = this;
        LoadLevels();
    }

    private void LoadLevels()
    {
        int currentBuildIndex = 0;

        foreach (Level level in levels)
        {
            currentBuildIndex++;
            level.buildIndex = currentBuildIndex;
            
            level.time += extraTime;

            if (level.buildIndex == 1) level.unlocked = true;
            else level.unlocked = bool.Parse(PlayerPrefs.GetString("Level" + level.buildIndex.ToString() + "Unlocked", "false"));

            level.stars = PlayerPrefs.GetInt("Level" + level.buildIndex.ToString() + "Stars", 0);
        }
    }

    public void SaveLevelStars(int buildIndex, int stars)
    {
        foreach (Level level in levels)
        {
            if (level.buildIndex == buildIndex)
            {
                if (PlayerPrefs.GetInt("Level" + level.buildIndex.ToString() + "Stars") < stars)
                {
                    PlayerPrefs.SetInt("Level" + level.buildIndex.ToString() + "Stars", stars);
                    level.stars = PlayerPrefs.GetInt("Level" + level.buildIndex.ToString() + "Stars", stars);
                }
            }
            else if (level.buildIndex == (buildIndex + 1) && !level.unlocked)
            {
                PlayerPrefs.SetString("Level" + level.buildIndex.ToString() + "Unlocked", "true");
                level.unlocked = bool.Parse(PlayerPrefs.GetString("Level" + level.buildIndex.ToString() + "Unlocked", "true"));
            }
        }

        PlayerPrefs.Save();
    }

    public bool GetLevelUnlocked(int buildIndex)
    {
        foreach (Level level in levels)
        {
            if (level.buildIndex == buildIndex) return level.unlocked;
        }

        return false;
    }

    public int GetLevelStars(int buildIndex)
    {
        foreach (Level level in levels)
        {
            if (level.buildIndex == buildIndex) return level.stars;
        }

        return 0;
    }

    public float GetLevelTime(int buildIndex)
    {
        foreach (Level level in levels)
        {
            if (level.buildIndex == buildIndex) return level.time;
        }

        return 0.0f;
    }

    public float GetLevelWaterYPosition(int buildIndex)
    {
        foreach (Level level in levels)
        {
            if (level.buildIndex == buildIndex) return level.waterYPosition;
        }

        return -5.5f;
    }
}
