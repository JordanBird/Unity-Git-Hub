﻿using UnityEngine;
using System.Collections;

public class AIShipBehaviour : MonoBehaviour
{
	public float speed = 0.5f;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		transform.position = new Vector3(transform.position.x + speed, transform.position.y, transform.position.z);
	}
}
