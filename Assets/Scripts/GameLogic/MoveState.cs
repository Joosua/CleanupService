using UnityEngine;
using System.Collections;

public class MoveState : GameState
{
	public Camera camera;

	public int totalTime = 10;
	private int timeLeft = 0;
	public int TimeLeft
	{
		get { return timeLeft; }
	}

	private float nextTick = 0f;

	public override void OnEnabled()
	{
		timeLeft = totalTime;
		MenuManager.Instance.ShowMenu<ItemMoveHUD>();
		MenuManager.Instance.Menu<ItemMoveHUD>().SetClock(timeLeft);
		nextTick = Time.time + 1f;

		Debug.Log(camera.name);
		camera.GetComponent<DragRigidbody>().enabled = true;
		camera.GetComponent<CameraControl>().enabled = true;

		base.OnEnabled();
	}

	public override void OnDisabled()
	{
		camera.GetComponent<DragRigidbody>().enabled = false;
		camera.GetComponent<CameraControl>().enabled = false;

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
