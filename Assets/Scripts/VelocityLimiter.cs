using System.Collections;
using UnityEngine;


public class VelocityLimiter : MonoBehaviour
{
	Rigidbody2D rb;
	[SerializeField] float velocityFactor = 0.95f;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	private void FixedUpdate()
	{
		rb.velocity *= velocityFactor;
		print(rb.velocity.magnitude);
	}
}
