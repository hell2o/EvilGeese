﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestPopupManager : MonoBehaviour {
	float timeToDisplay = 3f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		timeToDisplay -= Time.deltaTime;
		if (timeToDisplay < 0) {
			Destroy (this.gameObject);
		}
	}
}