using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;

public class TagScript : NetworkBehaviour, IPointerEnterHandler, IPointerExitHandler  {


    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        TouchListener.OnSingleClick += OnPlayerSingleClick;
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        TouchListener.OnSingleClick -= OnPlayerSingleClick;
    }

    public void OnPlayerSingleClick(Touch t)
    {
        Debug.Log(this.tag);
        if (isClient && this.tag == "bananatag")
        {
            Debug.Log("Testing OnPlayerSingleClick");
            this.GetComponent<NetworkIdentity>().AssignClientAuthority(this.GetComponent<NetworkIdentity>().connectionToClient);
            CmdTag();
        }
    }

    [Command]
    void CmdTag()
    {
        RpcTag();
    }

    [ClientRpc]
    void RpcTag()
    {
        this.gameObject.SetActive(false);
    }
}
