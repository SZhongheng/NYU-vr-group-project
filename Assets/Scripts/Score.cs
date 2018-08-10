using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class Score : NetworkBehaviour
{
    public TextMesh team0Score;
    public TextMesh team1Score;

    public static List<GameObject> team0List;
    public static List<GameObject> team1List;


    [SyncVar(hook = "OnChangeTeam0")]
    public int teamRemaining0;

    [SyncVar(hook = "OnChangeTeam1")]
    public int teamRemaining1;

    void OnChangeTeam0(int n)
    {
        teamRemaining0 = n;
    }

    void OnChangeTeam1(int n)
    {
        teamRemaining1 = n;
    }


    void Start()
    {
        if (isLocalPlayer)
        {
            team0Score = GameObject.FindGameObjectWithTag("team0tag").GetComponent<TextMesh>();
            team1Score = GameObject.FindGameObjectWithTag("team1tag").GetComponent<TextMesh>();
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (isLocalPlayer)
        {
            team0Score.text = team0List.Count.ToString();
            team1Score.text = team1List.Count.ToString();
        }
    }
}
