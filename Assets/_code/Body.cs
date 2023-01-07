using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour
{

	[SerializeField] SpriteRenderer _sprite;

	Player _player;
	bool _isGrabbed;

	void Start()
	{
		_player = FindObjectOfType<Player>();
	}


	void Update()
	{

	}

	public void Grab()
	{
		_isGrabbed = true;
		_sprite.enabled = false;
	}

	public void Drop()
	{
		if (_isGrabbed)
		{
			_isGrabbed = false;
			transform.position = new Vector3(_player.bodyGrabber.transform.position.x, _player.transform.position.y, transform.position.z);
			_sprite.enabled = true;
		}
	}
}
