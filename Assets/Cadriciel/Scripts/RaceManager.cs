using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class RaceManager : MonoBehaviour 
{


	[SerializeField]
	private GameObject _carContainer;

	[SerializeField]
	private GUIText _announcement;

	[SerializeField]
	private int _timeToStart;

	[SerializeField]
	private int _endCountdown;

	private bool rubberBanding = false;

	// Use this for initialization
	void Awake () 
	{
		CarActivation(false);

	}

	void Update() 
	{
		GameObject player = GameObject.Find("Joueur 1");
		if (GetComponent<CheckpointManager> ().getLastCarOnRace () == player.GetComponent<CarController> () && rubberBanding == false) {
			rubberBanding = true;
			player.GetComponent<CarStatus> ().Nitro += 20;
			player.GetComponent<CarController> ().MaxSpeed += 20;
		} else if (GetComponent<CheckpointManager> ().getLastCarOnRace () != player.GetComponent<CarController> () && rubberBanding == true) {
			rubberBanding = false;
			player.GetComponent<CarController> ().MaxSpeed -= 20;
		}
	}
	
	void Start()
	{
		StartCoroutine(StartCountdown());
	}

	IEnumerator StartCountdown()
	{
		int count = _timeToStart;
		do 
		{
			_announcement.text = count.ToString();
			yield return new WaitForSeconds(1.0f);
			count--;
		}
		while (count > 0);
		_announcement.text = "Partez!";
		CarActivation(true);
		yield return new WaitForSeconds(1.0f);
		_announcement.text = "";
	}

	public void EndRace(string winner)
	{
		StartCoroutine(EndRaceImpl(winner));
	}

	IEnumerator EndRaceImpl(string winner)
	{
		CarActivation(false);
		_announcement.fontSize = 20;
		int count = _endCountdown;
		do 
		{
			_announcement.text = "Victoire: " + winner + " en premiere place. Retour au titre dans " + count.ToString();
			yield return new WaitForSeconds(1.0f);
			count--;
		}
		while (count > 0);

		Application.LoadLevel("end");
	}

	public void Announce(string announcement, float duration = 2.0f)
	{
		StartCoroutine(AnnounceImpl(announcement,duration));
	}

	IEnumerator AnnounceImpl(string announcement, float duration)
	{
		_announcement.text = announcement;
		yield return new WaitForSeconds(duration);
		_announcement.text = "";
	}

	public void CarActivation(bool activate)
	{
		foreach (CarAIControl car in _carContainer.GetComponentsInChildren<CarAIControl>(true))
		{
			car.enabled = activate;
		}
		
		foreach (CarUserControlMP car in _carContainer.GetComponentsInChildren<CarUserControlMP>(true))
		{
			car.enabled = activate;
		}

	}

}
