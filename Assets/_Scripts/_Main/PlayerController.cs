using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

[System.Serializable]
public class Boundry
{
    public float xMin, xMax;
    public float yMin, yMax;
}

public class PlayerController : MonoBehaviour {

    private Rigidbody rb;
    public Boundry boundry;

    public float forwardSpeed, backwardSpeed, xSpeed;
    public float accelerationRate, decelerationRate;

    float dirX;

    private bool startAnimation;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        startAnimation = true;
        forwardSpeed = -forwardSpeed;
    }

    void Update()
    {
        dirX = Input.acceleration.x * xSpeed;
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, -7.5f, 7.5f), transform.position.y);

        AdManager.HideBanner();
    }

    private void FixedUpdate()
    {
        //float moverHorizontal = Input.GetAxisRaw("Horizontal");
        //float moverTouchHorizontal = CrossPlatformInputManager.GetAxisRaw("Horizontal");

        //Vector3 movement = new Vector3(moverTouchHorizontal, 0.0f, 0.0f);

        //rb.velocity = movement * xSpeed;

        rb.velocity = new Vector2(dirX, 0f);


        if (startAnimation == true && transform.position.y >= boundry.yMin)
        {
            rb.velocity += transform.up * forwardSpeed;
        }
        else
        {
            startAnimation = false;
        }

        if (GameController.win == true)
        {
            
            rb.velocity += transform.up * forwardSpeed;
        }

        //rb.position = new Vector3(Mathf.Clamp(rb.position.x, GameController.screenLeft, GameController.screenRight),
        //                           rb.position.y,
        //                           0.0f);
    }

    public void GoDown()
    {
        boundry.yMin -= accelerationRate;
    }

    public void GoUp()
    {
        boundry.yMin += decelerationRate;
    }
}