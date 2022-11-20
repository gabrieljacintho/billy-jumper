using UnityEngine;

public class HalfSawRollingController : MonoBehaviour
{
    [SerializeField]
    private float maximumLeftPosition = 0.0f;
    [SerializeField]
    private float maximumRightPosition = 0.0f;

    [SerializeField]
    private float speedMovement = 1.0f;
    private float speedRotate = 45.0f;

    private bool toLeft;

    private void Start()
    {
        int n = Random.Range(0, 2);
        toLeft = n == 1 ? true : false;
    }

    private void Update()
    {
        if (toLeft)
        {
            transform.Translate(Vector2.left * (speedMovement / 100), Space.World);
            transform.Rotate(Vector3.forward * (speedMovement / 100) * speedRotate, Space.World);
            if (transform.position.x <= maximumLeftPosition) toLeft = false;
        }
        else
        {
            transform.Translate(Vector2.right * (speedMovement / 100), Space.World);
            transform.Rotate(Vector3.back * (speedMovement / 100) * speedRotate, Space.World);
            if (transform.position.x >= maximumRightPosition) toLeft = true;
        }
    }
}
