using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class EventController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler{

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        print("enter");
        TouchListener.OnSingleClick += OnPlayerSingleClick;
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        TouchListener.OnSingleClick -= OnPlayerSingleClick;
    }

    public void OnPlayerSingleClick(Touch t)
    {
        Debug.Log("PlayerSingleClicked");
        //CmdTag();
    }

    //[Command]
    //void CmdTag()
    //{
    //    Debug.Log("Tag Working");
    //}
}
