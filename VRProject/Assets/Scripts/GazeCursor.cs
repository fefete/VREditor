using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazeCursor : MonoBehaviour
{

    public Camera viewCamera;
    public GameObject cursorPrefab;
    public float maxCursorDistance = 30;
    private GameObject cursorInstance;

    private GameObject objLookingAt = null;

    // Use this for initialization
    void Start()
    {
        cursorInstance = Instantiate(cursorPrefab);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCursor();
    }
    private void UpdateCursor()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (objLookingAt != null)
            {
                if (objLookingAt.layer != 5)
                {
                    Manager.getInstance().setObject(objLookingAt);
                }
                else
                {
                    ListItem temp = objLookingAt.GetComponent<ListItem>();
                    if (temp)
                    {
                        Manager.getInstance().spawnObject(temp.assetName);
                    }
                }
            }
        }
        Ray ray = new Ray(viewCamera.transform.position, viewCamera.transform.rotation * Vector3.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            cursorInstance.transform.position = hit.point;
            objLookingAt = hit.collider.gameObject;
            if (objLookingAt.CompareTag("Inspector"))
            {

            }
            else if (objLookingAt.CompareTag("Selector"))
            {
                objLookingAt.GetComponent<ListItem>().onGazeIn();
            }
        }
        else
        {
            if (objLookingAt != null)
            {
                if (objLookingAt.CompareTag("Inspector"))
                {

                }
                else if (objLookingAt.CompareTag("Selector"))
                {
                    objLookingAt.GetComponent<ListItem>().onGazeOut();
                }
            }
            cursorInstance.transform.position = ray.origin + ray.direction.normalized * maxCursorDistance;
            objLookingAt = null;
        }
    }
}
