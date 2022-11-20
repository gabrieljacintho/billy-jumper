using UnityEngine;

public class MaceAliveController : MonoBehaviour
{
    [SerializeField]
    private float maximumLeftPosition = 0.0f;
    [SerializeField]
    private float maximumRightPosition = 0.0f;

    [SerializeField]
    private float speedMovement = 1.0f;

    private bool toLeft;

    private void Start()
    {
        int n = Random.Range(0,2);
        toLeft = n == 1;
    }

    private void Update()
    {
        if (GameManager.instance != null && PlayerOnArea()) FollowPlayer();
        MovementController();
    }

    private void FollowPlayer()
    {
        float targetXPosition = GameManager.instance.playerController.gameObject.transform.position.x;

        if (targetXPosition < transform.position.x) toLeft = true;
        else toLeft = false;
    }

    private void MovementController()
    {
        if (toLeft) MoveToLeft();
        else MoveToRight();
    }

    private void MoveToLeft()
    {
        transform.Translate(Vector2.left * (speedMovement / 100));
        if (transform.position.x <= maximumLeftPosition) toLeft = false;
    }

    private void MoveToRight()
    {
        transform.Translate(Vector2.right * (speedMovement / 100));
        if (transform.position.x >= maximumRightPosition) toLeft = true;
    }

    private bool PlayerOnArea()
    {
        if (GameManager.instance.playerController.gameObject.transform.position.x >= maximumLeftPosition
            && GameManager.instance.playerController.gameObject.transform.position.x <= maximumRightPosition) return true; else return false;
    }
}
