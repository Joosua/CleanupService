using UnityEngine;
using System.Collections;

public class TweenCameraMove : MonoBehaviour
{
	private float tweenStart;
	private float tweenEnd;
	private Vector3 start;
	private Vector3 end;
	private bool tweenEnabled = false;

	public void MoveToTarget(Vector3 pos, float time)
	{
		tweenStart = Time.time;
		tweenEnd = Time.time + time;
		start = transform.position;
		end = pos;
		tweenEnabled = true;
	}

	public void Update()
	{
		if (tweenEnabled)
		{
			float value = (Time.time - tweenStart)/(tweenEnd - tweenStart);
			if (Time.time >= tweenEnd) {
				value = 1f;
				tweenEnabled = false;
			}
			value *= value;

			transform.position = Vector3.Lerp(start, end, value);
		}
	}
}
