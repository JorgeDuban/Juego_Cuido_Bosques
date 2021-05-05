using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Convertir: MonoBehaviour
{
    // Start is called before the first frame update
    public Text Vida;
    public Text Puntaje;
    GameObject go;
    void Start()
    {
        
        go = GameObject.Find("Datos");
            
    }

    // Update is called once per frame
    void Update()
    {
        Vida.text =go.GetComponent<Guardado>().vida.ToString() + "/100";
        Puntaje.text = go.GetComponent<Guardado>().puntaje.ToString();
    }
}
