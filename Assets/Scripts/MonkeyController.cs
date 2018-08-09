using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.EventSystems;


public class MonkeyController : NetworkBehaviour{
    
    Vector3 jumpDimensions; // Vector3 values of which the player should jump to
    public float jumpSpeed = 100.0f; // speed of jump

    Rigidbody rb; // Rigidbody of the player

    public int movementSpeed = 40;

    public bool hasMissile = false;
    public bool hasSpeedUp = false;

    public GameObject taggerPrefab;
    public GameObject missilePrefab;
    public Transform missileSpawn;
    public Transform taggerSpawn;

    Animator animator;
    bool playerIsGrounded = true;

	void Start () {
		if (!isLocalPlayer)
        {
            return;
        }

        rb = this.GetComponent<Rigidbody>();

        TouchListener.OnLongPressing += OnPlayerPressing;
        TouchListener.OnDoubleClick += OnPlayerDoubleClick;
        TouchListener.OnRelease += OnPlayerRelease;
        TouchListener.OnSingleClick += OnPlayerSingleClick;
        animator = this.GetComponent<Animator>();
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
        animator.Play("jump", 0, 0f);
        Jump();

    }

    public void OnPlayerSingleClick(Touch t)
    {
        if (isLocalPlayer)
        {
            animator.Play("attack1", 0, 0f);
        }
        CmdTag();
    }

    [Command]
    void CmdTag()
    {
        GameObject tagger = (GameObject)Instantiate(taggerPrefab, taggerSpawn.position, taggerSpawn.rotation);
        tagger.GetComponent<Rigidbody>().velocity = -tagger.transform.right * 200.0f;
        NetworkServer.Spawn(tagger);
        Destroy(tagger, 0.2f);
    }

    public void OnPlayerRelease(Touch t)
    {
        if (isLocalPlayer)
        {
            animator.SetBool("walk", false);
        }
    }

    public void OnPlayerSingleClickWithPowerup(Touch t)
    {
        if (!isLocalPlayer)
        {
            return;
        }

        if (hasMissile)
        {
            CmdShoot();
            hasMissile = false;
            TouchListener.OnSingleClick -= OnPlayerSingleClickWithPowerup;
            TouchListener.OnSingleClick += OnPlayerSingleClick;
        }
        else if (hasSpeedUp)
        {
            SpeedUp();
            hasSpeedUp = false;
            TouchListener.OnSingleClick -= OnPlayerSingleClickWithPowerup;
            TouchListener.OnSingleClick += OnPlayerSingleClick;
        }
    }


    [Command]
    void CmdShoot()
    {
        GameObject missile = (GameObject)Instantiate(missilePrefab, missileSpawn.position, missileSpawn.rotation);
        missile.GetComponent<Rigidbody>().velocity = -missile.transform.right * 80.0f;
        NetworkServer.Spawn(missile);
        Destroy(missile, 10.0f);
    }


    void SpeedUp()
    {
        if (isLocalPlayer)
        {
            movementSpeed = 70;
        }
    }

    void Move()
    {
        transform.position = transform.position + Camera.main.transform.forward * movementSpeed * Time.deltaTime;
        this.transform.rotation = Camera.main.transform.localRotation;

        if (isLocalPlayer)
        {
            animator.SetBool("walk", true);
        }
    }

    void Jump()
    {
        if (playerIsGrounded)
        {
            jumpDimensions = new Vector3(0.0f, 30.0f, 0.0f);
            rb.AddForce(jumpDimensions * jumpSpeed);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (isServer)
        {
            if (other.gameObject.CompareTag("missiletag"))
            {
                RpcMissileAttribute();
                //puc.CmdDeactivatePowerUp(other.gameObject);
                NetworkServer.Destroy(other.gameObject);
            }

            if (other.gameObject.CompareTag("speeduptag"))
            {
                RpcSpeedUpAttribute();
                //puc.CmdDeactivatePowerUp(other.gameObject);
                NetworkServer.Destroy(other.gameObject);
            }
        }


        if (other.tag == "plane")
        {
            playerIsGrounded = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "plane")
        {
            playerIsGrounded = false;
        }
    }

    [ClientRpc]
    void RpcMissileAttribute()
    {
        if (isLocalPlayer)
        {
            TouchListener.OnSingleClick -= OnPlayerSingleClick;
            TouchListener.OnSingleClick += OnPlayerSingleClickWithPowerup;
            this.hasSpeedUp = false;
            this.hasMissile = true;
        }
    }

    [ClientRpc]
    void RpcSpeedUpAttribute()
    {
        if (isLocalPlayer)
        {
            TouchListener.OnSingleClick -= OnPlayerSingleClick;
            TouchListener.OnSingleClick += OnPlayerSingleClickWithPowerup;
            this.hasSpeedUp = true;
            this.hasMissile = false;
        }
    }

}
