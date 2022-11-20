using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DoorScript : MonoBehaviour
{
    private SpriteRenderer[] starSpriteRenderers = new SpriteRenderer[3];

    private int targetSceneIndex = 0;

    private bool unlocked = false;

    private void Start()
    {
        string levelDoorNumber;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (gameObject.name.Length > 5) levelDoorNumber = gameObject.name.Substring(5, gameObject.name.Length - 5);
        else levelDoorNumber = (currentSceneIndex + 1).ToString();

        targetSceneIndex = int.Parse(levelDoorNumber);
        Text textNumber = transform.GetChild(5).transform.GetChild(0).GetComponent<Text>();

        if (currentSceneIndex == 0) textNumber.text = levelDoorNumber.ToString();
        else textNumber.text = (currentSceneIndex + 1).ToString();

        if (GameManager.instance != null)
        {
            if (currentSceneIndex == 0) unlocked = LevelManager.instance.GetLevelUnlocked(targetSceneIndex);
            else
            {
                targetSceneIndex = currentSceneIndex + 1;
                unlocked = true;
            }

            if (unlocked) gameObject.transform.GetChild(1).gameObject.SetActive(false);

            if (LevelManager.instance.GetLevelUnlocked(targetSceneIndex + 1))
            {
                for (int star = 0; star < 3; star++)
                {
                    starSpriteRenderers[star] = gameObject.transform.GetChild(2 + star).gameObject.GetComponent<SpriteRenderer>();
                }

                int stars = LevelManager.instance.GetLevelStars(targetSceneIndex);

                GameObject top = gameObject.transform.GetChild(0).gameObject;
                top.SetActive(true);
                top.GetComponent<SpriteRenderer>().sprite = ButtonsManager.instance.levelTopSprites[Mathf.Clamp(stars - 1, 0, 2)];

                foreach (SpriteRenderer starSpriteRenderers in starSpriteRenderers)
                {
                    if (stars > 0)
                    {
                        starSpriteRenderers.color = Color.white;
                        stars--;
                    }
                    starSpriteRenderers.gameObject.SetActive(true);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameManager.instance != null && collision.CompareTag("Player") && unlocked)
        {
            GameManager.instance.sceneIndexSelected = targetSceneIndex;
            ButtonsManager.instance.SetActiveEnterButtonCanvas(true, gameObject.transform.position);
            if (SceneManager.GetActiveScene().buildIndex == 0) GameManager.instance.initialPosition = transform.position;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (GameManager.instance != null && collision.CompareTag("Player") && unlocked)
        {
            ButtonsManager.instance.SetActiveEnterButtonCanvas(false, Vector2.zero);
        }
    }
}
