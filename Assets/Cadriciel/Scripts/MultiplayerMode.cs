using UnityEngine;
using System.Collections;

public class MultiplayerMode : MonoBehaviour {

	[SerializeField]
	private Camera _p1Camera;
	[SerializeField]
	private GameObject _p2CameraRig;
	[SerializeField]
	private GameObject _p2Car;

	[SerializeField]
	private string vertical = "Vertical2";
	
	[SerializeField]
	private string horizontal = "Horizontal2";

	void FixedUpdate()
	{
		// pass the input to the car!
		#if CROSS_PLATFORM_INPUT
		float h = CrossPlatformInput.GetAxis(horizontal);
		float v = CrossPlatformInput.GetAxis(vertical);
		#else
		float h = Input.GetAxis(horizontal);
		float v = Input.GetAxis(vertical);
		#endif

		if (h != 0f || v!= 0f)
		{
			Rect cam1Rect = _p1Camera.rect;
			cam1Rect.y = 0.5f;
			cam1Rect.height = 0.5f;
			_p1Camera.rect = cam1Rect;
			_p2CameraRig.SetActive(true);
			_p2Car.SetActive(true);
			enabled = false;
		}
	}
}
