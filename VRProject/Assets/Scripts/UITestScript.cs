using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITestScript : MonoBehaviour
{

    public GameObject obj_info;
    public Text nm;
    public Text posx;
    public Text posy;
    public GameObject player;
    // Use this for initialization
    void Start()
    {
        if (obj_info != null)
        {
            nm.text = obj_info.transform.name;
            posx.text = obj_info.transform.position.x.ToString();
            posy.text = obj_info.transform.position.y.ToString();
        }
        else {
            nm.text = "0";
            posx.text = "0";
            posy.text = "0";
        }
    }

    // Update is called once per frame
    void Update()
    {
        obj_info = GetComponentInParent<GetGameObjectLookingAt>().hitted_object;
        if (obj_info != null)
        {
            nm.text = obj_info.transform.name;
            posx.text = obj_info.transform.position.x.ToString();
            posy.text = obj_info.transform.position.y.ToString();
        }
        else {
            nm.text = "0";
            posx.text = "0";
            posy.text = "0";
            obj_info = GetComponentInParent<GetGameObjectLookingAt>().hitted_object;
        }
    }
}
