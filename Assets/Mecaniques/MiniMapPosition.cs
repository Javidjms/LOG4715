using UnityEngine;
using System.Collections;

public class MiniMapPosition : MonoBehaviour {

	[SerializeField]
	private GameObject car;

	[SerializeField]
	private float height;

	[SerializeField]
	private float offsetX;

	[SerializeField]
	private float offsetZ;



	
	// Update is called once per frame
	void Update () {
		this.gameObject.transform.position = new Vector3(car.transform.position.x + offsetX, height, car.transform.position.z+offsetZ );
	}
	
}
