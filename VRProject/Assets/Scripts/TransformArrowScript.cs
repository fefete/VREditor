﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class TransformArrowScript : MonoBehaviour, IVRInteractuable {

    private bool gaze_in;
    private double timer;
    public short sign;
	// Use this for initialization
	void Start () {
        if (GetComponentInParent<ArrowDataManager>()) {
            GetComponent<Image>().material = GetComponentInParent<ArrowDataManager>().onOutMaterial;
        }
    }

    // Update is called once per frame
    /*void Update () {


	}*/

    public void onGazeIn()
    {
        GetComponent<Image>().material = GetComponentInParent<ArrowDataManager>().onInMaterial;

        Debug.Log("GAZE IN");
        gaze_in = true;

    }
    public void onGazeOver()
    {
        Debug.Log("GAZE OVER");

        if (timer > 1.5)
        {
            GetComponent<Image>().material = GetComponentInParent<ArrowDataManager>().onOverMaterial;
            string t = GetComponentInParent<ArrowDataManager>().modifier.GetComponent<InputField>().text;
            float f1 = float.Parse(t);
            if (sign >= 0)
            {
                f1 += Time.deltaTime;
            }
            else {
                f1 -= Time.deltaTime;
            }
            GetComponentInParent<ArrowDataManager>().modifier.GetComponent<InputField>().text = f1.ToString();
        }
        else
        {
            timer += Time.deltaTime;
        }
    }
    public void onGazeOut()
    {
        Debug.Log("GAZE OUT");
        GetComponent<Image>().material = GetComponentInParent<ArrowDataManager>().onOutMaterial;
        gaze_in = false;
        timer = 0.0f;
    }

    public void action() {
    }
}
