using UnityEngine;
using System.Collections;

public class SpeedoMeter : MonoBehaviour {

	public Texture2D spDial ;
	public Texture2D spPointer ;
	CarController carController;

	float currentSpeed;
	float maxSpeed;
	// Use this for initialization
	void Start () {
		GameObject player = GameObject.Find("Joueur 1");
		carController = player.GetComponent<CarController> ();
		maxSpeed = carController.MaxSpeed;
	}
	
	// Update is called once per frame
	void Update () {
		currentSpeed = carController.CurrentSpeed;
	}

	void OnGUI() {
		GUI.DrawTexture(new Rect(Screen.width - 150,Screen.height-150,100,50),spDial);
		float spFactor = currentSpeed / maxSpeed;
		float rotationAngle ;

		if (currentSpeed >= 0){
			rotationAngle = Mathf.Lerp(0,180,spFactor);
		}
		else {
			rotationAngle = Mathf.Lerp(0,180,-spFactor);
		}

		GUIUtility.RotateAroundPivot(rotationAngle,new Vector2(Screen.width-100,Screen.height-100));
		GUI.DrawTexture(new Rect(Screen.width - 150,Screen.height-150,100,100),spPointer);
	}
}
