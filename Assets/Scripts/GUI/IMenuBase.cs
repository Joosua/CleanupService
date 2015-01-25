using UnityEngine;
using System.Collections;

public abstract class IMenuBase : MonoBehaviour
{
	public abstract void OnShow();

	public abstract void OnHide();

	public void Start()
	{
	}

	public void OnDestroyed()
	{
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
