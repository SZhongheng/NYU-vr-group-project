using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;

public class EventController : NetworkBehaviour, IPointerEnterHandler, IPointerExitHandler{

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
        CmdTag();
    }

    [Command]
    void CmdTag()
    {
        if (this.transform.name == "Banana")
        {
            Destroy(this.gameObject);
        }

    }
}
