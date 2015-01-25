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
		foreach (GameActor actor in GameLogic.Instance.gameObjects)
			if (actor.Type == GameActor.ActorType.Evidence)
				evidences.Add(actor);

		evidenceFound = 0;
		foreach (GameActor actor in evidences)
		{
			if (actor.VisibilityState == GameActor.Visibility.Visible)
				evidenceFound++;
		}

		GameOverHUD menu = MenuManager.Instance.Menu<GameOverHUD>();
		menu.Show();

		if (evidenceFound >= evidenceFailCount)
			menu.SetHeader("Failed");
		else
			menu.SetHeader("Success");

		menu.SetEvidenceCount(evidenceFound, evidenceFailCount);

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
		Application.LoadLevel("Level1");
	}
}
