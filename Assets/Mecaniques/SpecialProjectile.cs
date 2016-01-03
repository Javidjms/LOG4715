using UnityEngine;
using System.Collections;

public class SpecialProjectile : Projectile {

	public CarController firstPlayerTarget;
	public Transform waypointTarget;
	public int damage = 40;
	Transform[] waypoints;
	
	void Start () {
		waypoints = GameObject.Find ("Path A").GetComponent<WaypointCircuit> ().waypointList.items;
		Rigidbody rigidbody =  this.GetComponent<Rigidbody>();
		rigidbody.velocity = GameObject.Find("ProjectileSpawn").transform.up * 80;
		CheckpointManager cm =GameObject.Find ("Game Manager").GetComponent<CheckpointManager>() ;
		firstPlayerTarget = cm.getFirstCarOnRace ();
		waypointTarget = getClosestWayPoint ();
	}
	

	void Update(){
		
		if (waypointTarget != null) {
			float wpdistance =  Vector3.Distance(waypointTarget.position, transform.position);
			float fPlayerDistance =  Vector3.Distance(firstPlayerTarget.transform.position, transform.position);

			if (wpdistance < 2) {
				waypointTarget = getNextWayPoint();
			}

			Vector3 direction;

			if(fPlayerDistance < 30){
				direction = firstPlayerTarget.transform.position - this.transform.position;
			}
			else{
				direction = waypointTarget.position - this.transform.position;
			}

			direction.Normalize();
			float factor = Time.deltaTime * 80;
			this.transform.Translate(direction.x * factor, direction.y * factor, direction.z * factor, Space.World);
			this.transform.LookAt(waypointTarget.transform,Vector3.forward);
			this.transform.Rotate(new Vector3(90,0,0));
		}
	}

	Transform getNextWayPoint() {
		int maxindex = waypoints.Length -1;
		int index = System.Array.IndexOf(waypoints, waypointTarget);

		if (index == maxindex) {
			return waypoints [0];
		} else {
			return waypoints[index+1];
		}
	}

	Transform getClosestWayPoint() {

		Transform waypointTarget = null;
		float mindistance = 1000;
		float threshold = 20;
		foreach  (Transform wp in waypoints) {

			float distance =  Vector3.Distance(wp.position, transform.position);
			float direction = wp.position.z- transform.position.z ;

			if(distance < mindistance  && direction > 0){
				
				mindistance = distance;
				waypointTarget = wp ;

				if(distance <= threshold){
					break;
				}
			}
		}
		if (waypointTarget == null) {
			return null;
		}
		return waypointTarget;
	}

	void OnCollisionEnter(Collision collision) {
		if(collision.gameObject.GetComponent<CarController>()!=null){
			CarController carcontroller = collision.gameObject.GetComponent<CarController>();
			Explosion();
	
			if(carcontroller == firstPlayerTarget){

				GameObject.Destroy(this.gameObject);

			}
		}
		if(collision.gameObject.GetComponent<CarStatus>()!=null){  
			
			CarStatus carStatus = collision.gameObject.GetComponent<CarStatus>();
			carStatus.infligerDegat(damage);
		}
	}


	void Explosion(){
		GameObject prefabExplosion = Resources.Load("Explosion") as GameObject;
		ExplosionPhysicsForce epf = prefabExplosion.GetComponent<ExplosionPhysicsForce>();
		epf.explosionForce = 3;
		prefabExplosion.transform.position = this.transform.position;
		GameObject explosion = Instantiate (prefabExplosion) as GameObject ;
		GameObject.Destroy(explosion,3);
	}
}
