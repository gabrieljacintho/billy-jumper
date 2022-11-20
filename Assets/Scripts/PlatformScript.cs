using UnityEngine;

public class PlatformScript : MonoBehaviour
{
    private enum Platform
    {
        NONE, MOBILE, DESKTOP
    }

    [SerializeField]
    private Platform platform;
    [SerializeField]
    private bool disable = false;

    private void Awake()
    {
#if UNITY_ANDROID || UNITY_IOS
        if (platform == Platform.DESKTOP) Destroy(gameObject);
#else
        if (platform == Platform.MOBILE) Destroy(gameObject);
#endif
        gameObject.SetActive(!disable);
    }
}
