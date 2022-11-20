using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class MovimentationButtonScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [Serializable]
    private enum MobileControllerButton
    {
        NONE, LEFT, RIGHT, JUMP
    }

    [SerializeField]
    private MobileControllerButton mobileControllerButton;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (mobileControllerButton == MobileControllerButton.LEFT)
        {
            GameManager.instance.playerController.toLeft = true;
        }
        else if (mobileControllerButton == MobileControllerButton.RIGHT)
        {
            GameManager.instance.playerController.toRight = true;
        }
        else if (mobileControllerButton == MobileControllerButton.JUMP)
        {
            GameManager.instance.playerController.jump = true;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (mobileControllerButton == MobileControllerButton.LEFT)
        {
            GameManager.instance.playerController.toLeft = false;
        }
        else if (mobileControllerButton == MobileControllerButton.RIGHT)
        {
            GameManager.instance.playerController.toRight = false;
        }
        else if (mobileControllerButton == MobileControllerButton.JUMP)
        {
            GameManager.instance.playerController.jump = false;
        }
    }
}
