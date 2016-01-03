using UnityEngine;

public abstract class PowerUp : MonoBehaviour {

	public abstract void Init();
	public abstract void Execute(CarStatus car);
	public abstract string GetSpriteName();
}
