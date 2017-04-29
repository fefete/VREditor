using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollView : MonoBehaviour {

    public GameObject item;
    public GridLayoutGroup glgroup;
    public RectTransform scrollContent;
    public ScrollRect scroll;
    // if is a prefabs, material or scene list.
    public string listType;

     void OnEnable()
     {

     }



    private void ClearOldElements()
    {
        for(int i = 0; i< glgroup.transform.childCount; i++)
        {
            Destroy(glgroup.transform.GetChild(i).gameObject);
        }
    }

    public void setContentHeight()
    {
        float scrollContentHeight = (glgroup.transform.childCount * glgroup.cellSize.y) + ((glgroup.transform.childCount - 1) * glgroup.spacing.y);
        scrollContent.sizeDelta = new Vector2(scrollContent.sizeDelta.x, scrollContentHeight);
    }

    public void InitializeList(string type)
    {
        ClearOldElements();

        if (type == "prefab")
        {
            foreach (KeyValuePair<string, GameObject> entry in Manager.getInstance().prefab_dict)
            {
                InitializeNewItem(entry.Key, type);
            }

        }
        else if (type == "material")
        {
            foreach (KeyValuePair<string, Material> entry in Manager.getInstance().mat_dict)
            {
                InitializeNewItem(entry.Key, type);
            }
        }
        else if(type == "scenes")
        {
            foreach (string n in Manager.getInstance().scenes_dict)
            {
                InitializeNewItem(n, type);
            }

        }
        setContentHeight();
    }

    private void InitializeNewItem(string name, string type)
    {
        GameObject newItem = Instantiate(item, glgroup.transform);       
        newItem.name = name;
        newItem.transform.localPosition = Vector3.zero;
        newItem.transform.localRotation = Quaternion.Euler(Vector3.zero);
        newItem.transform.localScale = Vector3.one;

        newItem.GetComponent<Image>().enabled = true;
        string[] n = name.Split('/');
        newItem.GetComponentInChildren<Text>(true).text = n[n.Length - 1];
        newItem.GetComponentInChildren<Text>().enabled = true;
        newItem.GetComponentInChildren<ListItem>().scview = this;
        newItem.GetComponentInChildren<ListItem>().assetName = name;
        newItem.GetComponentInChildren<ListItem>().type = type;

        newItem.SetActive(true);
    }

    private IEnumerator MoveTowardsTarget(float time, float from, float target)
    {
        float i = 0;
        float rate = 1 / time;
        while (i < 1)
        {
            i += rate * Time.deltaTime;
            scroll.verticalNormalizedPosition = Mathf.Lerp(from, target, i);
            yield return 0;
        }
    }
}
