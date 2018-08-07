using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


/*
 * TODO:
 * 
 * 1) Pull playerprefabIndex from CustomNetworkManager to change playerprefabs to the right one
 * 
 * 2) Event Systems!!! ex: CmdTag() (PlayerController.cs)
 * 
 * 3) NetworkLobby value to NetworkManager
 * 
 * 4) When it changes to banana, there is a bug
 * 
*/

public class SetupLocalPlayer : NetworkBehaviour {
    
    public Timer t; // Timer Object
    public TextMesh timerText; // textobject displaying time

    public string playerName; // players parsed transform.name

    // [SyncVar(hook = "OnChangeTimer")]
    // public float timeRemaining;

    [SyncVar(hook = "OnChangeTeam")]
    public int pTeam; // players team


    void OnChangeTeam(int n)
    {
        // hook function to update pTeam
        pTeam = n;
    }


    void Start () {
        if (isLocalPlayer)
        {
            GetComponent<MyPlayerController>().enabled = true;
            OnChangeTeam(pTeam);
            t = FindObjectOfType<Timer>();
            timerText = FindObjectOfType<TextMesh>();
            Setup();
        }
        else
        {
            GetComponent<MyPlayerController>().enabled = false;
        }
	}

    void Update()
    {
        if (isLocalPlayer)
        {
            if (t == null || timerText == null)
            {
                OnChangeTeam(pTeam);
                t = FindObjectOfType<Timer>();
                timerText = FindObjectOfType<TextMesh>();
                playerName = this.transform.name.Substring(0, 6);
                Setup();
            }

            timerText.text = Mathf.Floor(t.timeRemaining).ToString();

            // condition to handle changing players halfway through the game
            if (t.timeRemaining < 61)
            {
                CmdUpdatePlayerCharacter(2);
            }
        }
    }

    void Setup()
    {
        // util function to setup 
        t = FindObjectOfType<Timer>();
        CmdSendName(playerName);
        CameraController.player = this.transform.GetChild(3);
        timerText.text = Mathf.Floor(t.timeRemaining).ToString();
    }

    [Command]
    public void CmdUpdatePlayerCharacter(int cid)
    {
        RpcUpdatePlayerCharacter(cid);
    }

    [Command]
    public void CmdSendName(string name)
    {
        RpcUpdateName(name);
    }

    [ClientRpc]
    public void RpcUpdatePlayerCharacter(int cid)
    {
        // Look into changing the mesh instead!
        // This does not work with Lobby because CustomNetworkManager is not being used!
        NetworkManager.singleton.GetComponent<CustomNetworkManager>().SwitchPlayer(this, cid);
    }

    [ClientRpc]
    public void RpcUpdateName(string name)
    {
        transform.name = name;
    }
}
