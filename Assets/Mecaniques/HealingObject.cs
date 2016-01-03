using UnityEngine;
using System.Collections;

public class HealingObject : MonoBehaviour {

	public int HP = 50 ;
	private bool secureTrigger = true;
	
	// Entre dans cette fonction lorsqu il y a une collision
	void OnCollisionEnter(Collision collision) {
		
		if (secureTrigger) { // Test if we already collide once but treatment not yet finished
			secureTrigger = false;

			if(collision.gameObject.GetComponentInParent<CarStatus>() ){ 
				
				CarStatus car = collision.gameObject.GetComponent<CarStatus>() ;


				GameObject.Destroy (this.gameObject);
				
				car.recupererDegat(HP);
				
			}
			else{
				secureTrigger = true;

			}

		}
		
	}
}
