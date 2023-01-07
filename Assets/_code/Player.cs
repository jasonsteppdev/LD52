using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	[SerializeField] float moveSpeed = 1f;

	Vector2 _input = new Vector2(0, 0);
	Vector2 _direction = new Vector2(0, 0);
	Rigidbody2D _rb;


	void Start()
	{
		_rb = GetComponent<Rigidbody2D>();
	}

	void Update()
	{
		Move();
	}

	void Move()
	{
		_input.x = Input.GetAxisRaw("Horizontal");
		_input.y = Input.GetAxisRaw("Vertical");
		_direction = _input;
		_direction.Normalize();

		_rb.position += _direction * moveSpeed * Time.deltaTime;

		// if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
		// 	_anim.SetBool("Walking", true);
		// else
		// 	_anim.SetBool("Walking", false);

	}
}
