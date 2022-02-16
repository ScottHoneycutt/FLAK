using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class TempDeleter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
        File.Delete(Application.persistentDataPath + "/playerInfo.dat");

        RefreshEditorProjectWindow();
        file.Close();
    }
    void RefreshEditorProjectWindow()
    {
#if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh();
#endif
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
