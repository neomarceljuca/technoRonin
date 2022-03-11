using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitStop : MonoBehaviour
{
	bool waiting = false;
	public void Stop(float duration, float timeScale)
	{
		if (waiting)
			return;
		Time.timeScale = timeScale;
		StartCoroutine(WaitAndRestoreTimeScale(duration));
	}
	public void Stop(float duration)
	{
		Stop(duration, 0f);
	}

	public void CompositeStopAndSlowMO(float pauseDuration, float slowMoDuration, float slowMoTimeScale) 
	{
		if (waiting)
			return;
		Time.timeScale = 0f;

		StartCoroutine(WaitAndRestoreTimeScale(pauseDuration, slowMoDuration, slowMoTimeScale));

	}

	IEnumerator WaitAndRestoreTimeScale(float duration, float slowMoDuration, float slowMoTimeScale)
	{
		waiting = true;
		yield return new WaitForSecondsRealtime(duration);
		Time.timeScale = slowMoTimeScale;
		yield return new WaitForSecondsRealtime(slowMoDuration);
		Time.timeScale = 1f;
		waiting = false;
	}



	IEnumerator WaitAndRestoreTimeScale(float duration)
	{
		waiting = true;
		yield return new WaitForSecondsRealtime(duration);
		Time.timeScale = 1f;
		waiting = false;
	}


}
