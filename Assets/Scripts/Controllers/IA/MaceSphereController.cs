using UnityEngine;

public class MaceSphereController : MonoBehaviour
{
    private Rigidbody2D thisRigidbody2D;
    private Vector2 initialPosition;

    private void Awake()
    {
        thisRigidbody2D = GetComponent<Rigidbody2D>();
        initialPosition = transform.position;
        if (GameManager.instance != null) GameManager.instance.maceSphereController = this;
    }

    public void ResetPosition()
    {
        thisRigidbody2D.velocity = Vector2.zero;
        thisRigidbody2D.angularVelocity = 0;
        transform.position = initialPosition;
    }
}
