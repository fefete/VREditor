using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollView : MonoBehaviour {

    public GameObject item;
    public GridLayoutGroup glgroup;
    public RectTransform scrollContent;
    public ScrollRect scroll;

     void OnEnable()
     {
        //InitializeList();
        //check this for icon http://answers.unity3d.com/questions/234147/editor-gui-object-preview-icon.html  AssetPreview.GetAssetPreview(Object obj)
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

    public void InitializeList()
    {
        ClearOldElements();
        foreach(KeyValuePair<string, GameObject> entry in Manager.getInstance().prefab_dict)
        {
            InitializeNewItem(entry.Key);
        }

        setContentHeight();
    }

    private void InitializeNewItem(string name)
    {
        GameObject newItem = Instantiate(item, glgroup.transform);       
        newItem.name = name;
        newItem.transform.localPosition = Vector3.zero;
        newItem.transform.localRotation = Quaternion.Euler(Vector3.zero);
        newItem.transform.localScale = Vector3.one;

        newItem.GetComponent<Image>().enabled = true;
        newItem.GetComponentInChildren<Text>(true).text = name;
        newItem.GetComponentInChildren<Text>().enabled = true;

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
