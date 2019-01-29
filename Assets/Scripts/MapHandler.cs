using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class LocationConfig {
    public string name;
    public string description;
    public string url;

    public LocationConfig(string name, string description, string url) {
        this.name = name;
        this.description = description;
        this.url = url;
    }
}

public class MapHandler : MonoBehaviour {

    public Transform unityChan;
    public Dictionary<int, LocationConfig> locationConfigs;
    public LayerMask locationMask;

    private Dictionary<Location, bool> touchDownLocations;

    private Location selectedLocation;

    public InfoPanel infoPanel;
    public CompletionPanel completionPanel;

    private float completionRate = 0.15f;

    private Dictionary<int, bool> visitedLocations;

    void Start() {
        locationConfigs = new Dictionary<int, LocationConfig>() {
            { 1, new LocationConfig("Singapore Flyer", "The Singapore Flyer is a giant Ferris wheel in Singapore. Described by its operators as an observation wheel, it opened in 2008, construction having taken about 2½ years. It carried its first paying passengers on 11 February, opened to the public on 1 March, and was officially opened on 15 April. It has 28 air-conditioned capsules, each able to accommodate 28 passengers, and incorporates a three-storey terminal building.", "Singapore Flyer") },
            { 2, new LocationConfig("Esplanade", "The Esplanade is a waterfront location just north of the mouth of the Singapore River in downtown Singapore. It is primarily occupied by the Esplanade Park, and was the venue for one of Singapore's largest congregation of satay outlets until their relocation to Clarke Quay as a result of the construction of a major performance arts venue, the Esplanade - Theatres on the Bay, which took its name from this location.", "Esplanade") },
            { 3, new LocationConfig("Marina Bay Sands", "Marina Bay Sands is an integrated resort fronting Marina Bay in Singapore, built by the South Korean company Ssangyong Engineering and Construction and completed in 2010. Prior to its opening, it was billed as the world's most expensive standalone casino property at S$8 billion, including the land cost.", "Marina_Bay_Sands") },
        };

        touchDownLocations = new Dictionary<Location, bool>();
        visitedLocations = new Dictionary<int, bool>();

        ControlManager.Instance.OnMouseDownFunc = OnMouseDownAt;
        ControlManager.Instance.OnMouseUpFunc = OnMouseUpAt;
    }
    public void OnMouseDownAt(Vector3 position, bool isNewInteraction, int touchId) {
        if (infoPanel.GetComponent<RectTransform>().localScale != Vector3.zero || completionPanel.isShow == true) {
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(position);
        RaycastHit[] raycastHit = Physics.RaycastAll(ray, 5000f, locationMask);

        if (isNewInteraction) {
            touchDownLocations.Clear();

            int raycastHitLength = raycastHit.Length;
            if (raycastHitLength > 0) {
                for (int i = 0; i < raycastHitLength; i++) {
                    Location location = raycastHit[i].transform.GetComponent<Location>();

                    if (location != null && touchDownLocations.ContainsKey(location) == false) {
                        touchDownLocations.Add(location, true);
                    }
                }
            }
        }
    }

    public void OnMouseUpAt(Vector3 position) {
        if (infoPanel.GetComponent<RectTransform>().localScale != Vector3.zero || completionPanel.isShow == true) {
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(position);
        RaycastHit[] raycastHit = Physics.RaycastAll(ray, 5000f, locationMask);
        int raycastHitLength = raycastHit.Length;
        if (raycastHitLength > 0) {
            for (int i = 0; i < raycastHitLength; i++) {
                Location location = raycastHit[i].transform.GetComponent<Location>();

                if (location != null && touchDownLocations.ContainsKey(location) == true) {
                    OnSelectedLocation(location);
                }
            }
        }
    }

    private int unityChanCurrentLocation = -1;

    public void OnSelectedLocation(Location location) {
        unityChanCurrentLocation = -1;

        location.GetComponent<Animator>().SetTrigger("Bounce");

        selectedLocation = location;
        LocationConfig locationConfig = locationConfigs[location.locationId];

        // bounce the location
        iTween.MoveTo(unityChan.gameObject, iTween.Hash("position", location.standPos.position, "orienttopath", true, "looktime", 0.1f, "easetype", iTween.EaseType.linear, "speed", 2, "oncomplete", "onReachLocation", "oncompletetarget", gameObject));

        // turn unity chan to run over
        //unityChan.GetComponent<Animator>().SetTrigger("Jump");
        unityChan.GetComponent<Animator>().SetBool("run", true);

        iTween.MoveTo(unityChan.gameObject, iTween.Hash("position", location.standPos.position, "orienttopath", true, "looktime", 0.1f, "easetype", iTween.EaseType.linear, "speed", 2, "oncomplete", "onReachLocation", "oncompletetarget", gameObject));
    }

    /*
    private void onReachLocation() {
        unityChanCurrentLocation = selectedLocation.locationId;

        // show location info
        unityChan.GetComponent<Animator>().SetBool("run", false);

        // add achievements
        if (visitedLocations.ContainsKey(selectedLocation.locationId) == false) {
            unityChan.GetComponent<Animator>().SetTrigger("Jump");

            visitedLocations.Add(selectedLocation.locationId, true);
            completionPanel.ShowPanel(locationConfigs[selectedLocation.locationId].name, completionRate, completionRate + 0.15f);

            completionRate += 0.15f;
            Debug.Log("REACH SHOW COMPLETE");
        } else {
            Debug.Log("REACH SHOW INFO");
            // show info
            infoPanel.ShowPanel(locationConfigs[selectedLocation.locationId]);
        }
    }
    */

    private void onReachLocation() {
        unityChanCurrentLocation = selectedLocation.locationId;

        // show location info
        unityChan.GetComponent<Animator>().SetBool("run", false);

        // add achievements
        if (visitedLocations.ContainsKey(selectedLocation.locationId) == false) {
            unityChan.GetComponent<Animator>().SetTrigger("Jump");

           
            completionPanel.ShowPanel(locationConfigs[selectedLocation.locationId].name, completionRate, completionRate + 0.15f);

            completionRate += 0.15f;

            // show info
            //infoPanel.ShowPanel(locationConfigs[selectedLocation.locationId]);
        } else {
            infoPanel.ShowPanel(locationConfigs[selectedLocation.locationId]);
        }
    }

    public void OnCompletionPanelHide() {
        visitedLocations.Add(selectedLocation.locationId, true);
        infoPanel.ShowPanel(locationConfigs[selectedLocation.locationId]);
    }
}
