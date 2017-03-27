using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
public class ImportFile : MonoBehaviour
{

    [MenuItem("Edit/ImportModifiedData")]
    static void ImportChangesFromFile()
    {
        string loadedText = System.IO.File.ReadAllText(Application.dataPath + "/exportData.txt");
        JsonData[] objects = JSonHelper.FromJson<JsonData>(loadedText);
        List<GameObject> p = new List<GameObject>();

        //first pass, taking the old objects and disabling them, in order to move them in a later stage
        foreach (JsonData data in objects)
        {
            if (data.obj_name == "") {
                //existing object
                Vector3 oldPos = Vector3.zero;
                float radius = (2.0f);
                Collider[] objsInSphere = Physics.OverlapSphere(oldPos, radius);
                if (objsInSphere.Length > 0)
                {
                    // found something
                    Debug.Log("Found Something");
                    p.Add(objsInSphere[0].gameObject);
                    objsInSphere[0].gameObject.SetActive(false);
                }
            }
        }
        int listCounter = 0;
        foreach (JsonData data in objects)
        {
            if (data.obj_name == "")
            {
                //existing object
                p[listCounter].SetActive(true);
                p[listCounter].transform.position = new Vector3(data.t_x, data.t_y, data.t_z);
                p[listCounter].transform.rotation = Quaternion.Euler(new Vector3(data.r_x, data.r_y, data.r_z));
                p[listCounter].transform.localScale = new Vector3(data.s_x, data.s_y, data.s_z);
                if (data.mat_name != "") {
                    //change material
                    Material m = (Material)AssetDatabase.LoadAssetAtPath("Assets/Content/Materials/" + data.mat_name + ".mat", typeof(Material));
                    if (m) {
                        p[listCounter].GetComponent<Renderer>().material = m;
                    }
                }
                listCounter++;
            }
            else {
                GameObject temp = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Content/Prefabs/" + data.obj_name + ".prefab", typeof(GameObject));
                Debug.Log(temp);
                GameObject temp2 = GameObject.Instantiate(temp);
                temp2.SetActive(true);
                temp2.transform.position = new Vector3(data.t_x, data.t_y, data.t_z);
                temp2.transform.rotation = Quaternion.Euler(new Vector3(data.r_x, data.r_y, data.r_z));
                temp2.transform.localScale = new Vector3(data.s_x, data.s_y, data.s_z);
                if (data.mat_name != "")
                {
                    //change material
                    Material m = (Material)AssetDatabase.LoadAssetAtPath("Assets/Content/Materials/" + data.mat_name + ".mat", typeof(Material));
                    if (m)
                    {
                        temp2.GetComponent<Renderer>().material = m;
                    }
                }
            }
        }

    }
}
#endif