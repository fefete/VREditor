using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateCollector : MonoBehaviour {

    public InputField x;
    public InputField y;
    public InputField z;

    public GameObject currentGameObject;

    public void onValueChangePos(string temp)
    {
        currentGameObject.transform.position = new Vector3(float.Parse(x.text), float.Parse(y.text), float.Parse(z.text));
    }
    public void onValueChangeRot(string temp)
    {
        currentGameObject.transform.eulerAngles = new Vector3(float.Parse(x.text), float.Parse(y.text), float.Parse(z.text));
    }
    public void onValueChangeScale(string temp)
    {
        currentGameObject.transform.localScale = new Vector3(float.Parse(x.text), float.Parse(y.text), float.Parse(z.text));
    }
    public void newValues(Vector3 v, GameObject obj)
    {
        x.text = v.x.ToString();
        y.text = v.y.ToString();
        z.text = v.z.ToString();

        currentGameObject = obj;
    }
}
