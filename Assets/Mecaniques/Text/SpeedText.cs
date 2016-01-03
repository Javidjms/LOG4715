using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SpeedText : MonoBehaviour {
	
	public Text speedText;
	
	// Update is called once per frame
	void Update ()
	{
		GameObject player = GameObject.Find("Joueur 1");
		if (player!=null)
		{
			CarController playercar = player.GetComponent<CarController>();
			if (playercar)
			{
				speedText.text ="Speed : " + ((int)playercar.CurrentSpeed).ToString () + " km/h";
			}
		}
	}
}
