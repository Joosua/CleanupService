using UnityEngine;
using System.Collections;

public class GameActor : MonoBehaviour
{
	public enum ActorType
	{
		Normal,
		Evidence
	}

	public ActorType actorType;
	public ActorType Type
	{
		get { return actorType; }
	}

	private Material defaultMaterial;
	public Material noticedMaterial;

	public enum Visibility {
		NotVisible,
		Visible
	}

	public string hoverText = "Unknown Object";

	public Visibility visibilityState = Visibility.NotVisible;
	public Visibility VisibilityState {
		set {
			if (value == Visibility.NotVisible)
				renderer.material = defaultMaterial;
			else if (noticedMaterial != null)
			{
				renderer.material = noticedMaterial;
			}
			visibilityState = value;
		}
		get { return visibilityState; }
	}
	
	void Start () {
		defaultMaterial = renderer.material;
		GameLogic.Instance.RegisterGameObject (this);
	}

	void OnDestroy () {
		GameLogic.Instance.UnregisterGameObject (this);
	}
}
