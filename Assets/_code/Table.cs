using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{

	[SerializeField] SpriteRenderer tableSprite;
	[SerializeField] SpriteRenderer bodySprite;
	[SerializeField] SpriteRenderer openBodySprite;

	public bool hasBody;

	public GameObject body;
	Body _body;

	public void PutBody(GameObject body)
	{
		tableSprite.enabled = false;
		if (body.TryGetComponent<Body>(out _body))
		{
			if (_body.isOpen)
				openBodySprite.enabled = true;
			else
				bodySprite.enabled = true;
		}

		this.body = body;
		hasBody = true;
	}

	public GameObject RemoveBody()
	{
		tableSprite.enabled = true;
		bodySprite.enabled = false;
		openBodySprite.enabled = false;
		hasBody = false;
		return body;
	}

	public void OpenBody()
	{
		openBodySprite.enabled = true;
		bodySprite.enabled = false;
		tableSprite.enabled = false;
	}

}
