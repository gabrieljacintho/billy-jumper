using UnityEngine;

public class MaceController : MonoBehaviour
{
    [SerializeField]
    private float jumpForce = 7;
    private Rigidbody2D thisRigidbody2D;

    private void Start()
    {
        thisRigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            thisRigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
}
