using UnityEngine;
using System.Collections;

public class DisplayObjectiveState : GameState
{
	public override void OnEnabled()
	{
		//MenuManager.Instance.HideMenu<ItemMoveHUD>();
		base.OnEnabled();
	}

	public override void OnDisabled()
	{
		//MenuManager.Instance.HideMenu<ItemMoveHUD>();
		base.OnDisabled();
	}

	public override void Tick()
	{
		
	}
}
