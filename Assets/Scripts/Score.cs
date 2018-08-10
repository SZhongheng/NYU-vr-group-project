using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class Score : NetworkBehaviour
{
    public TextMesh team0Score;
    public TextMesh team1Score;

    public static List<GameObject> team0List = new List<GameObject>();
    public static List<GameObject> team1List = new List<GameObject>();


    [SyncVar(hook = "OnChangeTeam0")]
    public int teamRemaining0 = 1;

    [SyncVar(hook = "OnChangeTeam1")]
    public int teamRemaining1 = 1;

    void OnChangeTeam0(int n)
    {
        teamRemaining0 = n;
        team0Score.text = 0.ToString();

    }

    void OnChangeTeam1(int n)
    {
        teamRemaining1 = n;
        team1Score.text = 0.ToString();
    }


    void Start()
    {
        team0Score = GameObject.FindGameObjectWithTag("team0tag").GetComponent<TextMesh>();
        team1Score = GameObject.FindGameObjectWithTag("team1tag").GetComponent<TextMesh>();
        //team0Score.text = 0.ToString();
    }

    void Update()
    {
        if (SetupLocalPlayer.isDead)
        {
            team1Score.text = 0.ToString();
        }
    }
}
