using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameLogic : MonoBehaviour
{
	public LayerMask visionMask;

	private static GameLogic instance;
	public static GameLogic Instance
	{
		get
		{
			return instance;
		}
	}

	public List<GameActor> gameObjects = new List<GameActor>();

	public List<GameState> gameStates = new List<GameState>();

	private GameState activeState = null;
	public GameState ActiveState
	{
		get { return activeState; }
		set
		{
			if (activeState != value)
			{
				if (activeState != null)
					activeState.OnDisabled();

				activeState = value;

				if (activeState != null)
					activeState.OnEnabled();
			}
		}
	}

	public T State<T>() where T : GameState
	{
		foreach (GameState state in gameStates)
			if (state.GetType() == typeof(T))
				return (T)state;
		return null;
	}

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
	
	void Start ()
	{
		Invoke("StartGame", 0.1f);
	}

	void StartGame()
	{
		this.ActiveState = State<ObjectiveState>();
	}

	void FixedUpdate()
	{
		if (activeState != null)
			activeState.Tick();
	}
}
