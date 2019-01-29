using UnityEngine;
using System.Collections;

public class ControlManager : MonoBehaviour {

    public delegate void OnMouseDown(Vector3 position, bool isNewInteraction, int touchId);
    public delegate void OnMouseUp(Vector3 position);
    public OnMouseDown OnMouseDownFunc;
    public OnMouseUp OnMouseUpFunc;

    private static ControlManager _instance;

    private Vector3 mousePosition;

    void Awake() {
        if (_instance == null) {
            //If I am the first instance, make me the Singleton
            _instance = this;
            //DontDestroyOnLoad(this);
        }
    }

    public static ControlManager Instance {
        get {
            if (_instance == null) {
                _instance = GameObject.FindObjectOfType<ControlManager>();

                //Tell unity not to destroy this object when loading a new scene!
                //if(_instance != null) {
                //	DontDestroyOnLoad(_instance.gameObject);
                //}
            }

            return _instance;
        }
    }

    public void Init() {
    }

    // Update is called once per frame
    void Update() {
        //#if UNITY_IOS || UNITY_ANDROID || UNITY_BLACKBERRY || UNITY_WP8
        checkTouchInput();
        //#else
        checkMouseInput();
        //#endif
    }

    private void checkMouseInput() {
        // start mouse down
        if (Input.GetMouseButtonDown(0)) {
            if (OnMouseDownFunc != null) {
                OnMouseDownFunc(Input.mousePosition, true, 0);
            }
            mousePosition = Input.mousePosition;
        } else if (Input.GetMouseButton(0)) {
            if (OnMouseDownFunc != null && mousePosition != Input.mousePosition) {
                OnMouseDownFunc(Input.mousePosition, false, 0);
            }
            mousePosition = Input.mousePosition;
        } else if (Input.GetMouseButtonUp(0)) {
            if (OnMouseUpFunc != null) {
                OnMouseUpFunc(Input.mousePosition);
            }
        }
    }

    private void checkTouchInput() {
        if (Input.touches.Length > 0) {
            Touch touch = Input.GetTouch(0);
            //Vector2 touchPosition = new Vector2(touchPo
            switch (touch.phase) {
                case TouchPhase.Began:
                    if (OnMouseDownFunc != null) {
                        OnMouseDownFunc(touch.position, true, touch.fingerId);
                    }
                    break;
                case TouchPhase.Ended:
                    if (OnMouseDownFunc != null) {
                        OnMouseUpFunc(touch.position);
                    }
                    break;
                case TouchPhase.Moved:
                    if (OnMouseDownFunc != null) {
                        OnMouseDownFunc(touch.position, false, touch.fingerId);
                    }
                    break;
            }
        }
    }
}