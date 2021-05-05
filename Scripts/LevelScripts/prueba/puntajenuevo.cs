using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class puntajenuevo : MonoBehaviour
{
    public float valor;
    public puntajenuevo puntajeNuevo;
    public Text texto1;
    GameObject go;
    // Start is called before the first frame update
    private void Awake()
    {
        go = GameObject.Find("Datos");
    }
    void Start()
    {
        valor = go.GetComponent<Guardado>().puntaje;
        
    }

    // Update is called once per frame
    void Update()
    {
        
        go.GetComponent<Guardado>().puntaje = valor;

      texto1.text = "Puntaje: "+puntajeNuevo.valor;
     

    }
}
