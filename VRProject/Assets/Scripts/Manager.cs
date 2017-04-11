using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{

    static Manager instance = null;
    public GameObject obj_in_use;
    //public bool has_to_reset_values;

    public string material_bundle_name;
    public string prefab_bundle_name;
    public string scene_bundle_name;

    public UpdateCollector pos;
    public UpdateCollector rot;
    public UpdateCollector sca;

    public GameObject inspector_ui_;
    public GameObject mat_selection_ui_;
    public GameObject scene_selection_ui_;
    public GameObject prefab_selection_ui_;
    public GameObject keyboard_ui_;

    public GameObject user_;

    public Dictionary<string, GameObject> prefab_dict;
    public Dictionary<string, Material> mat_dict;
    public string[] scenes_dict;
    public Dictionary<int, JsonData> changelog;

    public ScrollView prefabDataShowing;
    public ScrollView sceneDataShowing;
    public ScrollView materialDataShowing;


    AssetBundle myLoadedBundle;

    public Camera UiCamera;

    public GameObject inspectorArrow;

    // Use this for initialization

    private void Start()
    {
        //removeObject();
        inspector_ui_.SetActive(false);
        keyboard_ui_.SetActive(false);
        prefab_selection_ui_.SetActive(false);
        mat_selection_ui_.SetActive(false);
        scene_selection_ui_.SetActive(true);

    }

    void Awake()
    {
        if (instance == null)
        {
            Caching.CleanCache();
            DontDestroyOnLoad(this);
            prefab_dict = new Dictionary<string, GameObject>();
            mat_dict = new Dictionary<string, Material>();
            changelog = new Dictionary<int, JsonData>();
            StartCoroutine(LoadAssetBundleOnApp(material_bundle_name, "material"));
            StartCoroutine(LoadAssetBundleOnApp(prefab_bundle_name, "prefab"));
            StartCoroutine(LoadAssetBundleOnApp(scene_bundle_name, "scenes"));
            instance = this;
            obj_in_use = null;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }


    void Update()
    {

        if (Input.GetButton("Fire2"))
        {
            removeObject();
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            ExportChanges();
        }
        if (Input.GetButton("Fire3"))
        {
            obj_in_use.transform.position = user_.transform.position;
            pos.newValues(new Vector3(user_.transform.position.x, user_.transform.position.y, user_.transform.position.z));
        }
    }

    public void removeObject()
    {

        obj_in_use = null;
        pos.newValues(new Vector3(0, 0, 0));
        rot.newValues(new Vector3(0, 0, 0));
        sca.newValues(new Vector3(0, 0, 0));
        inspector_ui_.SetActive(false);
        mat_selection_ui_.SetActive(false);
        keyboard_ui_.SetActive(false);
        prefab_selection_ui_.SetActive(true);
    }

    public void setObject(GameObject obj)
    {
        obj_in_use = obj;
        pos.newValues(obj_in_use.transform.position);
        rot.newValues(obj_in_use.transform.rotation.eulerAngles);
        sca.newValues(obj_in_use.transform.localScale);
        keyboard_ui_.SetActive(true);
        inspector_ui_.SetActive(true);
        mat_selection_ui_.SetActive(true);
        inspector_ui_.SetActive(true);
        prefab_selection_ui_.SetActive(false);
    }

    public GameObject getObject()
    {
        return obj_in_use;

    }

    public static Manager getInstance()
    {
        return instance;
    }

    public void spawnObject(string obj)
    {
        GameObject go = Instantiate(prefab_dict[obj]);
        removeObject();
        setObject(go);
        createEntryInChangelog(go);

        // name =! string.empty == new object
        changelog[go.GetInstanceID()].obj_name = obj;

    }

    public void changeMaterialToCurrentObject(string mat_name)
    {
        obj_in_use.GetComponent<Renderer>().material = mat_dict[mat_name];
        if (!changelog.ContainsKey(obj_in_use.GetInstanceID()))
        {
            createEntryInChangelog(obj_in_use);
        }
        changelog[obj_in_use.GetInstanceID()].mat_name = mat_name;

    }

    public void loadScene(string scene)
    {
        scene_selection_ui_.SetActive(false);
        prefab_selection_ui_.SetActive(true);
        SceneManager.LoadScene(scene);
    }
    //"/AssetBundles/assetstoimport"
    IEnumerator LoadAssetBundleOnApp(string file, string type)
    {
        string path = "File://" + Application.dataPath + file;
        WWW www = WWW.LoadFromCacheOrDownload(path, 2);

        print("Loading");


        //waiting for completion
        yield return www;
        if (!string.IsNullOrEmpty(www.error))
        {
            Debug.LogError(www.error);
            yield return null;
        }

        myLoadedBundle = www.assetBundle;


        if (type == "prefab")
        {
            GameObject[] loadedObjs_obj = myLoadedBundle.LoadAllAssets<GameObject>();
            for (int i = 0; i < loadedObjs_obj.Length; i++)
            {
                prefab_dict.Add(loadedObjs_obj[i].name, loadedObjs_obj[i]);
            }
            prefabDataShowing.InitializeList(type);

        }
        else if (type == "material")
        {
            Material[] loadedObjs_obj = myLoadedBundle.LoadAllAssets<Material>();
            for (int i = 0; i < loadedObjs_obj.Length; i++)
            {
                mat_dict.Add(loadedObjs_obj[i].name, loadedObjs_obj[i]);
            }
            materialDataShowing.InitializeList(type);

        }
        else if (type == "scenes")
        {
            scenes_dict = myLoadedBundle.GetAllScenePaths();
            sceneDataShowing.InitializeList(type);
        }

        www.Dispose();
    }

    private void createEntryInChangelog(GameObject obj)
    {
        JsonData temp = new JsonData();
        changelog[obj.GetInstanceID()] = temp;

        temp.old_x = obj.transform.position.x;
        temp.old_y = obj.transform.position.y;
        temp.old_z = obj.transform.position.z;

        temp.t_x = obj.transform.position.x;
        temp.t_y = obj.transform.position.y;
        temp.t_z = obj.transform.position.z;

        temp.r_x = obj.transform.rotation.x;
        temp.r_y = obj.transform.rotation.y;
        temp.r_z = obj.transform.rotation.z;

        temp.s_x = obj.transform.localScale.x;
        temp.s_y = obj.transform.localScale.y;
        temp.s_z = obj.transform.localScale.z;

        temp.mat_name = string.Empty;
        temp.obj_name = string.Empty;

    }

    public void updateObjInUsePos(Vector3 newpos)
    {
        obj_in_use.transform.position = newpos;
        if (!changelog.ContainsKey(obj_in_use.GetInstanceID()))
        {
            createEntryInChangelog(obj_in_use);
        }
        changelog[obj_in_use.GetInstanceID()].t_x = newpos.x;
        changelog[obj_in_use.GetInstanceID()].t_y = newpos.y;
        changelog[obj_in_use.GetInstanceID()].t_z = newpos.z;


    }
    public void updateObjInUseRot(Vector3 newRot)
    {
        obj_in_use.transform.rotation = Quaternion.Euler(newRot);
        if (!changelog.ContainsKey(obj_in_use.GetInstanceID()))
        {
            createEntryInChangelog(obj_in_use);
        }
        changelog[obj_in_use.GetInstanceID()].r_x = newRot.x;
        changelog[obj_in_use.GetInstanceID()].r_y = newRot.y;
        changelog[obj_in_use.GetInstanceID()].r_z = newRot.z;


    }
    public void updateObjInUseSca(Vector3 newSca)
    {
        obj_in_use.transform.localScale = newSca;
        if (!changelog.ContainsKey(obj_in_use.GetInstanceID()))
        {
            createEntryInChangelog(obj_in_use);
        }
        changelog[obj_in_use.GetInstanceID()].s_x = newSca.x;
        changelog[obj_in_use.GetInstanceID()].s_y = newSca.y;
        changelog[obj_in_use.GetInstanceID()].s_z = newSca.z;


    }

    public void ExportChanges() {
        JsonData[] finalJson = new JsonData[changelog.Count];
        finalJson.Initialize();
        int counter = 0;
        foreach (KeyValuePair<int, JsonData> item in changelog) {
            finalJson[counter] = item.Value;
            counter++;
        }

        string toJson = JSonHelper.ToJson<JsonData>(finalJson);

        string path = Application.dataPath + "/exportData.txt";
        System.IO.File.WriteAllText(Application.dataPath + "/exportData.txt", toJson);
    }
}
    