using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {
    private int speed = 20;
    Vector3 jumpDimensions;
    float jumpSpeed = 10.0f;
    Rigidbody rb;
    private Vector3 distance;

	// Use this for initialization
	void Start () {
        Debug.Log("On Start PlayerController");
        rb = this.GetComponent<Rigidbody>();
        TouchListener.OnLongPressing += OnPlayerPressing;
        TouchListener.OnDoubleClick += OnPlayerDoubleClick;
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void OnPlayerPressing(Touch t)
    {
        Debug.Log("On Pressing");
        Move();
    }

    public void OnPlayerDoubleClick(Touch t)
    {
        Debug.Log("On Double Click");
        Jump();
    }

    void Move()
    {
        Debug.Log("Move");
        transform.position = transform.position + Camera.main.transform.forward * speed * Time.deltaTime;
    }

    void Jump()
    {
        Debug.Log("Jump");
        // Maybe velocity
        jumpDimensions = new Vector3(0.0f, 90.0f, 0.0f);
        rb.AddForce(jumpDimensions * jumpSpeed);
    }


}
