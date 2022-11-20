using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [NonSerialized]
    public bool toLeft = false;
    [NonSerialized]
    public bool toRight = false;
    [NonSerialized]
    public bool jump = false;

    public Rigidbody2D thisRigidbody2D;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private LayerMask floorLayer = 8;

    private readonly float speed = 4.5f;
    [Space]
    [SerializeField]
    private float jumpForce = 4.5f;
    [SerializeField]
    private float speedInTheAir = 4.5f;

    private bool onFloor = true;


    private void Update()
    {
        if (!GameManager.instance.loadingScene)
        {
            //onFloor = Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y - 0.4f), 0.35f, floorLayer, -1, 1);
            onFloor = Physics2D.OverlapBox(new Vector2(transform.position.x, transform.position.y - 0.42f), new Vector2(0.73f, 0.25f), 0, floorLayer);
#if UNITY_EDITOR
            if (GameManager.instance.useJoystick)
            {
                toLeft = Input.GetAxis("Horizontal") < 0;
                toRight = Input.GetAxis("Horizontal") > 0;
                jump = Input.GetKey("joystick button 0");
            }
#endif
            Controller(toLeft, toRight, jump);

            animator.SetFloat("Y", thisRigidbody2D.velocity.y + 0.5f);
            if (animator.GetBool("OnFloor") != onFloor) animator.SetBool("OnFloor", onFloor);
            if (animator.GetBool("Running") != (toLeft || toRight)) animator.SetBool("Running", toLeft || toRight);
        }
    }

    private void Controller(bool toLeft, bool toRight, bool jump)
    {
        if (toLeft || toRight || jump)
        {
            if (!TaskManager.instance.GetRoundStarted()) TaskManager.instance.SetRoundStarted(true);

            if (toRight) RunToRight();
            else if (toLeft) RunToLeft();

            if (jump && onFloor && thisRigidbody2D.velocity.y == 0) Jump();
        }
    }

    private void RunToLeft()
    {
        if (animator.GetBool("OnFloor")) transform.Translate(Vector2.left * speed * Time.deltaTime);
        else transform.Translate(Vector2.left * speedInTheAir * Time.deltaTime);

        if (!animator.GetBool("Running")) animator.SetBool("Running", true);

        if (!spriteRenderer.flipX) spriteRenderer.flipX = true;
    }

    private void RunToRight()
    {
        if (animator.GetBool("OnFloor")) transform.Translate(Vector2.right * speed * Time.deltaTime);
        else transform.Translate(Vector2.right * speedInTheAir * Time.deltaTime);

        if (!animator.GetBool("Running")) animator.SetBool("Running", true);

        if (spriteRenderer.flipX) spriteRenderer.flipX = false;
    }

    private void Jump()
    {
        thisRigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name.Contains("Coin") || collision.name.Contains("Chest"))
        {
            if (collision.gameObject.CompareTag("Coin")) TaskManager.instance.AddCoin();
            else if (collision.gameObject.CompareTag("Chest")) TaskManager.instance.AddChest();
            else if (collision.gameObject.CompareTag("BigChest")) TaskManager.instance.AddBigChest();
            else if (collision.gameObject.CompareTag("ToyChest")) TaskManager.instance.AddToyChest();
            collision.gameObject.SetActive(false);
        }
        else if (collision.gameObject.CompareTag("Water") || (collision.gameObject.CompareTag("Enemy")
#if UNITY_EDITOR
            && !GameManager.instance.devMode
#endif
            )) GameManager.instance.RestartLevel();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")
#if UNITY_EDITOR
            && !GameManager.instance.devMode
#endif
            ) GameManager.instance.RestartLevel();
    }
}