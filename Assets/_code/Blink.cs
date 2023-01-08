using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Blink : MonoBehaviour
{
	[SerializeField] TMP_Text text;
	public float blinkSpeed = 0.25f;

	float blinkTime = 0f;

	bool direction = true;

	void Update()
	{
		FadeInOut();
	}

	void FadeInOut()
	{
		if (direction)
		{
			if (blinkTime < 1)
			{
				blinkTime += Time.deltaTime * blinkSpeed;
				text.color = Color.Lerp(new Color(1, 1, 1, 1), new Color(1, 1, 1, 0.5f), blinkTime);
			}
			else
			{
				blinkTime = 0f;
				direction = !direction;
			}
		}

		if (!direction)
		{
			if (blinkTime < 1)
			{
				blinkTime += Time.deltaTime * blinkSpeed;
				text.color = Color.Lerp(new Color(1, 1, 1, 0.5f), new Color(1, 1, 1, 1), blinkTime);
			}
			else
			{
				blinkTime = 0f;
				direction = !direction;
			}
		}

	}
}
