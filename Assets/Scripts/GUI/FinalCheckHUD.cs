using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FinalCheckHUD : IMenuBase
{
	private FinalCheckState state;
	public Text evidenceText;

	public override void OnShow()
	{
		state = GameLogic.Instance.State<FinalCheckState>();
	}

	public override void OnHide()
	{
		
	}

	public void SetEvidenceCount(int foundCount, int FailCount)
	{
		evidenceText.text = foundCount.ToString("0") + " / " + FailCount.ToString("0");
	}
}
