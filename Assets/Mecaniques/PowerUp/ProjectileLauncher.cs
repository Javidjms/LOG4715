using UnityEngine;
using System.Collections;

public class ProjectileLauncher : PowerUp {

	private CarStatus carStatus;
	private GameObject prefab;
	public string[] availablePrefab = {"ProjectilePrefab","HomingProjectilePrefab","SpecialProjectilePrefab"};
	public int randomnumber;

	public override void Init ()
	{
		//TODO Use availablePrefab array to choose resource
		randomnumber = Random.Range (0, availablePrefab.Length);
		prefab = Resources.Load(availablePrefab[randomnumber]) as GameObject;
	}

	public override void Execute (CarStatus car)
	{
		GameObject.Instantiate(prefab, GameObject.Find("ProjectileSpawn").transform.position,  GameObject.Find("ProjectileSpawn").transform.rotation);
	}

	public override string GetSpriteName ()
	{
		string[] prefabSpriteName = {"Rocket-icon-rb","Rocket-icon-hm","Rocket-icon-sp"};
		return prefabSpriteName[randomnumber];
	}

}
