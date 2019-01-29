using UnityEngine;
using UnityEngine.UI;

public class PinchZoom3DObject : MonoBehaviour
{
	public float zoomSpeed = 0.01f;        // The rate of change of the field of view in perspective mode.
	public GameObject targetGameObject;

	void Update()
	{
		// If there are two touches on the device...
		if (Input.touchCount == 2)
		{
			// Store both touches.
			Touch touchZero = Input.GetTouch(0);
			Touch touchOne = Input.GetTouch(1);
			
			// Find the position in the previous frame of each touch.
			Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
			Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;
			
			// Find the magnitude of the vector (the distance) between the touches in each frame.
			float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
			float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;
			
			// Find the difference in the distances between each frame.
			float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

			float scale = targetGameObject.transform.localScale.x;
			scale -= deltaMagnitudeDiff * zoomSpeed;
      		scale = Mathf.Clamp(scale,0.5f,2f);
			targetGameObject.transform.localScale = new Vector3(scale,scale,scale);
		}
	}
}