using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ItemMoveHUD : IMenuBase
{
	private ItemMovingState state;
	public Text timeText;

	public override void OnShow()
	{
		state = GameLogic.Instance.State<ItemMovingState>();
	}

	public override void OnHide()
	{
		
	}

	public void SetClock(int value)
	{
		timeText.text = value.ToString("0");
	}
}
