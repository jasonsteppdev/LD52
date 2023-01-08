using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice : MonoBehaviour
{
	[SerializeField] SpriteRenderer _spriteRenderer;
	[SerializeField] Collider2D _collider;
	[SerializeField] GameObject placementPos;
	[SerializeField] SpriteRenderer eyesSprite;
	[SerializeField] SpriteRenderer lungsSprite;
	[SerializeField] SpriteRenderer heartSprite;
	[SerializeField] SpriteRenderer kidneySprite;

	public OrganType organType;

	Player _player;
	bool _isGrabbed = false;
	bool _isDeposited = false;

	void Awake()
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
		eyesSprite.sortingOrder = 2;
		lungsSprite.sortingOrder = 2;
		heartSprite.sortingOrder = 2;
		kidneySprite.sortingOrder = 2;
		_collider.enabled = false;
	}

	public void Drop()
	{
		if (_isGrabbed)
		{
			GameManager.Instance.audioManager.PlayDropOff();
			_isGrabbed = false;
			transform.position = new Vector3(_player.grabber.transform.position.x, _player.transform.position.y, transform.position.z);
			_spriteRenderer.sortingOrder = 0;
			eyesSprite.sortingOrder = 0;
			lungsSprite.sortingOrder = 0;
			heartSprite.sortingOrder = 0;
			kidneySprite.sortingOrder = 0;
			_collider.enabled = true;
		}
	}

	public void Deposit()
	{
		GameManager.Instance.audioManager.PlayDropOff();
		gameObject.SetActive(false);
	}


	public void SetOrganType(Organ organ)
	{
		organType = organ.organType;

		_spriteRenderer.enabled = false;

		switch (organType)
		{
			case OrganType.Eyes:
				eyesSprite.enabled = true;
				lungsSprite.enabled = false;
				heartSprite.enabled = false;
				kidneySprite.enabled = false;
				break;
			case OrganType.Lungs:
				lungsSprite.enabled = true;
				eyesSprite.enabled = false;
				heartSprite.enabled = false;
				kidneySprite.enabled = false;
				break;
			case OrganType.Heart:
				heartSprite.enabled = true;
				lungsSprite.enabled = false;
				eyesSprite.enabled = false;
				kidneySprite.enabled = false;
				break;
			case OrganType.Kidney:
				kidneySprite.enabled = true;
				lungsSprite.enabled = false;
				heartSprite.enabled = false;
				eyesSprite.enabled = false;
				break;
		}
	}

}
