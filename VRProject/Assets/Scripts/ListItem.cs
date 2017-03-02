using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListItem : MonoBehaviour {

    public string assetName;
    public ScrollView scview;
    public GameObject ob_to_look_at;

        
    // Use this for initialization
    // Update is called once per frame
    public void OnRemoveMe()
    {
        DestroyImmediate(gameObject);
        scview.setContentHeight();
    }

    public void OnMouseOver()
    {
        Debug.LogWarning(gameObject.name);
        GetComponentInParent<Image>().color = Color.red;
    }

    public void OnMouseExit()
    {
        Debug.LogWarning(gameObject.name);
        GetComponentInParent<Image>().color = Color.green;
    }

    public void LateUpdate()
    {
        //GetComponent<BoxCollider2D>().transform.LookAt(ob_to_look_at.transform);
    }
}
