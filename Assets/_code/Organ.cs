using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Organ : MonoBehaviour
{

	[SerializeField] SpriteRenderer _spriteRenderer;
	[SerializeField] Collider2D _collider;
	[SerializeField] float toxicityRate = 0.25f;
	[SerializeField] float toxicityValue = 0.001f;
	public OrganType organType;

	Player _player;
	bool _isGrabbed = false;
	bool _isDeposited = false;
	GameObject _freezer;
	float _toxicityTimer = 0f;

	void Start()
	{
		_player = FindObjectOfType<Player>();
	}

	void Update()
	{
		GameManager.Instance.toxic = true;

		if (_isGrabbed)
			transform.position = _player.grabber.transform.position;

		_toxicityTimer += Time.deltaTime;
		if (_toxicityTimer > toxicityRate)
		{
			GameManager.Instance.toxicity += toxicityValue;
			_toxicityTimer = 0f;
		}
	}

	public void Grab()
	{
		_isGrabbed = true;
		_spriteRenderer.sortingOrder = 2;
		_collider.enabled = false;
	}

	public void Drop()
	{
		if (_isGrabbed)
		{
			GameManager.Instance.audioManager.PlayDropOrgan();
			_isGrabbed = false;
			transform.position = new Vector3(_player.grabber.transform.position.x, _player.transform.position.y, transform.position.z);
			_spriteRenderer.sortingOrder = 0;
			_collider.enabled = true;
		}
	}

	public void Dispose()
	{
		GameManager.Instance.audioManager.PlayDropOrgan();
		_isGrabbed = false;
		_spriteRenderer.sortingOrder = 0;
		_collider.enabled = true;
		GameManager.Instance.toxicity -= 0.05f;
		GameManager.Instance.toxic = false;
		gameObject.SetActive(false);
	}

	public void Deposit()
	{
		GameManager.Instance.audioManager.PlayDropOrgan();
		_isGrabbed = false;
		_spriteRenderer.sortingOrder = 0;
		_collider.enabled = true;
		GameManager.Instance.toxic = false;
		gameObject.SetActive(false);
	}

}

public enum OrganType
{
	Eyes,
	Heart,
	Lungs,
	Kidney
}