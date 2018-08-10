using System.Collections.Generic;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine;


public class BananaController : NetworkBehaviour{

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

    public Transform deathCam;

    public Timer t;
    public TextMesh timerText;

    public Score score;


    void Start()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        rb = this.GetComponent<Rigidbody>();
        deathCam = GameObject.FindWithTag("deathcamtag").transform;
        score = GetComponent<Score>();


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

        if (t == null || timerText == null)
        {
            t = FindObjectOfType<Timer>();
            timerText = GameObject.FindGameObjectWithTag("timertag").GetComponent<TextMesh>();
        }

        timerText.text = Mathf.Floor(t.timeRemaining).ToString();
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


    void Move()
    {
        transform.position = transform.position + Camera.main.transform.forward * movementSpeed * Time.deltaTime;
        this.transform.rotation = Camera.main.transform.localRotation;


        // Go to walk animation
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
        if ((collision.gameObject.tag == "missiletag"||collision.gameObject.tag == "tagger") && isLocalPlayer)
        {
            this.GetComponent<NetworkIdentity>().AssignClientAuthority(this.GetComponent<NetworkIdentity>().connectionToClient);
            Debug.Log("entered");
            Debug.Log(collision.gameObject);
            Debug.Log("entered 2");
            CameraController.player = deathCam;
            timerText.text = "You got Tagged";
            CmdChangeIsDead();
            CmdDeactivateBanana(collision.gameObject);
        }

        if (collision.gameObject.tag == "missiletag" || collision.gameObject.tag == "tagger")
        {
            NetworkManager.singleton.ServerChangeScene("EndScene2");
        }
    }

    [Command]
    void CmdDeactivateBanana(GameObject other)
    {
        NetworkServer.Destroy(other);
        NetworkServer.Destroy(this.gameObject);
    }

    [Command]
    void CmdChangeIsDead()
    {
        RpcChangeIsDead();
    }

    [ClientRpc]
    void RpcChangeIsDead()
    {
        if (isLocalPlayer)
        {
            SetupLocalPlayer.isDead = true;
        }
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
