using UnityEngine;
using System.Collections;

public class AutoReplacement : MonoBehaviour {

	public string tag = "Player";

	public Transform replacementPoint;

	void OnTriggerEnter(Collider collider) { // Rajouter aussi pour projectile

		if (IsPlayer (collider.GetComponentInParent<CarController> ())) {
			Transform player = collider.transform.parent.parent;
			player.position = replacementPoint.position;
			player.rotation = replacementPoint.rotation;
			player.rigidbody.velocity = Vector3.zero;
			player.rigidbody.angularVelocity = Vector3.zero;
		
		} else if (collider.gameObject.GetComponent<Projectile> ()) {

			if (IsPlayer (collider.GetComponentInParent<CarController> ())) {
				Destroy (collider);
			}
		}
	}

	bool IsPlayer(CarController car)
	{
		if (car == null)
			return false;

		return car.GetComponent<CarUserControlMP>() != null;
	}

}
