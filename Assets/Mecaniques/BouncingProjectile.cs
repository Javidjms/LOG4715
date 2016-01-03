using UnityEngine;
using System.Collections;

public class BouncingProjectile : Projectile {
	
	public int maxBouncing = 1;
	public int bouncing = 0;
	public int damage = 20;

	void Start () {
		Rigidbody rigidbody =  this.GetComponent<Rigidbody>();
		rigidbody.velocity = GameObject.Find("ProjectileSpawn").transform.up * 80;

	}

	void OnCollisionEnter(Collision collision) {
		
		if (collision.gameObject.tag == "Wall" && bouncing<maxBouncing){
			ContactPoint cp = collision.contacts[0];
			//rigidbody.velocity = Vector3.Reflect(rigidbody.velocity,collision.collider.transform.position.normalized);
			rigidbody.velocity = Vector3.Reflect(rigidbody.velocity,cp.normal) * 2;
			bouncing++;
		}
		else { 		
			if(collision.gameObject.GetComponent<CarStatus>()!=null){  
			
				CarStatus car = collision.gameObject.GetComponent<CarStatus>();
				car.infligerDegat(damage);
			}

			GameObject prefabExplosion = Resources.Load("Explosion") as GameObject;
			ExplosionPhysicsForce epf = prefabExplosion.GetComponent<ExplosionPhysicsForce>();
			epf.explosionForce = 1;
			prefabExplosion.transform.position = this.transform.position;
			GameObject explosion = Instantiate (prefabExplosion) as GameObject ;

			GameObject.Destroy(this.gameObject);
			GameObject.Destroy(explosion,3);
		}
	}
}
