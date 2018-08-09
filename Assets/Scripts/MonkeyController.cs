using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.EventSystems;

public class MonkeyController : NetworkBehaviour, IPointerEnterHandler, IPointerExitHandler {
    
    Vector3 jumpDimensions; // Vector3 values of which the player should jump to
    public float jumpSpeed = 80.0f; // speed of jump

    Rigidbody rb; // Rigidbody of the player

    public int movementSpeed = 40;

    public bool isTouching = true;
    public bool hasMissile = false;
    public bool hasSpeedUp = false;

    public GameObject missilePrefab;
    public Transform missileSpawn;

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        TouchListener.OnSingleClick -= OnPlayerSingleClickWithPowerup;
        TouchListener.OnSingleClick += OnPlayerSingleClick;
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        TouchListener.OnSingleClick -= OnPlayerSingleClick;
        TouchListener.OnSingleClick += OnPlayerSingleClickWithPowerup;
    }

	void Start () {
		if (!isLocalPlayer)
        {
            return;
        }

        rb = this.GetComponent<Rigidbody>();

        TouchListener.OnLongPressing += OnPlayerPressing;
        TouchListener.OnDoubleClick += OnPlayerDoubleClick;
        TouchListener.OnSingleClick += OnPlayerSingleClickWithPowerup;
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
        }
        else if (hasSpeedUp)
        {
            SpeedUp();
            hasSpeedUp = false;
        }
    }

    public void OnPlayerSingleClick(Touch t)
    {
        CmdTag();
    }

    [Command]
    void CmdShoot()
    {
        GameObject missile = (GameObject)Instantiate(missilePrefab, missileSpawn.position, missileSpawn.rotation);
        missile.GetComponent<Rigidbody>().velocity = -missile.transform.right * 80.0f;
        NetworkServer.Spawn(missile);
        Destroy(missile, 10.0f);
    }

    [Command]
    void CmdTag()
    {
        if (this.transform.tag == "bananatag")
        {
            Destroy(this.gameObject);
        }
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

    }

    [ClientRpc]
    void RpcMissileAttribute()
    {
        if (isLocalPlayer)
        {
            this.hasSpeedUp = false;
            this.hasMissile = true;
        }
    }

    [ClientRpc]
    void RpcSpeedUpAttribute()
    {
        if (isLocalPlayer)
        {
            this.hasSpeedUp = true;
            this.hasMissile = false;
        }
    }

}
