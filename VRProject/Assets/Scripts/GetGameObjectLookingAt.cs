using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetGameObjectLookingAt : MonoBehaviour {
    private RaycastHit hit;
    public GameObject eye;
    public GameObject hitted_object;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.Space)) {

            if (Physics.Raycast(eye.transform.position, eye.transform.forward, out hit)) {
                Debug.DrawRay(eye.transform.position, eye.transform.forward, Color.green);
                hitted_object = hit.transform.gameObject;
                Debug.Log(hit.transform.name);
            }   
        }
	}
}
