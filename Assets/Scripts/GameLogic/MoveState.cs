using UnityEngine;
using System.Collections;

public class MoveState : GameState
{
	public Camera camera;
	public GameObject speechBuble;
	public float speechBubleAnimState = 0;
	public AnimationCurve speechBubleSclCurve;
	public AnimationCurve speechBubleRotCurve;
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

		camera.GetComponent<DragRigidbody>().enabled = true;
		camera.GetComponent<CameraControl>().enabled = true;
		speechBuble.SetActive(true);

		base.OnEnabled();
	}

	public void AnimateSpeechBubble(){
		speechBubleAnimState += Time.fixedDeltaTime;
		float scl = speechBubleSclCurve.Evaluate(speechBubleAnimState * 4.0f);
		float rot = speechBubleRotCurve.Evaluate(speechBubleAnimState * 4.0f) * 42f;
		speechBuble.transform.localScale = new Vector3 (scl, scl, scl);
		speechBuble.transform.eulerAngles = new Vector3 (0, 0, rot);

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
		AnimateSpeechBubble ();

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
