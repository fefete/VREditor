using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollRectHandler : MonoBehaviour {
    double timer = 0.0f;

    public GameObject scrollRectToHandle;

    //public Image top;
    //public Image down;

    // Use this for initialization
    void Start () {
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetButton("RB"))
        {

            scrollRectToHandle.GetComponent<ScrollRect>().verticalNormalizedPosition += Time.deltaTime*0.5f;
        }
        if (Input.GetButton("LB"))
        {
            scrollRectToHandle.GetComponent<ScrollRect>().verticalNormalizedPosition -= Time.deltaTime * 0.5f;

        }
    }


}
