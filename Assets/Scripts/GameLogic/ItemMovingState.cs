using UnityEngine;
using System.Collections;

public class ItemMovingState : GameState
{
	private int timeLeft = 5 * 60;
	public int TimeLeft
	{
		get { return timeLeft; }
	}

	private float nextTick = 0f;

	public override void OnEnabled()
	{
		timeLeft = 5 * 60;
		MenuManager.Instance.ShowMenu<ItemMoveHUD>();
		MenuManager.Instance.Menu<ItemMoveHUD>().SetClock(timeLeft);
		nextTick = Time.time + 1f;

		base.OnEnabled();
	}

	public override void OnDisabled()
	{
		MenuManager.Instance.HideMenu<ItemMoveHUD>();
		base.OnDisabled();
	}

	public override void Tick()
	{
		if (Time.time >= nextTick)
		{
			timeLeft -= 1;
			if (timeLeft >= 0)
			{
				MenuManager.Instance.Menu<ItemMoveHUD>().SetClock(timeLeft);
			}
			else
			{
				GameLogic.Instance.ActiveState = GameLogic.Instance.State<FinalCheckState>();
			}
			nextTick = Time.time + 1f;
		}
	}
}
