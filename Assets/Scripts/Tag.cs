using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Tag : NetworkBehaviour {


    public void OnSeekerSingleClick(Touch touch)
    {
        Debug.Log("Click Working!");
        CmdTag();
    }


    public void SetInGaze(bool isin)
    {
        if (isin)
        {
            TouchListener.OnSingleClick += OnSeekerSingleClick;
        }
        else
        {
            TouchListener.OnSingleClick -= OnSeekerSingleClick;
        }
    }

    [Command]
    public void CmdTag()
    {
        if (this.transform.name == "Banana")
        {
            Destroy(this);
        }
        else
        {
            //GameObject e = Instantiate(explosion, this.transform.position, Quaternion.identity);
            //NetworkServer.Spawn(e);
            //this.GetComponent<Rigidbody>().velocity = Vector3.zero;
            //Destroy(this);
        }
    }
}
