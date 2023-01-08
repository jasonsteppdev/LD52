using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
	public bool isOpen = false;
	public bool isOpening = false;
	public bool isClosing = false;

	public bool direction = true;

	float floatAnimationTime = 0f;

	public float speed = .5f;
	public float openCloseSpeed = 3f;

	private Vector3 start;
	private Vector3 des;
	Vector3 close;
	Vector3 open;
	private float openFraction = 0;
	private float closeFraction = 0;
	private float floatFraction = 0;

	float openDelay;

	void Start()
	{
		transform.localScale = new Vector3(1, 0, 1);

		start = new Vector3(transform.position.x, transform.position.y, transform.position.z);
		des = new Vector3(transform.position.x, transform.position.y + 0.025f, transform.position.z);

		close = new Vector3(1, 0, 1);
		open = new Vector3(1, 1, 1);
	}

	void Update()
	{
		FloatAnimation();
		OpenAnimation();
		CloseAnimation();
	}

	void OpenAnimation()
	{
		if (!isOpen && isOpening)
		{
			openDelay += Time.deltaTime;
			if (openDelay > 2)
			{
				if (openFraction < 1)
				{
					openFraction += Time.deltaTime * openCloseSpeed;
					transform.localScale = Vector3.Lerp(close, open, openFraction);
				}
				else
				{
					openFraction = 0;
					isOpen = true;
					isOpening = false;
					isClosing = false;
					openDelay = 0;
				}
			}

		}
	}

	void CloseAnimation()
	{
		if (isOpen && isClosing)
		{
			if (closeFraction < 1)
			{
				closeFraction += Time.deltaTime * openCloseSpeed;
				transform.localScale = Vector3.Lerp(open, close, closeFraction);
			}
			else
			{
				transform.localScale = new Vector3(1, 0, 0);
				closeFraction = 0;
				isOpen = false;
				isClosing = false;
				isOpening = false;
				GameManager.Instance.RandomizeOrgan();
			}
		}
	}

	void FloatAnimation()
	{
		if (isOpen && !isClosing && !isOpening)
		{
			if (direction)
			{
				if (floatFraction < 1)
				{
					floatFraction += Time.deltaTime * speed;
					transform.position = Vector3.Lerp(start, des, floatFraction);
				}
				else
				{
					floatFraction = 0;
					direction = !direction;
				}
			}
			else
			{
				if (floatFraction < 1)
				{
					floatFraction += Time.deltaTime * speed;
					transform.position = Vector3.Lerp(des, start, floatFraction);
				}
				else
				{
					floatFraction = 0;
					direction = !direction;
				}
			}
		}
	}
}
