using UnityEngine;
using System.Collections;

public class Collectable : MonoBehaviour {
	
	public PowerUp[] items;
	private bool secureTrigger = true;

	void OnTriggerEnter(Collider other)
	{
		if (secureTrigger) { // Test if we already collide once but treatment not yet finished
			secureTrigger = false;

			CarController car = other.transform.GetComponentInParent<CarController> ();
			int chosenItem = 0;

			if (IsPlayer (car)) {

					if(!car.GetComponent<CarStatus> ().hasAlreadyPowerUp()){
						chosenItem = Random.Range (0, items.Length);
						car.GetComponent<CarStatus> ().MyPowerUp = items [chosenItem];
						car.GetComponent<CarStatus> ().MyPowerUp.Init ();
						
				}
				this.collider.enabled = false;
				Destroy (this.gameObject);
			}
			else
				secureTrigger = true;
		}
	}

	bool IsPlayer(CarController car)
	{
		if (car == null)
			return false;

		return car.GetComponent<CarUserControlMP>() != null;
	}
}
