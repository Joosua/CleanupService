using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ItemMoveHUD : IMenuBase
{
	private MoveState state;
	public Text timeText;
	public Button leaveButton;

	public override void OnShow()
	{
		state = GameLogic.Instance.State<MoveState>();
		leaveButton.onClick.AddListener(OnLeave);
	}

	public override void OnHide()
	{
		leaveButton.onClick.RemoveListener(OnLeave);
	}

	public void SetClock(int value)
	{
		timeText.text = value.ToString("0");
	}

	public void OnLeave()
	{
		GameLogic.Instance.State<MoveState>().Leave();
	}
}
