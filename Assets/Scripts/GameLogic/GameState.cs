using UnityEngine;
using System.Collections;

public abstract class GameState : MonoBehaviour
{
	public abstract void Tick();

	virtual public void OnEnabled()
	{

	}

	virtual public void OnDisabled()
	{

	}
}
