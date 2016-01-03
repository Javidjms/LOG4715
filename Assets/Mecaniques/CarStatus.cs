using UnityEngine;
using System.Collections;

public class CarStatus : MonoBehaviour {
	
	public int currentHP = 100;
	public int maxHP = 100;
	public CarController carController ;
	public Transform damageLocation; 
	private PowerUp myPowerUp;
	private float nitro = 50;
	public float nitroMax = 100;

	public bool hasAlreadyPowerUp(){
		return myPowerUp != null;
	}

	public PowerUp MyPowerUp {
		get {
			return this.myPowerUp;
		}
		set {
			myPowerUp = value;
		}
	}
	
	public float Nitro {
		get {
			return this.nitro;
		}
		set {
			nitro = value;
			if(nitro > nitroMax)
				nitro = nitroMax;
		}
	}

	void Start () {
		carController = GetComponent<CarController> ();
	}

	public void recupererDegat(int HP)
	{
		changeHP(HP);
		changeCarHealth ();
		
	}

	public void infligerDegat(int HP)
	{
		changeHP(-HP);
		changeCarHealth ();

	}

	public void changeCarHealth () { 
		if(currentHP < 0){
			carController.ChangeMaxSpeed(0);
			Explosion();
		}
		else if(currentHP >= 0 && currentHP<20){
			carController.ChangeMaxSpeed(40);
			FireDamage();
			Smoke();
		}
		else if(currentHP >= 20 && currentHP<40){
			carController.ChangeMaxSpeed(50);
			FireDamage();
		}
		else if(currentHP >= 40 && currentHP<60){
			carController.ChangeMaxSpeed(55);
			FireDamage();
		}
		else if(currentHP >= 60 && currentHP<80){
			carController.ChangeMaxSpeed(60);
		}
		else if(currentHP >= 80){
			carController.ChangeMaxSpeed(60);
		}

	}

	public void LaunchPowerUp() {
		if (myPowerUp != null) {
			myPowerUp.Execute (this);
			myPowerUp = null;
		}
	}

	public void ConsumeNitro(bool nitroPressed) {

		if (nitroPressed && nitro > 0) {
			nitro -= 0.2f;
			carController.Nitro = true;
		} else {
			carController.Nitro = false;
		}
	}

	public void Jump(bool jumpPressed)
	{
		if (jumpPressed) {
			carController.EnableSimpleJump = true;
		} else {
			carController.EnableSimpleJump = false;
		}

		carController.SimpleJump ();

	}

	public void FireDamage() {
		GameObject prefabflare = Resources.Load("Flare") as GameObject;
		prefabflare.transform.position = damageLocation.position;
		GameObject flare = Instantiate (prefabflare) as GameObject ;
		GameObject.Destroy(flare,5);
	}

	public void Smoke() {
		GameObject prefabSmoke = Resources.Load("Smoke") as GameObject;
		prefabSmoke.transform.position = damageLocation.position;
		GameObject smoke = Instantiate (prefabSmoke) as GameObject ;
		GameObject.Destroy(smoke,10);
	}

	public void Explosion(){
		GameObject prefabExplosion = Resources.Load("Explosion") as GameObject;
		ExplosionPhysicsForce epf = prefabExplosion.GetComponent<ExplosionPhysicsForce>();
		epf.explosionForce = 1;
		prefabExplosion.transform.position = this.transform.position;
		GameObject explosion = Instantiate (prefabExplosion) as GameObject ;
		
		GameObject.Destroy(explosion,3);
		
		Destroy(this.gameObject,3);

	}

	public void changeHP(int HP)
	{
		if (this.currentHP + HP < this.maxHP) {
			this.currentHP += HP;
		} else {
			this.currentHP = this.maxHP;
		}
	}

}
