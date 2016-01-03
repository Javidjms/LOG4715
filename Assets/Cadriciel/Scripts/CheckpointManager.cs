using UnityEngine;
using System.Collections.Generic;

public class CheckpointManager : MonoBehaviour 
{

	[SerializeField]
	private GameObject _carContainer;

	[SerializeField]
	private int _checkPointCount;
	[SerializeField]
	private int _totalLaps;

	private bool _finished = false;
	
	private Dictionary<CarController,PositionData> _carPositions = new Dictionary<CarController, PositionData>();

	[SerializeField] WaypointCircuit circuit; 

	private class PositionData
	{
		public int lap;
		public int checkPoint;
		public int position;
		public float progress=0;
		public float progressDistance=0;
		public int progressNum = 0;
		public Transform target;
		public WaypointCircuit.RoutePoint progressPoint;
	}

	// Use this for initialization
	void Awake () 
	{
		foreach (CarController car in _carContainer.GetComponentsInChildren<CarController>(true))
		{
			_carPositions[car] = new PositionData();
		}
	}
	
	public void CheckpointTriggered(CarController car, int checkPointIndex)
	{
		PositionData carData = _carPositions[car];
		if (!_finished)
		{
			if (checkPointIndex == 0)
			{
				if (carData.checkPoint == _checkPointCount-1)
				{
					carData.checkPoint = checkPointIndex;
					carData.lap += 1;
					Debug.Log(car.name + " lap " + carData.lap);
					if (IsPlayer(car))
					{
						GetComponent<RaceManager>().Announce("Tour " + (carData.lap+1).ToString());
					}

					if (carData.lap >= _totalLaps)
					{
						_finished = true;
						GetComponent<RaceManager>().EndRace(car.name.ToLower());
					}
				}
			}
			else if (carData.checkPoint == checkPointIndex-1) //Checkpoints must be hit in order
			{
				carData.checkPoint = checkPointIndex;
			}
		}


	}

	void Update() {

		Debug.Log (getFirstCarOnRace ());
		foreach (CarController car in _carContainer.GetComponentsInChildren<CarController>(true))
		{
			float counter;
			counter = _carPositions[car].lap * 1000 + _carPositions[car].checkPoint * 100 ;
			if(car.GetComponent<WaypointProgressTracker>()!=null){
				WaypointProgressTracker wp = car.GetComponent<WaypointProgressTracker>();
				counter += wp.GetProgressDistance() ;
				_carPositions[car].progress = counter;
			}
			else{
				if (_carPositions[car].target == null)
				{
					_carPositions[car].target = new GameObject(name+" Waypoint Target").transform;
				}

				Vector3 targetDelta = _carPositions[car].target.position-car.transform.position;
				if (targetDelta.magnitude < 20)
				{
					_carPositions[car].progressNum = (_carPositions[car].progressNum+1) % circuit.Waypoints.Length;
				}
				
				_carPositions[car].target.position = circuit.Waypoints[_carPositions[car].progressNum ].position;
				_carPositions[car].target.rotation = circuit.Waypoints[ _carPositions[car].progressNum ].rotation;


				_carPositions[car].progressPoint = circuit.GetRoutePoint( _carPositions[car].progressDistance );
				Vector3 progressDelta = _carPositions[car].progressPoint.position-car.transform.position;
				if (Vector3.Dot(progressDelta,_carPositions[car].progressPoint.direction) < 0) {
					_carPositions[car].progressDistance += progressDelta.magnitude;
				}
				counter +=_carPositions[car].progressDistance;
				_carPositions[car].progress =  + counter;

			}
		}


	}

	public CarController getFirstCarOnRace(){
		float progressCounter = 0;
		CarController firstCar = null;
		foreach (CarController car in _carContainer.GetComponentsInChildren<CarController>(false))
		{
			if(_carPositions[car].progress > progressCounter){
				progressCounter = _carPositions[car].progress;
				firstCar = car;
			}
		}

		return firstCar;


	}

	public CarController getLastCarOnRace(){
		float progressCounter = 1000;
		CarController lastCar= null;
		foreach (CarController car in _carContainer.GetComponentsInChildren<CarController>(false))
		{
			if(_carPositions[car].progress <= progressCounter){
				progressCounter = _carPositions[car].progress;
				lastCar = car;
			}
		}
		
		return lastCar;
		
		
	}
	
	public int GetLap(CarController car){
		PositionData carData = _carPositions[car];
		return carData.lap;
	}

	bool IsPlayer(CarController car)
	{
		return car.GetComponent<CarUserControlMP>() != null;
	}

	public int getTotalLap(){

		return _totalLaps;

	}

}
