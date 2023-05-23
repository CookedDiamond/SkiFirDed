using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumping : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float jumpForce;
    [SerializeField] private float charger;
    private bool discharge;

    private StateHandler state;


    // Start is called before the first frame update
    void Start()
    {
        state = GetComponent<StateHandler>();   
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.Space) && state.onGround)
        {
            charger += Time.deltaTime;
            
        }

        if (Input.GetKeyUp(KeyCode.Space) && state.onGround)
        {
            discharge = true;
        }

    }

    private void FixedUpdate()
    {

        if (discharge)
        {

        jumpForce = 10 * charger;
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        
        discharge = false;
        charger = 0f;
        }

    }

}
