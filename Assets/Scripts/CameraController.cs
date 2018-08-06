﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CameraController : NetworkBehaviour {
    public static Transform player;


    // Update is called once per frame
    void LateUpdate () {
        if (player)
        {
            transform.position = player.transform.position;
            transform.rotation = player.transform.rotation;
        }
	}
}