using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class BendIndication : MonoBehaviour
{
	
	public string turnDirection ; // "Left" or "Right"
	public Image image;
	public bool display = false;

	void OnTriggerEnter (Collider collider)
	{
		if (!display) {
			image.CrossFadeAlpha (0, 0.5f, false);

			if (IsPlayer (collider.GetComponentInParent<CarController> ())) {
				if (turnDirection == "Left") {
					image.sprite = Resources.Load ("turn-left", typeof(Sprite)) as Sprite;
					image.CrossFadeAlpha (1, 1f, false);
						
				} else if (turnDirection == "Right") {
					image.sprite = Resources.Load ("turn-right", typeof(Sprite)) as Sprite;
					image.CrossFadeAlpha (1, 1f, false);
						
				}
				display = true;
			}
		}
	}
	void OnTriggerExit (Collider collider)
	{

		if (display) {
			if (IsPlayer (collider.GetComponentInParent<CarController> ())) {

				image.CrossFadeAlpha (0, 0.5f, false);
				display = false;
			}
		}
	}

	bool IsPlayer (CarController car)
	{
		if (car == null)
			return false;

		return car.GetComponent<CarUserControlMP> () != null;
	}



}
