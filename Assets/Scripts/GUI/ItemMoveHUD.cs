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
	}

	public override void OnHide()
	{
		
	}

	public void SetClock(int value)
	{
		timeText.text = value.ToString("0");
	}
}
