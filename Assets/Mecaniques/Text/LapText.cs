using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LapText : MonoBehaviour {

	public Text lapText;

	public int lap;

	public int totallap;

	public CheckpointManager cm;

	void Start() {
	
		totallap = cm.getTotalLap ();
	
	}

	// Update is called once per frame
	void Update ()
	{
		GameObject player = GameObject.Find("Joueur 1");
		if (player!=null)
		{
			CarController playercar = player.GetComponent<CarController>();
			if (playercar)
			{
				GameObject manager = GameObject.Find("Game Manager");
				CheckpointManager cmanager = manager.GetComponent<CheckpointManager>();
				lap = cmanager.GetLap(playercar) + 1;	
				lapText.text ="Lap : " + lap.ToString () + "/" +totallap;
			}
		}
	}
}
