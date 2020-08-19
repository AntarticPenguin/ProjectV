using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeSlider : MonoBehaviour
{
	public Slider _slider;
	public Text _timeText;

	public float _totalTime;

	private GameObject _target;


	public void SetTime(GameObject target, float time)
	{
		_target = target;
		_totalTime = time;
		_timeText.text = time.ToString();
		StartCoroutine(SliderCoroutine(time));
	}

	IEnumerator SliderCoroutine(float time)
	{
		while(time > 0f)
		{
			yield return null;
			time -= Time.deltaTime;
			_slider.value = 1 - (time / _totalTime);
			_timeText.text = time.ToString("F1");
		}

		Destroy(_target);
	}
}
