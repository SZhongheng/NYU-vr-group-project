using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


/*
 * TODO:
 * 
 * 1) Pull playerprefabIndex from CustomNetworkManager to change playerprefabs to the right one
 * 
 * 2) Fix Camera and Prefab Offset Distance (CameraController.cs)
 * 
 * 3) Event Systems!!! ex: CmdTag() (PlayerController.cs)
 * 
 * 4) NetworkLobby value to NetworbManager
 * 
 * 5) When it changes to banana, there is a bug
 * 
*/

public class SetupLocalPlayer : NetworkBehaviour {
    public Timer t;
    public TextMesh timerText;

    [SyncVar(hook = "OnChangeTeam")]
    public int pTeam;

   // [SyncVar(hook = "OnChangeTimer")]
   // public float timeRemaining;




    // To sync values for late comers
    public override void OnStartClient()
    {
        base.OnStartClient();
        OnChangeTeam(pTeam);
        t = FindObjectOfType<Timer>();
        timerText = FindObjectOfType<TextMesh>();

    }

    void OnChangeTeam(int n) {
        pTeam = n;

    }

    // Use this for initialization
    void Start () {

        if (isLocalPlayer)
        {
            GetComponent<PlayerController>().enabled = true;
            t = FindObjectOfType<Timer>();
            CameraController.player = this.transform.GetChild(3);
            timerText.text = Mathf.Floor(t.timeRemaining).ToString();

        }
        else
        {
            GetComponent<PlayerController>().enabled = false;
        }
		
	}
	
	// Update is called once per frame
	void Update () {
        if (isLocalPlayer)
        {
            timerText.text = Mathf.Floor(t.timeRemaining).ToString();
            if (t.timeRemaining < 31)
            {

                // 1) Pull playerprefabIndex from CustomNetworkManager to change playerprefabs to the right one
                CmdUpdatePlayerCharacter(2);
            }
        }
	}

    [Command]
    public void CmdUpdatePlayerCharacter(int cid)
    {
        NetworkManager.singleton.GetComponent<CustomNetworkManager>().SwitchPlayer(this, cid);
    }
}
