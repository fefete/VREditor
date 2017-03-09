using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListItem : MonoBehaviour, IVRInteractuable {

    public string assetName;
    public ScrollView scview;
    public GameObject ob_to_look_at;
    public string type;

    public void LateUpdate()
    {
        Vector2 vec2ScreenPoint = new Vector2(transform.position.x, transform.position.y);
        if (RectTransformUtility.RectangleContainsScreenPoint(scview.GetComponent<RectTransform>(), vec2ScreenPoint)) {
            GetComponent<BoxCollider>().enabled = true;

        }
        else
        {
            GetComponent<BoxCollider>().enabled = false;
        }

    }

    public void onGazeIn() {
    }
    public void onGazeOver() {
    }
    public void onGazeOut() {

    }


    public void action()
    {
        if (type == "prefab")
        {
            Manager.getInstance().spawnObject(assetName);

        }
        else if (type == "material")
        {
            Manager.getInstance().obj_in_use.GetComponent<Renderer>().material = Manager.getInstance().mat_dict[assetName];

        }
        else if (type == "scenes")
        {

            Manager.getInstance().loadScene(assetName);

        }
    }

}
