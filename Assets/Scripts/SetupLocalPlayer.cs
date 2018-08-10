using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class SetupLocalPlayer : NetworkBehaviour
{
    public Timer t;
    public TextMesh timerText;

    public TextMesh namePrefab;

    public int teamNumber;
    string textboxname = "";

    [SyncVar(hook = "OnChangeName")]
    public string pName = "player";

    [SyncVar(hook = "OnChangeTeam")]
    public int pTeam = 0;

    public override void OnStartClient()
    {
        base.OnStartClient();
        Invoke("UpdateStates", 1);
    }

    void UpdateStates()
    {
        OnChangeName(pName);
        OnChangeTeam(pTeam);
        Debug.Log(Score.team0List);
        //if (pTeam == 0)
        //{
        //    Score.team0List.Add(this.gameObject);
        //}
        //else 
        //{
        //    Score.team1List.Add(this.gameObject);
        //}
    }

    void OnChangeName(string n)
    {
        pName = n;
        namePrefab.text = pName;
    }

    void OnChangeTeam(int n)
    {
        pTeam = n;
        teamNumber = pTeam;
    }

    [Command]
    public void CmdChangeName(string newName)
    {
        pName = newName;
        namePrefab.text = pName;
    }

    [Command]
    public void CmdChangeTeam(int newTeam)
    {
        pTeam = newTeam;
        teamNumber = pTeam;
    }

    void Start()
    {
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
        //if (t == null || timerText == null)
        //{
        //    t = FindObjectOfType<Timer>();
        //    timerText = GameObject.FindGameObjectWithTag("timertag").GetComponent<TextMesh>();
        //}

        //timerText.text = Mathf.Floor(t.timeRemaining).ToString();


        //determine if the object is inside the camera's viewing volume
        //if it is on screen draw its label attached to is name position

        //Vector3 nameLabelPos = Camera.main.WorldToScreenPoint(namePos.position);
        //nameLabel.transform.position = namePos.position;]

        //if (onScreen)
        //{
        //    Vector3 nameLabelPos = Camera.main.WorldToScreenPoint(namePos.position);
        //    nameLabel.transform.position = nameLabelPos;
        //}
        //else //otherwise draw it WAY off the screen 
        //nameLabel.transform.position = new Vector3(-1000, -1000, 0);
    }


    //public void OnDestroy()
    //{
    //    if (namePrefab.text != null)
    //        Destroy(namePrefab.gameObject);
    //}
}