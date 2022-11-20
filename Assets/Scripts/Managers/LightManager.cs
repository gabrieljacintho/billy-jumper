/*using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightManager : MonoBehaviour
{
    public static LightManager instance;

    [SerializeField]
    private Light2D pointLight2D;
    [SerializeField]
    private Light2D globalLight2D;
    [SerializeField]
    private Light2D uiLight2D;

    private readonly float morningGlobalLight2D = 1.0f;
    private readonly float afternoonGlobalLight2D = 0.5f;
    private readonly float eveningGlobalLight2D = 0.2f;

    private float targetGlobalLight2D = 0.5f;


    private void Awake()
    {
        if (instance == null) instance = this;
    }

    private void UpdateTargetGlobalLight2D()
    {
        if (globalLight2D.intensity == morningGlobalLight2D) targetGlobalLight2D = afternoonGlobalLight2D;
        else if (globalLight2D.intensity == afternoonGlobalLight2D) targetGlobalLight2D = eveningGlobalLight2D;
        else if (globalLight2D.intensity == eveningGlobalLight2D) targetGlobalLight2D = morningGlobalLight2D;
    }

    public void UpdateLightIntensities()
    {
        UpdateTargetGlobalLight2D();

        pointLight2D.intensity = 1.0f - targetGlobalLight2D;
        globalLight2D.intensity = targetGlobalLight2D;
        uiLight2D.intensity = Mathf.Clamp(targetGlobalLight2D + 0.2f, 0.4f, 1.0f);
    }
}
*/