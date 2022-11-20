using UnityEngine;
using UnityEngine.UI;

public class ParallaxController : MonoBehaviour
{
    [SerializeField]
    private RawImage[] rawImages = null;

    private void Update()
    {
        if (!GameManager.instance.loadingScene)
        {
            float cameraPositionNormalized = GameManager.instance.mainCamera.transform.position.x - GameManager.instance.defaultInitialPosition.x;

            for (int i = 0; i < rawImages.Length; i++)
            {
                rawImages[i].uvRect = new Rect(cameraPositionNormalized * ((i + 2) * 0.1f) / GameManager.instance.cameraWidth, 0, rawImages[i].uvRect.width, rawImages[i].uvRect.height);
            }
        }
    }
}
