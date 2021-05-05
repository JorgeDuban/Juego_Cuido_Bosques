using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecogerBasura : MonoBehaviour
{
    public Text avisorecoger;
    public  GameObject puntajepantalla;
    private bool sumarpuntos;
    private Animator animator;

    void Start()

    {
        //can.enabled = false;
        avisorecoger.enabled = false;
        animator = GetComponent<Animator>();

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
                //animator.CrossFadeInFixedTime("recoger", 0.1f);
                sumarpuntos = true;
                if (sumarpuntos)
                {
                    puntajepantalla.GetComponent<Puntaje>().valor += 10;
                    sumarpuntos = false;
                }
                //para acceder a la variable contador y camviarle el valor
               // other.GetComponent<canvasxd>().contadorObjetos = other.GetComponent<canvasxd>().contadorObjetos + 50;
             //   other.GetComponent<MoveBehaviour>().numero = other.GetComponent<MoveBehaviour>().numero + 1;
                Destroy(this.gameObject);
                //animator.CrossFadeInFixedTime("recoger", 0.1f);

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
