using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FinalCheckState : GameState
{
	public int evidenceFound = 0;
	public int evidenceFailCount = 7;
	public List<GameActor> evidences = new List<GameActor>();

	public List<Transform> scanLocations = new List<Transform>();
	private int scanIndex = 0;
	public GameObject policeOfficer;

	public float scanTime = 3f;
	public float turnTime = 1.5f;
	private float nextLocationTime = 0f;
	private float nextTurnTime = 0f;

	public override void OnEnabled()
	{
		foreach (GameActor actor in GameLogic.Instance.gameObjects)
			if (actor.Type == GameActor.ActorType.Evidence)
				evidences.Add(actor);

		evidenceFound = 0;

		FinalCheckHUD menu = MenuManager.Instance.Menu<FinalCheckHUD>();
		menu.Show();
		menu.SetEvidenceCount(evidenceFound, evidenceFailCount);

		scanIndex = 0;

		base.OnEnabled();
	}

	public override void OnDisabled()
	{
		MenuManager.Instance.HideMenu<FinalCheckHUD>();

		if (!MoveNextLocation())
			OnFinished();

		base.OnDisabled();
	}

	public bool MoveNextLocation()
	{
		if (scanIndex > scanLocations.Count)
			return false;

		policeOfficer.transform.position = scanLocations[scanIndex].position;
		scanIndex++;

		return true;
	}

	public void FlipCharacter()
	{
		Vector3 euler = policeOfficer.transform.eulerAngles;
		euler.y = euler.y > 0 ? -90f : 90f;
		policeOfficer.transform.eulerAngles = euler;
	}

	public override void Tick()
	{
		if (Time.time >= nextLocationTime)
		{
			if (!MoveNextLocation())
				OnFinished();
			nextLocationTime = Time.time + scanTime;
		}
		if (Time.time >= turnTime)
		{
			FlipCharacter();
			nextTurnTime = nextLocationTime + turnTime;
		}
	}

	void OnFinished()
	{
		GameLogic.Instance.ActiveState = GameLogic.Instance.State<GameOverState>();
	}
}
