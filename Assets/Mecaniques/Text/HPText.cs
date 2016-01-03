using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HPText : MonoBehaviour
{
	public Text hpText;
	
	// Update is called once per frame
	void Update ()
	{
		GameObject player = GameObject.Find ("Joueur 1");
		if (player != null) {
			CarStatus car = player.GetComponent<CarStatus> ();

			if (car != null) {
				hpText.text = "HP : " + car.currentHP.ToString () + "/" + car.maxHP.ToString ();
				
				if (car.currentHP <= car.maxHP / 2) {			
					hpText.color = Color.red;
				} 
			}
		}
	}
}
