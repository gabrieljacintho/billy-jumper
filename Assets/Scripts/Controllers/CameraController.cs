using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector3 velocity = Vector3.zero;
    private GameObject playerObject;
    [NonSerialized]
    public Vector3 targetPosition;


    private void Start()
    {
        playerObject = GameManager.instance.playerController.gameObject;
        targetPosition = GameManager.instance.initialPosition;
    }

    private void Update()
    {
        if (playerObject.transform.position.x - gameObject.transform.position.x > 1.5f)
        {
            targetPosition = new Vector3(playerObject.transform.position.x - 1.5f, targetPosition.y, -10);
        }
        else if (gameObject.transform.position.x - playerObject.transform.position.x > 1.5f)
        {
            targetPosition = new Vector3(playerObject.transform.position.x + 1.5f, targetPosition.y, -10);
        }

        if (playerObject.transform.position.y - gameObject.transform.position.y > 0.75f)
        {
            targetPosition = new Vector3(targetPosition.x, playerObject.transform.position.y - 0.75f, -10);
        }
        else if (gameObject.transform.position.y - playerObject.transform.position.y > 0.75f)
        {
            targetPosition = new Vector3(targetPosition.x, playerObject.transform.position.y + 0.75f, -10);
        }

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, 0.3f);
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, GameManager.instance.waterController.gameObject.transform.position.y + (GameManager.instance.mainCamera.orthographicSize - 0.5f), Mathf.Infinity), -10);

        GameManager.instance.waterController.UpdateSpeed(-velocity.x + 1);
    }
}
