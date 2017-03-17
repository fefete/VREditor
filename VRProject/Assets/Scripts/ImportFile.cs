using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
public class ImportFile : MonoBehaviour {

    [MenuItem("Edit/ImportModifiedData")]
    static void ImportChangesFromFile() {
        string loadedText = System.IO.File.ReadAllText(Application.dataPath + "/exportData.txt");
    }
}
#endif