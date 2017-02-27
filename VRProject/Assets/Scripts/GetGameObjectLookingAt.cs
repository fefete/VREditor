using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetGameObjectLookingAt : MonoBehaviour {
    private RaycastHit hit;
    public GameObject eye;



    // Update is called once per frame
    void Update () {
        if (Input.GetKey(KeyCode.Space)) {

            if (Physics.Raycast(eye.transform.position, eye.transform.forward, out hit)) {
                Debug.DrawRay(eye.transform.position, eye.transform.forward, Color.green);
                Manager.getInstance().setObject(hit.transform.gameObject);

            }   
        }
	}
}
