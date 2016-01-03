using UnityEngine;
using System.Collections;

public class BlocDestructible : MonoBehaviour {
	
	// Entre dans cette fonction lorsqu il y a une collision
	void OnCollisionEnter(Collision collision) {
		// Si l objet rentre en contact avec un projectile   //Mettre une classe abstraite Super Projectile
		if(collision.gameObject.GetComponent<Projectile>()){  
			Destroy(this.gameObject); //Destruction de l obstacle ( Je pense a un bloc ou rocher )
		}
	}
}
