using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public GameObject grabber;
	public GameObject bodyGrabber;

	[SerializeField] float moveSpeed = 1f;
	[SerializeField] GameObject empty;
	[SerializeField] SpriteRenderer _normal;
	[SerializeField] SpriteRenderer _holding;

	Vector2 _input = new Vector2(0, 0);
	Vector2 _direction = new Vector2(0, 0);
	Rigidbody2D _rb;

	GameObject _pickup;

	bool _isDisposal;
	bool _isTable;

	Organ _organ;
	Body _body;



	bool _isCarrying = false;

	void Start()
	{
		_rb = GetComponent<Rigidbody2D>();
		_pickup = empty;
	}

	void Update()
	{
		Move();
		Action();
	}

	void Move()
	{

		_input.x = Input.GetAxisRaw("Horizontal");
		_input.y = Input.GetAxisRaw("Vertical");
		_direction = _input;
		_direction.Normalize();

		_rb.position += _direction * moveSpeed * Time.deltaTime;


		if (_input.x < 0)
			transform.rotation = Quaternion.Euler(0, 180, 0);
		else if (_input.x > 0)
			transform.rotation = Quaternion.Euler(0, 0, 0);


		/*
			if context menu is open 
				if input left, right, up, or down then select context menu item
		*/
	}

	void Action()
	{
		if (Input.GetKeyDown(KeyCode.E))
		{
			// check if body 

			// check if orgran
			if (_isCarrying)
			{
				if (_organ != null)
				{
					_organ.Drop();
					_organ = null;
				}

				if (_body != null)
				{
					_normal.enabled = true;
					_holding.enabled = false;
					_body.Drop();
					_body = null;
				}

				_isCarrying = false;
				_pickup = empty;
			}

			if (_pickup != empty && _pickup.TryGetComponent<Organ>(out _organ))
			{
				_isCarrying = true;
				_organ.Grab();
				return;
			}

			if (_pickup != empty && _pickup.TryGetComponent<Body>(out _body))
			{
				_isCarrying = true;
				_normal.enabled = false;
				_holding.enabled = true;
				_body.Grab();
				return;
			}


			/*
				if player is not carrying anything
					if player is near tank then take body
					if player is near body then grab body
					if player is near freezer && freezer contains organ then grab freezer
					if player is near organ then grab organ

				if player is carrying body 
					if player is not near anything drop body
					if player is near disposal then dispose of body
					if player is near table put on table

				if player is carrying organ
					if player is not near anything drop organ
					if player is near disposal then dispose of organ
					if player is near freezer put in freezer

				if player is carrying freezer
					if player is not near anything then drop freezer
					if player is near pickup then exchange freezer

			*/
		}

		if (Input.GetKeyDown(KeyCode.F))
		{
			/*
				if player is near table and table has body then open context menu

				if context menu open && context menu item selected then begin organ harvest
			*/
		}

	}

	void OnCollisionStay2D(Collision2D other)
	{
		if (other.gameObject.CompareTag("Body") || other.gameObject.CompareTag("Organ"))
			_pickup = other.gameObject;
	}

	void OnCollisionExit2D(Collision2D other)
	{
		_pickup = empty;
	}
}
