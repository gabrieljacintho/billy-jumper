using UnityEngine;

public class RollingController : MonoBehaviour
{
    private Rigidbody2D thisRigidbody2D;
    
    [SerializeField]
    private float maximumLeftPosition = 0.0f;
    [SerializeField]
    private float maximumRightPosition = 10.0f;

    [SerializeField]
    private float speedMovement = 90;

    private bool toLeft;

    private void Start()
    {
        thisRigidbody2D = GetComponent<Rigidbody2D>();

        int n = Random.Range(0, 2);
        toLeft = n == 1 ? true : false;
    }

    private void Update()
    {
        if (toLeft)
        {
            thisRigidbody2D.angularVelocity = speedMovement;
            if (transform.position.x <= maximumLeftPosition) toLeft = false;
        }
        else
        {
            thisRigidbody2D.angularVelocity = -speedMovement;
            if (transform.position.x >= maximumRightPosition) toLeft = true;
        }
    }
}
