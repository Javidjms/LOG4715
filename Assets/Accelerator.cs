using UnityEngine;
using System.Collections;

public class Accelerator : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void OnTriggerEnter (Collider other) {
		Debug.Log ("Zone");
		CarController car = other.transform.GetComponentInParent<CarController> ();
		
		if (IsPlayer (car)) {
			other.GetComponentInParent<CarStatus>().Nitro+=10f;
		}
	}

	bool IsPlayer(CarController car)
	{
		if (car == null)
			return false;

		return car.GetComponent<CarUserControlMP>() != null;
	}
}
