using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateCollector : MonoBehaviour
{

    public InputField x;
    public InputField y;
    public InputField z;

    public void onValueChangePos(string temp)
    {
        float new_x = GetFloat(x.text, 0);
        float new_y = GetFloat(y.text, 0);
        float new_z = GetFloat(z.text, 0);
        if (Manager.getInstance().getObject() != null)
            Manager.getInstance().updateObjInUsePos(new Vector3(new_x, new_y, new_z));
    }
    public void onValueChangeRot(string temp)
    {
        float new_x = GetFloat(x.text, 0);
        float new_y = GetFloat(y.text, 0);
        float new_z = GetFloat(z.text, 0);
        if (Manager.getInstance().getObject() != null)
            Manager.getInstance().updateObjInUseRot(new Vector3(new_x, new_y, new_z));
    }
    public void onValueChangeScale(string temp)
    {
        float new_x = GetFloat(x.text, 0);
        float new_y = GetFloat(y.text, 0);
        float new_z = GetFloat(z.text, 0);
        if (Manager.getInstance().getObject() != null)
            Manager.getInstance().updateObjInUseSca(new Vector3(new_x, new_y, new_z));
    }
    public void newValues(Vector3 v)
    {
        x.text = v.x.ToString();
        y.text = v.y.ToString();
        z.text = v.z.ToString();
    }

    private float GetFloat(string stringValue, float defaultValue)
    {
        float result = defaultValue;
        float.TryParse(stringValue, out result);
        return result;
    }


}
