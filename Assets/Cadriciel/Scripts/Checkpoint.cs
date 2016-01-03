using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour 
{
	[SerializeField]
	private CheckpointManager _manager;

	[SerializeField]
	private int _index;

	void OnTriggerEnter(Collider other)
	{
		if (other as WheelCollider == null)
		{
			CarController car = other.transform.GetComponentInParent<CarController>();
			if (car)
			{
				_manager.CheckpointTriggered(car,_index);
				Debug.Log (car+" Checkpoint : "+_index);
			}
		}

	}
}
