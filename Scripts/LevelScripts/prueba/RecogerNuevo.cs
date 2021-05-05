using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecogerNuevo : MonoBehaviour
{
    public Text avisorecoger;
    public GameObject puntajepantalla;
    private bool sumarpuntos;

    void Start()

    {
        //can.enabled = false;


        avisorecoger.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnTriggerStay(Collider other)
    {
        if (other.name == "personaje")
        {
            avisorecoger.enabled = true;
            if (Input.GetKey(KeyCode.E))
            {
                sumarpuntos = true;
                if (sumarpuntos)
                {
                    puntajepantalla.GetComponent<puntajenuevo>().valor += 10;
                    sumarpuntos = false;
                }
                //para acceder a la variable contador y camviarle el valor
                // other.GetComponent<canvasxd>().contadorObjetos = other.GetComponent<canvasxd>().contadorObjetos + 50;
                //   other.GetComponent<MoveBehaviour>().numero = other.GetComponent<MoveBehaviour>().numero + 1;
                Destroy(this.gameObject);

                avisorecoger.enabled = false;




            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.name == "personaje")
        {
            avisorecoger.enabled = false;

        }
    }
}
