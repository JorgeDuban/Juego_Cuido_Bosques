using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   

public class Puntaje : MonoBehaviour
{
    public float valor;
    public Puntaje puntaje;
    public Text texto;
    GameObject go;
    // Start is called before the first frame update
    void Start()
    {
        valor = 0;
        go = GameObject.Find("Datos");
    }

    // Update is called once per frame
    void Update()
    {
        go.GetComponent<Guardado>().puntaje = valor;
        texto.text = "Puntaje: " + puntaje.valor;
    }
}
