using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransformArrowScript : MonoBehaviour, IVRInteractuable {

    private bool gaze_in;
    private double timer;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	/*void Update () {


	}*/

    public void onGazeIn()
    {
        Debug.Log("GAZE IN");
        gaze_in = true;

    }
    public void onGazeOver()
    {
        if (timer > 1.5)
        {
            string t = GetComponentInParent<ArrowBehaviour>().modifier.GetComponent<InputField>().text;
            float f1 = float.Parse(t);
            f1 += 1;
            GetComponentInParent<ArrowBehaviour>().modifier.GetComponent<InputField>().text = f1.ToString();
        }
        else
        {
            timer += Time.deltaTime;
        }
    }
    public void onGazeOut()
    {
        Debug.Log("GAZE OUT");
        gaze_in = false;
        timer = 0.0f;
    }

    public void action() {
    }
}
