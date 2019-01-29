using UnityEngine;
using System.Collections;

public class Location : MonoBehaviour {

    public int locationId;
    public Transform standPos;
    public MapHandler mapHandler;

    // Update is called once per frame
    /*
    void Update() {

        if (Application.platform == RuntimePlatform.Android) {
            if (Input.touchCount > 0) {
                if (Input.GetTouch(0).phase == TouchPhase.Began) {
                    OnSelectedLocation();
                }
            }
        }

        if (Application.platform == RuntimePlatform.WindowsEditor) {
            if (Input.GetMouseButtonDown(0)) {
                OnSelectedLocation();
            }
        }
    }
*/

   // private void OnSelectedLocation() {
    //    mapHandler.OnSelectedLocation(this);
   // }
}
