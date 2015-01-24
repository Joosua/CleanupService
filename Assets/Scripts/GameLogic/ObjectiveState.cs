using UnityEngine;
using System.Collections;

public class ObjectiveState : GameState
{
	public override void OnEnabled()
	{
		base.OnEnabled();
		MenuManager.Instance.HideAll();
		MenuManager.Instance.ShowMenu<ObjectiveHUD>();
	}

	public override void OnDisabled()
	{
		MenuManager.Instance.HideMenu<ObjectiveHUD>();
		base.OnDisabled();
	}

	public override void Tick()
	{
		
	}

	public void OnFinish()
	{
		GameLogic.Instance.ActiveState = GameLogic.Instance.State<MoveState>();
	}
}
