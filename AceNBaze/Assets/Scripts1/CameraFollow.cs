﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    private Vector3 startPos;
	private Vector3 offset;

    private void Start()
    {
        startPos = transform.position;
		offset = player.transform.position;
    }

    void Update()
    {
        transform.position = startPos + player.transform.position - offset;
    }
}
