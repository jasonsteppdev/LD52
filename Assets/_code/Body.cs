using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour
{

	[SerializeField] SpriteRenderer _sprite;
	[SerializeField] SpriteRenderer _spriteOpen;
	[SerializeField] Collider2D _collider;
	[SerializeField] Vector3 _tablePos;

	Player _player;
	Table _table;
	bool _isGrabbed;
	public bool isOpen;

	void Start()
	{
		_player = FindObjectOfType<Player>();
		_table = FindObjectOfType<Table>();
	}

	public void Grab()
	{
		_isGrabbed = true;
		_sprite.enabled = false;
		_spriteOpen.enabled = false;
		_collider.enabled = false;
	}

	public void Drop()
	{
		if (_isGrabbed)
		{
			_isGrabbed = false;
			transform.position = new Vector3(_player.bodyGrabber.transform.position.x, _player.transform.position.y, transform.position.z);
			
			if(!isOpen)
				_sprite.enabled = true;
			else
				_spriteOpen.enabled = true;

			_collider.enabled = true;
		}
	}

	public void Dispose()
	{
		gameObject.SetActive(false);
	}

	public void Put()
	{
		_isGrabbed = false;
		_collider.enabled = false;
		_table.PutBody(gameObject);
	}

	public void Chop()
	{
		isOpen = true;
	}
}
