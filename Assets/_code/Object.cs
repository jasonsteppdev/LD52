using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{

	[SerializeField] ObjectType _type = ObjectType.Organ;
	[SerializeField] SpriteRenderer _spriteRenderer;

	Player _player;

	bool _isGrabbed = false;

	void Start()
	{
		_player = FindObjectOfType<Player>();
	}

	void Update()
	{
		if(_isGrabbed)
			transform.position = _player.grabber.transform.position;
	}

	public void Grab()
	{
		_isGrabbed = true;
		_spriteRenderer.sortingOrder = 2;
	}

	public void Drop()
	{
		if (_isGrabbed)
		{
			_isGrabbed = false;
			transform.position = new Vector3(transform.position.x, _player.transform.position.y, transform.position.z);
			_spriteRenderer.sortingOrder = 0;
		}
	}
}

enum ObjectType
{
	Organ,
	Body
}