using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazeCursor : MonoBehaviour
{

    public Camera viewCamera;
    public GameObject cursorPrefab;
    public float maxCursorDistance = 30;
    private GameObject cursorInstance;

    public GameObject objLookingAt = null;

    // Use this for initialization
    void Start()
    {
        cursorInstance = Instantiate(cursorPrefab);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCursor();
        if (objLookingAt != null)
        {
            if (objLookingAt.CompareTag("Inspector"))
            {
                objLookingAt.GetComponent<InspectorItem>().onGazeOver();
            }
            else if (objLookingAt.CompareTag("Selector"))
            {
                objLookingAt.GetComponent<ListItem>().onGazeOver();
            }
            else if (objLookingAt.CompareTag("TransformArrows"))
            {
                objLookingAt.GetComponent<TransformArrowScript>().onGazeOver();
            }
            else if (objLookingAt.CompareTag("KeyboardItem"))
            {
                objLookingAt.GetComponent<KeyboardItem>().onGazeOver();
            }
        }
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
                    if (objLookingAt.CompareTag("Inspector"))
                    {
                        Manager.getInstance().inspectorArrow.SetActive(true);
                        Manager.getInstance().inspectorArrow.GetComponent<ArrowBehaviour>().modifier = objLookingAt;
                        Manager.getInstance().inspectorArrow.transform.position = objLookingAt.transform.position;
                    }
                    else if (objLookingAt.CompareTag("Selector"))
                    {
                        objLookingAt.GetComponent<ListItem>().action();
                    }
                    else if (objLookingAt.CompareTag("KeyboardItem"))
                    {
                        objLookingAt.GetComponent<KeyboardItem>().action();

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
                objLookingAt.GetComponent<InspectorItem>().onGazeIn();
            }
            else if (objLookingAt.CompareTag("Selector"))
            {
                objLookingAt.GetComponent<ListItem>().onGazeIn();

            }
            else if (objLookingAt.CompareTag("TransformArrows"))
            {
                objLookingAt.GetComponent<TransformArrowScript>().onGazeIn();

            }
            else if (objLookingAt.CompareTag("KeyboardItem"))
            {
                objLookingAt.GetComponent<KeyboardItem>().onGazeIn();

            }
        }
        else
        {
            if (objLookingAt != null)
            {
                if (objLookingAt.CompareTag("Inspector"))
                {
                    objLookingAt.GetComponent<InspectorItem>().onGazeOut();

                }
                else if (objLookingAt.CompareTag("Selector"))
                {
                    objLookingAt.GetComponent<ListItem>().onGazeOut();

                }
                else if (objLookingAt.CompareTag("TransformArrows"))
                {
                    objLookingAt.GetComponent<TransformArrowScript>().onGazeOut();

                }
                else if (objLookingAt.CompareTag("KeyboardItem"))
                {
                    objLookingAt.GetComponent<KeyboardItem>().onGazeOut();

                }
            }
            cursorInstance.transform.position = ray.origin + ray.direction.normalized * maxCursorDistance;
            objLookingAt = null;
        }
    }
}
