using UnityEngine;
using UnityEngine.EventSystems;

public class ModelRotator : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
	public GameObject rotateObject;
	public bool autoRotate = false;
	bool button_down;
	
	float start_position_x;
	float start_position_y;
	float start_rotation_y;
	float start_rotation_x;
	int dragTime;

	// Use this for initialization
	void Start()
	{
		button_down = false;
	}

	void Update()
	{
		if (autoRotate && !button_down && rotateObject)
		{
			//rotateObject.transform.Rotate(0, 0.05f, 0);
		}

		if (button_down)
		{
			dragTime++;
        }
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		if (!rotateObject)
		{
			return;
		}

		// Add event handler code here
		button_down = true;
		dragTime = 0;
		start_position_x = eventData.position.x;
		start_rotation_y = rotateObject.transform.eulerAngles.y;
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		// Add event handler code here
		button_down = false;
		dragTime = 0;
	}

	public void OnDrag(PointerEventData data)
	{
		if (!button_down || !rotateObject)
		{
			return;
		}

		// Rotate right left
		float moved_x = (start_position_x - data.position.x) / 2;
		Vector3 tmp = rotateObject.transform.eulerAngles;
		tmp.y = start_rotation_y + moved_x;

		

		rotateObject.transform.eulerAngles = tmp;
	}

	public void OnPointerClick(PointerEventData data)
	{
		if (dragTime < 10)
		{
			//
        }
	}
}
