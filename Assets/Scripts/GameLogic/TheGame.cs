using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class LevelInfo
{
    public string sceneName;
    public string name;
    public string description;
    public Texture2D image;
}

public class TheGame : MonoBehaviour
{
    private static TheGame instance;
    public static TheGame Instance
    {
        get
        {
            return instance;
        }
    }

    void Awake()
    {
        instance = this;
    }
    
    public List<LevelInfo> levels = new List<LevelInfo>();
}
