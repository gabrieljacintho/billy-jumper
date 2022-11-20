using UnityEngine;

public class SpikeAliveController : MonoBehaviour
{
    private bool toUp = false;
    private Vector2 topPosition;
    private Vector2 bottomPosition;

    private float hiddenTime;
    [SerializeField]
    private float hiddenTimeScale = 1.0f;
    [SerializeField]
    private float speedScale = 1.0f;

    [Space]
    [SerializeField]
    private float delayTime = 0.0f;
    private float timeToStart;

    private void Start()
    {
        topPosition = transform.position;
        bottomPosition = new Vector2(transform.position.x, transform.position.y - 0.87f);

        timeToStart = Time.time + delayTime;
    }

    private void Update()
    {
        if (Time.time >= timeToStart)
        {
            if (toUp)
            {
                transform.position = Vector2.Lerp(transform.position, new Vector2(transform.position.x, topPosition.y + 0.2f), 6 * speedScale * Time.deltaTime);

                if (transform.position.y >= topPosition.y)
                {
                    toUp = false;
                    hiddenTime = Time.time + (2 * hiddenTimeScale);
                }
            }
            else
            {
                transform.position = Vector2.Lerp(transform.position, new Vector2(transform.position.x, bottomPosition.y - 0.05f), 3 * speedScale * Time.deltaTime);

                if (transform.position.y <= bottomPosition.y && Time.time >= hiddenTime) toUp = true;
            }
        }
    }
}
