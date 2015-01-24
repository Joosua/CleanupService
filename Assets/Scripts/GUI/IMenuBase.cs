using UnityEngine;
using System.Collections;

public abstract class IMenuBase : MonoBehaviour
{
	public abstract void OnShow();

	public abstract void OnHide();

	public void Start()
	{
		MenuManager.Instance.RegisterMenu(this);
	}

	public void OnDestroyed()
	{
		MenuManager.Instance.UnregisterMenu(this);
	}

	virtual public void Hide()
	{
		gameObject.SetActive(false);
		OnHide();
	}

	virtual public void Show()
	{
		gameObject.SetActive(true);
		OnShow();
	}
}
