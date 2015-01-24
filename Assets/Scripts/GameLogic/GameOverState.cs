using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameOverState : GameState
{
	public int evidenceFound = 0;
	public int evidenceFailCount = 7;
	public List<GameActor> evidences = new List<GameActor>();

	public override void OnEnabled()
	{
		MenuManager.Instance.ShowMenu<GameOverHUD>();
		Invoke("OnFinish", 5f);
		base.OnEnabled();
	}

	public override void OnDisabled()
	{
		base.OnDisabled();
	}

	public override void Tick()
	{
		
	}

	public void OnFinish()
	{
		MenuManager.Instance.HideMenu<GameOverHUD>();
		Application.LoadLevel("start");
	}
}
