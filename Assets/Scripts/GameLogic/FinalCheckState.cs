using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FinalCheckState : GameState
{
	public int evidenceFound = 0;
	public int evidenceFailCount = 7;
	public List<GameActor> evidences = new List<GameActor>();

	public override void OnEnabled()
	{
		foreach (GameActor actor in GameLogic.Instance.gameObjects)
			if (actor.Type == GameActor.ActorType.Evidence)
				evidences.Add(actor);

		evidenceFound = 0;

		FinalCheckHUD menu = MenuManager.Instance.Menu<FinalCheckHUD>();
		menu.Show();
		menu.SetEvidenceCount(evidenceFound, evidenceFailCount);

		base.OnEnabled();
	}

	public override void OnDisabled()
	{
		MenuManager.Instance.HideMenu<FinalCheckHUD>();

		base.OnDisabled();
	}

	public override void Tick()
	{
		
	}
}
