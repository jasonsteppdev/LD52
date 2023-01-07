using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{

	[SerializeField] SpriteRenderer tableSprite;
	[SerializeField] SpriteRenderer bodySprite;

	public bool hasBody;

	GameObject _body;

	public void PutBody(GameObject body)
	{
		tableSprite.enabled = false;
		bodySprite.enabled = true;
		_body = body;
		hasBody = true;
	}

	public GameObject RemoveBody()
	{
		tableSprite.enabled = true;
		bodySprite.enabled = false;
		hasBody = false;
		return _body;
	}

}
