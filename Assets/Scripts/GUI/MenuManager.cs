using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MenuManager : MonoBehaviour
{
	static MenuManager instance;
	static public MenuManager Instance
	{
		get { return instance; }
	}

	public List<IMenuBase> menus = new List<IMenuBase>();

	public T Menu<T>() where T : IMenuBase
	{
		foreach (IMenuBase menu in menus)
			if (menu.GetType() == typeof(T))
				return (T)menu;
		return null;
	}

	public void ShowMenu<T>() where T : IMenuBase
	{
		foreach (IMenuBase menu in menus)
		{
			if (menu.GetType() == typeof(T))
			{
				menu.Show();
				break;
			}
		}
	}

	public void HideMenu<T>() where T : IMenuBase
	{
		foreach (IMenuBase menu in menus)
		{
			if (menu.GetType() == typeof(T))
			{
				menu.Hide();
				break;
			}
		}
	}

	public void HideAll()
	{
		foreach (IMenuBase menu in menus)
			menu.Hide();
	}

	public void RegisterMenu(IMenuBase menu)
	{
		if (!menus.Contains(menu))
			menus.Add(menu);
	}

	public void UnregisterMenu(IMenuBase menu)
	{
		if (menus.Contains(menu))
			menus.Remove(menu);
	}

	public void Awake()
	{
		instance = this;
	}

	public void Start()
	{
		HideAll();
	}
}
