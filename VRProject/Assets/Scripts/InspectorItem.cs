using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectorItem : MonoBehaviour, IVRInteractuable {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void onGazeIn()
    {
        //Manager.getInstance().inspectorArrow.SetActive(true);
        //       Manager.getInstance().inspectorArrow.transform.position = transform.position;

        //        Manager.getInstance().inspectorArrow.transform.SetParent(transform);
        //        Manager.getInstance().inspectorArrow.transform.localPosition = Vector3.zero;
        //        Manager.getInstance().inspectorArrow.transform.localPosition = new Vector3(GetComponent<RectTransform>().sizeDelta.x / 2, -GetComponent<RectTransform>().sizeDelta.y, 0);
    }
    public void onGazeOver()
    {
    }
    public void onGazeOut()
    {
        //Manager.getInstance().inspectorArrow.SetActive(false);


    }
}
