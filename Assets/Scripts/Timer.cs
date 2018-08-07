using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class Timer : NetworkBehaviour {
    public TextMesh timerText; // textobject displaying time

    [SyncVar(hook = "OnChangeTimer")]
    public float timeRemaining; // time remaining in seconds

    void OnChangeTimer(float n)
    {
        timeRemaining = n;
    }

	// Use this for initialization
	void Start () {
		
	}

    public override void OnStartServer()
    {
        base.OnStartServer();

    }

	
	// Update is called once per frame
	void Update () {
        if (isServer)
        {
            timeRemaining = timeRemaining - Time.deltaTime;

            if (timeRemaining < 1)
            {
                timeRemaining = 0;
                SceneManager.LoadScene("EndScene");
            }
        }
	}
}
