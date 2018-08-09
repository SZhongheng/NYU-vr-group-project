using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class Timer : NetworkBehaviour {
    
    [SyncVar(hook = "OnChangeTimer")]
    public float timeRemaining; // time remaining in seconds

    void Start()
    {
        if (!isLocalPlayer)
        {
            return;
        }
    }

    void OnChangeTimer(float n)
    {
        timeRemaining = n;
    }
	
	// Update is called once per frame
	void Update () {
        if (isServer)
        {
            timeRemaining = timeRemaining - Time.deltaTime;

            if (timeRemaining < 1)
            {
                timeRemaining = 0;
                NetworkManager.singleton.ServerChangeScene("EndScene");
            }
        }
	}

}
