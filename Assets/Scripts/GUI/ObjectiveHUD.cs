using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ObjectiveHUD : IMenuBase
{
	private ObjectiveState state;
	public Button closeButton;

	public override void OnShow()
	{
		state = GameLogic.Instance.State<ObjectiveState>();
		closeButton.onClick.AddListener(OnClose);
	}

	public override void OnHide()
	{
		closeButton.onClick.RemoveListener(OnClose);
	}

	private void OnClose()
	{
		state.OnFinish();
	}
}
