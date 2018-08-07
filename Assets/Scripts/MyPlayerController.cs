using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.EventSystems;

public class MyPlayerController : NetworkBehaviour{
    private int movementSpeed = 60; // movement speed of the player
    Vector3 jumpDimensions; // Vector3 values of which the player should jump to
    float jumpSpeed = 10.0f; // speed of jump
    Rigidbody rb; // Rigidbody of the player

	// Use this for initialization
	void Start () {
        Debug.Log("On Start PlayerController");
        rb = this.GetComponent<Rigidbody>();
        TouchListener.OnLongPressing += OnPlayerPressing;
        TouchListener.OnDoubleClick += OnPlayerDoubleClick;
	}
	
	// Update is called once per frame
	void Update () {
        var rot = Quaternion.Euler(0.0f, Camera.main.transform.localEulerAngles.y, 0.0f);
        this.transform.rotation = rot;
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

        transform.position = transform.position + Camera.main.transform.forward * movementSpeed * Time.deltaTime;
        this.transform.rotation = Camera.main.transform.localRotation;
    }

    void Jump()
    {
        Debug.Log("Jump");
        // Maybe velocity
        jumpDimensions = new Vector3(0.0f, 200.0f, 0.0f);
        rb.AddForce(jumpDimensions * jumpSpeed);
    }

}
