using UnityEngine;
using System.Collections;

public class EndScreenManager : MonoBehaviour 
{
	void Awake()
	{
		Input.simulateMouseWithTouches = true;
	}

	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
		{
			Application.LoadLevel("final_course");
		}
	}
}
