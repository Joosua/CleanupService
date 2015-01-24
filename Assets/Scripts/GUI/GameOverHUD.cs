using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOverHUD : IMenuBase
{
	private GameOverState state;
	public Text headerText;
	public Text evidenceText;
	public Button closeButton;

	public override void OnShow()
	{
		state = GameLogic.Instance.State<GameOverState>();
		//closeButton.onClick.AddListener(OnClose);
	}

	public override void OnHide()
	{
		//closeButton.onClick.RemoveListener(OnClose);
	}

	public void SetHeader(string header)
	{
		headerText.text = header;
	}

	public void SetEvidenceCount(int foundCount, int FailCount)
	{
		evidenceText.text = foundCount.ToString("0") + " / " + FailCount.ToString("0");
	}

	private void OnClose()
	{
		state.OnFinish();
	}
}
