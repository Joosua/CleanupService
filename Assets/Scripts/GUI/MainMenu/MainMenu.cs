using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenu : IMenuBase
{
    public Transform moveCameraTarget;
    public Button playButton;
    public Button settingsButton;
    public Button armoryButton;

    override public void OnHide()
    {
        
    }

    override public void OnShow()
    {
        if (moveCameraTarget != null)
            Camera.main.GetComponent<TweenCameraMove>().MoveToTarget(moveCameraTarget.position, 1f);
    }
}
