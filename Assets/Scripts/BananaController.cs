using System.Collections.Generic;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine;


public class BananaController : NetworkBehaviour, IPointerEnterHandler, IPointerExitHandler {

    Vector3 jumpDimensions; // Vector3 values of which the player should jump to
    public float jumpSpeed = 80.0f; // speed of jump

    Rigidbody rb; // Rigidbody of the player

    public int movementSpeed = 60;

    public bool isTouching = true;
    public bool hasMissile = false;
    public bool hasSpeedUp = false;

    public GameObject missilePrefab;
    public Transform missileSpawn;
    public Score scoreBoard;

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        TouchListener.OnSingleClick += OnPlayerSingleClick;
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        TouchListener.OnSingleClick -= OnPlayerSingleClick;
    }

    void Start()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        scoreBoard = GetComponent<Score>();
        rb = this.GetComponent<Rigidbody>();

        TouchListener.OnLongPressing += OnPlayerPressing;
        TouchListener.OnDoubleClick += OnPlayerDoubleClick;
    }

    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }

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
        if (isTouching == true)
        {
            isTouching = false;
            Jump();
        }

    }

    public void OnPlayerSingleClick(Touch t)
    {
        if (isServer)
        {
            RpcTag();   
        }
    }

    [ClientRpc]
    void RpcTag()
    {
        if (isLocalPlayer)
        {
            Destroy(this.gameObject);
        }
    }


    void Move()
    {
        transform.position = transform.position + Camera.main.transform.forward * movementSpeed * Time.deltaTime;
        this.transform.rotation = Camera.main.transform.localRotation;
    }

    void Jump()
    {
        jumpDimensions = new Vector3(0.0f, 30.0f, 0.0f);
        rb.AddForce(jumpDimensions * jumpSpeed);
    }

    private void OnCollisionStay()
    {
        // variable to prevent double jump
        isTouching = true;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "missiletag" && isLocalPlayer)
        {
            this.GetComponent<NetworkIdentity>().AssignClientAuthority(this.GetComponent<NetworkIdentity>().connectionToClient);
            Debug.Log("entered");
            Debug.Log(collision.gameObject);
            Debug.Log("entered 2");
            CmdDeactivateBanana(collision.gameObject);
        }
    }

    [Command]
    void CmdDeactivateBanana(GameObject other)
    {
        NetworkServer.Destroy(other);
        NetworkServer.Destroy(this.gameObject);
    }

    //[ClientRpc]
    //void RpcDeactivateBanana(GameObject other)
    //{
    //    if (isLocalPlayer)
    //    {
    //        Destroy(other);
    //        Destroy(this.gameObject);
    //    }
    //}
}
