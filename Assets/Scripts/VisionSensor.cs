using UnityEngine;
using System.Collections;

public class VisionSensor : MonoBehaviour
{
	// How many times we check items with sensor per second.
	public float refreshRate = 10f;
	private float nextCheckTime = 0f;
	// Vision cone angle in degrees.
	public float visionAngle = 60f;
	public float maxDistance = 20f;

	public AudioSource alertSound;

	public GameObject[] visibleItems;

	void Start ()
	{
		nextCheckTime = Time.time + (1.0f / refreshRate);
	}

	void Update ()
	{
		if (Time.time >= nextCheckTime)
		{
			Vector3 direction;
			float distance;
			Ray ray = new Ray();
			RaycastHit hitInfo;
			foreach (GameActor obj in GameLogic.Instance.gameObjects)
			{
				// If Actor is already seen ignore raycast.
				if (obj.visibilityState == GameActor.Visibility.Visible)
					continue;

				direction = obj.transform.position - transform.position;
				distance = direction.sqrMagnitude;
				direction.Normalize();

				if (Vector3.Dot(transform.right, direction) > 0 &&
					distance <= maxDistance &&
					Vector3.Angle(direction, transform.right) <= visionAngle)
				{
					ray.direction = direction;
					ray.origin = transform.position;
					Debug.DrawLine(ray.origin, ray.origin + ray.direction * 5f, Color.blue, 2f);
					if (Physics.Raycast(ray, out hitInfo, maxDistance) &&
						hitInfo.collider.gameObject == obj.gameObject)
					{
						obj.VisibilityState = GameActor.Visibility.Visible;
						if (!alertSound.isPlaying)
							alertSound.Play();
					}
				}
			}
			nextCheckTime = Time.time + (1.0f / refreshRate);
		}
	}
}
