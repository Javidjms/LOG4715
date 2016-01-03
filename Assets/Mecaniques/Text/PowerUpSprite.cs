using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PowerUpSprite : MonoBehaviour {

	public Image image;
	public PowerUp savePowerUp;
	
	// Update is called once per frame
	void Update ()
	{
		GameObject player = GameObject.Find("Joueur 1");
		if (player != null)
		{
			CarStatus car = player.GetComponent<CarStatus>();
			if (car != null && savePowerUp != car.MyPowerUp && car.MyPowerUp != null)
			{
				image.sprite = Resources.Load(car.MyPowerUp.GetSpriteName(), typeof(Sprite)) as Sprite;
				image.CrossFadeAlpha(1, 0.5f, false);
				savePowerUp = car.MyPowerUp;
			}
			else if (car.MyPowerUp == null) {
				image.CrossFadeAlpha(0, 0.5f, false);
				savePowerUp = null;
			}
		}
	}
}
