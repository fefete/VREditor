using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    public Dictionary<string, GameObject> prefab_dict;
    public Dictionary<string, Material> mat_dict;
    public string[] scenes_dict;

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
            DontDestroyOnLoad(this);
            prefab_dict = new Dictionary<string, GameObject>();
            mat_dict = new Dictionary<string, Material>();
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

        if (Input.GetKeyDown("escape"))
        {
            removeObject();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            prefab_selection_ui_.GetComponentInChildren<Scrollbar>().value = prefab_selection_ui_.GetComponentInChildren<Scrollbar>().value + 0.1f;
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            prefab_selection_ui_.GetComponentInChildren<Scrollbar>().value = prefab_selection_ui_.GetComponentInChildren<Scrollbar>().value + 0.1f;
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
        WWW www = WWW.LoadFromCacheOrDownload("File://" + Application.dataPath + file, 2);

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
}
