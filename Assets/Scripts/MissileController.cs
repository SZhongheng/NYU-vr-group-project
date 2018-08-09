using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MissileController : NetworkBehaviour {

    //private void OnCollisionEnter(Collision collision)
    //{
    //    Debug.Log("entered");
    //    Debug.Log(collision.gameObject);
    //    if (collision.gameObject.CompareTag("bananatag"))
    //    {
    //        Debug.Log("entered 2");
    //        CmdDeactivateBanana(collision.gameObject);
    //    }
    //}

    //[Command]
    //void CmdDeactivateBanana(GameObject other)
    //{
    //    RpcDeactivateBanana(other);
    //}

    //[ClientRpc]
    //void RpcDeactivateBanana(GameObject other)
    //{
    //    Destroy(other);
    //    Destroy(this.gameObject);
    //}
}
