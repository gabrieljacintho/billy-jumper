using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaterController : MonoBehaviour
{
    private float currentWaterYPosition = -8.5f;

    [SerializeField]
    private List<Animator> animatorsList = new List<Animator>();

    private void Update()
    {
        transform.position = new Vector2(GameManager.instance.mainCamera.transform.position.x, currentWaterYPosition);
    }

    public void UpdateWaterYPosition()
    {
        currentWaterYPosition = LevelManager.instance.GetLevelWaterYPosition(SceneManager.GetActiveScene().buildIndex);
    }

    public void UpdateSpeed(float speed)
    {
        foreach(Animator animator in animatorsList)
        {
            animator.SetFloat("Speed", speed);
        }
    }
}
