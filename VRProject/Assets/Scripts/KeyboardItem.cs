using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyboardItem : MonoBehaviour, IVRInteractuable {

    public bool gaze_in;
    private double timer;
    public string value;

    private void OnEnable()
    {
        if (GetComponentInChildren<Text>()) {
            GetComponentInChildren<Text>().text = value;
        }
    }

    public void onGazeIn()
    {
        Debug.Log("GAZE IN"); 
        gaze_in = true;
        //GetComponent<Image>().material = GetComponentInParent<KeyboardDataManager>().onOverMaterial;
        //GetComponent<Image>().material.color = Color.grey;

    }
    public void onGazeOver()
    {
        //GetComponent<Image>().material = GetComponentInParent<KeyboardDataManager>().onOverMaterial;
    }
    public void onGazeOut()
    {
        Debug.Log("GAZE OUT");
        gaze_in = false;
        timer = 0.0f;
        //GetComponent<Image>().material = GetComponentInParent<KeyboardDataManager>().onOutMaterial;
        //GetComponent<Image>().material.color = Color.black;

    }


    public void action()
    {
        InputField f = Manager.getInstance().inspectorArrow.GetComponent<ArrowDataManager>().modifier.GetComponent<InputField>();
        f.ActivateInputField();
        string final_value = "0";
        if (value == "del" && f.text.Length > 0)
        {
            f.text = f.text.Remove(f.text.Length - 1, 1);
        }
        else if (value == ".")
        {
            f.text = f.text.Insert(f.text.Length, ".");

        }
        else if (value == "minus")
        {
            f.text = f.text.Insert(0, "-");
        }
        else
        {
            final_value = f.text + value;
            f.text = final_value;
        }
        if (f.text.StartsWith("0") && !f.text.Contains(".") && f.text.Length > 1)
            f.text = f.text.Remove(0, 1);

        f.DeactivateInputField();
    }
}
