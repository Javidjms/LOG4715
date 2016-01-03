using UnityEngine;
using System.Collections;

public class HomingProjectile : Projectile
{
	
	public GameObject target;
	public int damage = 20;

	void Start ()
	{
		Rigidbody rigidbody = this.GetComponent<Rigidbody> ();
		rigidbody.velocity = GameObject.Find ("ProjectileSpawn").transform.up * 80;
		target = null;
	}
	
	void Update ()
	{
		target = GetClosestCar ();
		if (target != null) {
			Vector3 direction = target.transform.position - this.transform.position;
			direction.Normalize ();
			float factor = Time.deltaTime * 80;
			this.transform.Translate (direction.x * factor, direction.y * factor, direction.z * factor, Space.World);
			this.transform.LookAt (target.transform, Vector3.forward);
			this.transform.Rotate (new Vector3 (90, 0, 0));
		}
	}

	GameObject GetClosestCar ()
	{
		Transform closestplayer = null;
		Collider[] colliders = Physics.OverlapSphere (transform.position, 50);
		foreach (Collider hit in colliders) {
			Transform player = hit.transform;
			player = (player != null) ? player.parent : player; // 1st parent
			player = (player != null) ? player.parent : player; // 2nd parent
			if (player != null) {
				float distance = Vector3.Distance (transform.position, player.transform.position);
				float direction = player.transform.position.z - transform.position.z;
				if (player.tag == "Player" && direction > 0) {
					
					if (closestplayer == null) {
						closestplayer = player;
					}
					
					if (distance <= Vector3.Distance (transform.position, closestplayer.position)) {
						closestplayer = player;
					}
				}
			}
		}
		if (closestplayer == null) {
			return null;
		}

		return closestplayer.gameObject;
	}

	void Explosion ()
	{
		GameObject prefabexplosion = Resources.Load ("Explosion") as GameObject;
		ExplosionPhysicsForce epf = prefabexplosion.GetComponent<ExplosionPhysicsForce> ();
		epf.explosionForce = 1;
		prefabexplosion.transform.position = this.transform.position;
		GameObject explosion = Instantiate (prefabexplosion) as GameObject;
		
		GameObject.Destroy (this.gameObject);
		GameObject.Destroy (explosion, 3);
	}

	void OnCollisionEnter (Collision collision)
	{
		Debug.Log ("Collision Proj");

		if (collision.gameObject.tag == "Wall") {			
			Explosion ();
		} else {
			if (collision.gameObject.GetComponent<CarStatus> () != null) {  

				CarStatus car = collision.gameObject.GetComponent<CarStatus> ();
				car.infligerDegat (damage);
			}
			Explosion ();
		}
	}
}
