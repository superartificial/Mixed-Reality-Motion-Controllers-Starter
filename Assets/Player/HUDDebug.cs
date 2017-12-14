using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDDebug : MonoBehaviour {

    private Text hudDebugText;
    private static string debugOuputString = "";

    private static HUDDebug ThisInstance = null;

    //Property for accessing single instance
    public static HUDDebug Instance
    {
        get
        {
            if (ThisInstance == null)
            {
                GameObject HUDDebugObject = new GameObject("HUDDebug");
                ThisInstance = HUDDebugObject.AddComponent<HUDDebug>();
            }

            return ThisInstance;
        }
    }

    void Awake()
    {
        //If single object already exists then destroy
        if (ThisInstance != null)
        {
            DestroyImmediate(gameObject);
            return;
        }
        //Make this single instance
        ThisInstance = this;
    }

    // Use this for initialization
    void Start () {
        hudDebugText = GetComponentInChildren<Text>();
    }
	
	// Update is called once per frame
	void LateUpdate () {
        hudDebugText.text = "DEBUG: \n" + debugOuputString;
        debugOuputString = "";
    }

    public static void AddMessage(string message)
    {
        debugOuputString += message + "\n";
    }
}
