using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListItem : MonoBehaviour {

    public Text assetName;
    public ScrollView scview;
    private bool init = false;
    
    // Use this for initialization
    void Update()
    {
        if (!init)
        {
            assetName.text = transform.name;
            GetComponent<Text>().text = transform.name;
            init = true;
        }
    }
    // Update is called once per frame
    public void OnRemoveMe()
    {
        DestroyImmediate(gameObject);
        scview.setContentHeight();
    }
}
