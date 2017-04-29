using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazeCursor : MonoBehaviour
{
    //Camera to raycast from
    public Camera viewCamera;
    //max distance of the cursor
    public float maxCursorDistance = 30;
    //physical representation for the cursor
    public GameObject cursorInstance;
    //gameobject currently being looked
    public GameObject objLookingAt = null;

    // Use this for initialization
    // Update is called once per frame
    void Update()
    {
        UpdateCursor();
        /*if the object is not null, we are going to get the tag of the object,
         * and if it has one of the classes that are supposed to be vr interactuable
         * we use the methods implemented in the IVRInractuableItem interface
         */
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
                //if the layer is not the UI layer, we directly set the object in the manager as the current object being inspected.
                if (objLookingAt.layer != 5)
                {
                    Manager.getInstance().setObject(objLookingAt);
                }
                else
                {
                    //if is an UI element, we do other things, depending of the tag
                    if (objLookingAt.CompareTag("Inspector"))
                    {
                        Manager.getInstance().inspectorArrow.SetActive(true);
                        Manager.getInstance().inspectorArrow.GetComponent<ArrowDataManager>().modifier = objLookingAt;
                        Vector3 newvec = new Vector3(objLookingAt.transform.position.x, objLookingAt.transform.position.y, objLookingAt.transform.position.z);
                        newvec = newvec - new Vector3(0, 0, 0.0001f);
                        Manager.getInstance().inspectorArrow.transform.position = newvec;

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
        LayerMask layer = ~LayerMask.NameToLayer("UI");
        //as the UI is going to be always before everything else, we first get if there's an object in the UI layer that we can collide with
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layer))
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
        //if there's no UI element, we try to get whatever element we can collide
        else if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            cursorInstance.transform.position = hit.point;
            objLookingAt = hit.collider.gameObject;
        }
        //if we don't hit anything but we have an object currently being used, we need to call the method of the interface if is UI, if not, nothing
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
