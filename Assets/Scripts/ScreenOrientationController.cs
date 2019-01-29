using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ScreenOrientationController : MonoBehaviour
{
	public ScreenOrientation screenOrientation;

	void Awake()
	{
		if (screenOrientation == ScreenOrientation.Portrait)
		{
			OrientToPortrait();
		}
		else
		{
			OrientToLandscape();
		}
	}

	public void OrientToPortrait()
	{
		Screen.orientation = ScreenOrientation.Portrait;
		Screen.autorotateToPortrait = true;
		Screen.autorotateToLandscapeLeft = false;
		Screen.autorotateToLandscapeRight = false;
	}

	public void OrientToLandscape()
	{
		Screen.orientation = ScreenOrientation.Landscape;
		Screen.autorotateToPortrait = false;
		Screen.autorotateToLandscapeLeft = true;
		Screen.autorotateToLandscapeRight = true;
	}

	public void BackToMap()
	{
		OrientToPortrait();
        SceneManager.LoadScene("Map");
	}
}
