using UnityEngine;
using System.Collections;

public class SwitchScenes : MonoBehaviour {
	
	void Update () {
		if (Input.anyKeyDown)
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
