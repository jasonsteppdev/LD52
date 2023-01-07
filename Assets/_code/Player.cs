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
	GameObject _freezer;

	bool _isDisposal;
	bool _isTable;

	Organ _organ;
	Body _body;
	Table _table;



	bool _isCarrying = false;

	void Start()
	{
		_rb = GetComponent<Rigidbody2D>();
		_table = FindObjectOfType<Table>();
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
			if (_isCarrying)
			{
				if (_organ != null)
				{
					if (_isDisposal)
						_organ.Dispose();
					else if (_freezer != empty)
						_organ.Deposit(_freezer);
					else
						_organ.Drop();

					_organ = null;
				}

				if (_body != null)
				{
					_normal.enabled = true;
					_holding.enabled = false;

					if (_isDisposal)
						_body.Dispose();
					else if (_isTable && !_table.hasBody)
						_body.Put();
					else
						_body.Drop();

					_body = null;
				}

				_isCarrying = false;
				_pickup = empty;

				return;
			}

			if (_isTable && _table.hasBody)
			{
				_table.hasBody = false;
				_pickup = _table.body;
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

		if (other.gameObject.CompareTag("Disposal"))
			_isDisposal = true;

		if (other.gameObject.CompareTag("Table"))
			_isTable = true;

		if (other.gameObject.CompareTag("Freezer"))
			_freezer = other.gameObject;
	}

	void OnCollisionExit2D(Collision2D other)
	{
		_freezer = empty;
		_pickup = empty;

		_isTable = false;
		_isDisposal = false;
	}
}
