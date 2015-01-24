using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameLogic : MonoBehaviour
{
	private static GameLogic instance;
	public static GameLogic Instance
	{
		get {
			return instance;
		}
	}

    public List<GameActor> gameObjects = new List<GameActor>();

	void Awake() 
	{
		instance = this;
	}

    public void RegisterGameObject(GameActor go)
    {
		if (!gameObjects.Contains(go))
			gameObjects.Add(go);
	}

    public void UnregisterGameObject(GameActor go)
    {
		if (gameObjects.Contains(go))
			gameObjects.Remove(go);
	}
	
	void Start () {
	
	}

	void Update () {
	
	}
}
