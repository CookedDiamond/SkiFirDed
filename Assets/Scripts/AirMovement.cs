using System.Collections;
using UnityEngine;


public class AirMovement : MonoBehaviour
{
	private StateHandler state;
	private Rigidbody2D rb;
	[SerializeField] float turnSpeed = 10f;

	void Start()
	{
		state = GetComponent<StateHandler>();
		rb = GetComponent<Rigidbody2D>();
	}

	private void Update()
	{
		// if (state.onGround) return;

		if (Input.GetKey(KeyCode.A))
		{
			rb.angularVelocity = turnSpeed;
		}
		if(Input.GetKey(KeyCode.D))
		{
			rb.angularVelocity = -turnSpeed;
		}
	}
}
