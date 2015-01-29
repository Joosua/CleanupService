using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class LevelMenu : IMenuBase
{
    public Transform moveCameraTarget;
    public Button backButton;

    public GameObject buttonPrefab;
    protected Dictionary<string, LevelInfo> levelButtons = new Dictionary<string, LevelInfo>();

    override public void OnShow()
    {
        if (moveCameraTarget != null)
            Camera.main.GetComponent<TweenCameraMove>().MoveToTarget(moveCameraTarget.position, 1f);

        if (levelButtons.Count != TheGame.Instance.levels.Count)
        {
            GameObject go;
            Button button;
            int index = 0;

            foreach (LevelInfo info in TheGame.Instance.levels)
            {
                go = Instantiate(buttonPrefab, Vector3.zero, Quaternion.identity) as GameObject;
                go.transform.parent = transform;
                RectTransform rect = go.GetComponent<RectTransform>();

                Vector2 pos = buttonPrefab.GetComponent<RectTransform>().anchoredPosition;
                pos.y -= (90f * (float)index);
                rect.anchoredPosition = pos;

                levelButtons[info.sceneName] = info;

                button = go.transform.GetChild(0).GetComponent<Button>();
                button.name = info.sceneName;
                button.transform.GetChild(0).GetComponent<Text>().text = "Level " + (index + 1).ToString();
                button.onClick.AddListener(OnLevelButtonPressed);
                index++;
            }
        }

        backButton.onClick.AddListener(OnBackButtonPress);
    }

    override public void OnHide()
    {
        backButton.onClick.RemoveListener(OnBackButtonPress);
    }

    void OnBackButtonPress()
    {
        MenuManager.Instance.HideMenu<LevelMenu>();
        MenuManager.Instance.ShowMenu<MainMenu>();
    }

    void OnLevelButtonPressed()
    {
        GameObject go = EventSystem.current.currentSelectedGameObject;
        if (go != null)
        {
            Button button = go.GetComponent<Button>();
            if (button)
            {
                Application.LoadLevel(button.name);
            }
        }
    }
}
