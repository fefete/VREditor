using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minimapCameraMovement : MonoBehaviour {

    public GameObject user = null;
    public float camera_distance;
    private void Start()
    {

    }

    // Update is called once per frame
    void Update () {
        Vector3 v = new Vector3(user.transform.position.x, user.transform.position.y + camera_distance, user.transform.position.z);
        transform.position = v;
	}
}
