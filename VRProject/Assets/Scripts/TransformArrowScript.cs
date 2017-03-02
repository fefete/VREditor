using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformArrowScript : MonoBehaviour, IVRInteractuable {

    private bool gaze_in;
    private double timer;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (timer > 1.5) {

        }
        else {
            timer += Time.deltaTime;
        }

	}

    public void onGazeIn()
    {
        Debug.Log("GAZE IN");
        gaze_in = true;

    }
    public void onGazeOver()
    {
    }
    public void onGazeOut()
    {
        Debug.Log("GAZE OUT");
        gaze_in = false;
        timer = 0.0f;
    }
}
