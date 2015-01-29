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
        if (playButton != null)
            playButton.onClick.RemoveListener(OnPlayButtonPressed);
    }

    override public void OnShow()
    {
        if (moveCameraTarget != null)
            Camera.main.GetComponent<TweenCameraMove>().MoveToTarget(moveCameraTarget.position, 1f);

        if (playButton != null)
            playButton.onClick.AddListener(OnPlayButtonPressed);
    }

    void OnPlayButtonPressed()
    {
        MenuManager.Instance.HideMenu<MainMenu>();
        MenuManager.Instance.ShowMenu<LevelMenu>();
    }
}
