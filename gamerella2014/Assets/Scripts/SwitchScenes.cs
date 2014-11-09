﻿using UnityEngine;
using System.Collections;

public class SwitchScenes : MonoBehaviour {
	
	void Update () {
		if (GameObject.Find ("GameManager") != null)
		{
			Destroy (GameObject.Find ("GameManager"));
		}
		if (Input.GetKeyDown (KeyCode.Space))
		{
			if (Application.loadedLevelName.Equals ("Title"))
			{
				Application.LoadLevel ("Main");
			}
			else if (Application.loadedLevelName.Equals ("Lose") || Application.loadedLevelName.Equals ("Win"))
			{
				Application.LoadLevel ("Title");
			}
		}
	}
}
