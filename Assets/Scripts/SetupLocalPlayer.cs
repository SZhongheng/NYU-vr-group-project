using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SetupLocalPlayer : NetworkBehaviour {
    public Timer t;
    public TextMesh timerText;

    public int teamNumber;
    
    void Start () {
        if (!isLocalPlayer)
        {
            return;
        }
        CameraController.player = this.transform.GetChild(3);
	}

    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        if (t == null || timerText == null)
        {
            t = FindObjectOfType<Timer>();
            timerText = GameObject.FindGameObjectWithTag("timertag").GetComponent<TextMesh>();
        }

        timerText.text = Mathf.Floor(t.timeRemaining).ToString();
    }
}