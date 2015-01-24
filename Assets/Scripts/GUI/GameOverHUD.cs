using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOverHUD : IMenuBase
{
	private GameOverState state;
	public Text gradeText;
	public Text evidenceText;
	public Button closeButton;

	public override void OnShow()
	{
		state = GameLogic.Instance.State<GameOverState>();
		closeButton.onClick.AddListener(OnClose);
	}

	public override void OnHide()
	{
		closeButton.onClick.RemoveListener(OnClose);
	}

	public void SetGrade(string grade)
	{
		gradeText.text = grade;
	}

	public void SetEvidenceCount(int foundCount, int FailCount)
	{
		evidenceText.text = foundCount.ToString("0") + " / " + FailCount.ToString("0");
	}

	private void OnClose()
	{
		Debug.Log("Add implementation!");
		// TODO! load main menu.
	}
}
