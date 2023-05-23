using System.Collections;
using UnityEngine;


public class StateHandler : MonoBehaviour
{
	public bool onGround;

	private void OnCollisionEnter2D(Collision2D collision)
	{
		onGround = true;
	}

	private void OnCollisionExit2D(Collision2D collision)
	{
		onGround = false;
	}
}
