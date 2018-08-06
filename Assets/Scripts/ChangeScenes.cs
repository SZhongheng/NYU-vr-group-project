using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
                 
public class ChangeScenes : MonoBehaviour {
    
    public GameObject networkmanager;

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(networkmanager);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void MoveToGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }
}
