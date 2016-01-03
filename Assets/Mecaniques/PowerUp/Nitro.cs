using UnityEngine;
using System.Collections;
using System;

public class Nitro : PowerUp {

	private int nitroValue;

	public override void Init ()
	{
		nitroValue = 20;	
	}

	public override void Execute (CarStatus car)
	{
		Debug.Log ("Nitro ! ");
		car.Nitro += nitroValue;
	}

	public override string GetSpriteName ()
	{
		return "Nitro-icon";
	}
}
