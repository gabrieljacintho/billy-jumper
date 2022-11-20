using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    private void Awake()
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag(gameObject.tag);
        if (gameObjects.Length > 1) Destroy(gameObject); else DontDestroyOnLoad(gameObject);
    }
}
