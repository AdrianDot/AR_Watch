// ####################################################
// # AR-Watch Demo by Adrian Schroeder @Adrian_Schr   #
// # Date: 02-17-2021                                 #
// ####################################################

//#define DEBUGGING

using UnityEngine;
using UnityEngine.UI;
using System;


public class ShowDebugLines : MonoBehaviour
{
    [SerializeField] private GameObject clearLogButton;
    [HideInInspector] public string debugText;
    private int debugNumber = 1;

    void Awake()
    {
#if DEBUGGING
        clearLogButton.SetActive(true);
#endif
    }

    public void OnGUI()
    {
        GUIStyle myStyle = new GUIStyle();
        myStyle.fontSize = 25;
        myStyle.normal.textColor = Color.black;
#if DEBUGGING
        GUI.Label (new Rect (10, 20, 1300, 100), debugText, myStyle);
#endif
    }

    public void ChangeDebugText(string text)
    {        
        int numLines = debugText.Split('\n').Length;
        if(numLines >= 20)
        {
            Int32 amountToSplit = 2;
            string[] splitText = debugText.Split(new char[]{'\n'}, amountToSplit);
            debugText = splitText[1];
        }
        debugText += "\n" + debugNumber.ToString() + ": " + text;   
        debugNumber++;     
    }

    public void ClearDebugText()
    {
        debugText = "";
        debugNumber = 1;
    }
    
}
