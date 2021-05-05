using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gaficos : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Graficos1()
    {
        QualitySettings.currentLevel = QualityLevel.Fastest;
    }
    public void Graficos2()
    {
        QualitySettings.currentLevel = QualityLevel.Fast;
    }
    public void Graficos3()
    {
        QualitySettings.currentLevel = QualityLevel.Simple;
    }
    public void Graficos4()
    {
        QualitySettings.currentLevel = QualityLevel.Good;
    }
    public void Graficos5()
    {
        QualitySettings.currentLevel = QualityLevel.Beautiful;
    }
    public void Graficos6()
    {
        QualitySettings.currentLevel = QualityLevel.Fantastic;
    }
}
