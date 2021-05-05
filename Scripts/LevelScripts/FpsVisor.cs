using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class FpsVisor : MonoBehaviour
{
    [SerializeField]
    Text fpsText;

    Vector3 initialLineRenderPosition;
    void Start()
    {
        
        InvokeRepeating("LineRenderUpdate", 0, 0.5f);
//____________________________________________________________________
        //fpsText.enabled = false;
    }

    float deltaTime = 0.0f;
    void Update()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        GuiUpdate();
    }


    float fps;
    float msec;
    string text;
    void GuiUpdate()
    {
        msec = deltaTime * 1000.0f;
        fps = 1.0f / deltaTime;
        //fpsTimes.Add(Camera.main.ScreenToWorldPoint(Camera.main.pixelWidth, Camera.main.));
        text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
        fpsText.text = text;
    }

   


}