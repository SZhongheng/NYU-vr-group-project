using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {
    public int speed = 3;
    Vector3 jumpDimensions;
    float jumpSpeed = 10.0f;
    Rigidbody rb;
    private Vector3 distance;

	// Use this for initialization
	void Start () {
        rb = this.GetComponent<Rigidbody>();
        TouchListener.OnLongPressing += OnPlayerPressing;
        TouchListener.OnDoubleClick += OnPlayerDoubleClick;
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void OnPlayerPressing(Touch t)
    {
        Move();
    }

    public void OnPlayerDoubleClick(Touch t)
    {
        Jump();
    }

    void Move()
    {
        transform.position = transform.position + Camera.main.transform.forward * speed * Time.deltaTime;
    }

    void Jump()
    {
        // Maybe velocity
        jumpDimensions = new Vector3(0.0f, 50.0f, 0.0f);
        rb.AddForce(jumpDimensions * jumpSpeed);
    }


}
