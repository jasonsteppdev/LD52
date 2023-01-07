using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Organ : MonoBehaviour
{

	[SerializeField] SpriteRenderer _spriteRenderer;
	[SerializeField] Collider2D _collider;

	Player _player;
	bool _isGrabbed = false;
	bool _isDeposited = false;
	GameObject _freezer;

	void Start()
	{
		_player = FindObjectOfType<Player>();
	}

	void Update()
	{
		if (_isGrabbed)
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

	public void Dispose()
	{
		gameObject.SetActive(false);
	}

	public void Deposit(GameObject freezer)
	{
		_isDeposited = true;
		_collider.enabled = false;
		_freezer = freezer;
	}

}