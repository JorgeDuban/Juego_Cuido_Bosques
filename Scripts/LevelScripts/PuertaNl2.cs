using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PuertaNl2 : MonoBehaviour
{
    public Canvas can;
    public Animator anim;
    public GameObject puntajepantalla;
    public Text avisopasarNL1;
    //GameObject PruebaBoton;
    // Start is called before the first frame update
    void Start()
    {
        can.enabled = false;
        anim.GetComponent<Animator>();
        avisopasarNL1.enabled = false;
        //BNivel2=GameObject.Find("BNivel2");
        //PruebaBoton = GameObject.Find("PruebaBoton");
        //PruebaBoton.SetActive(false);
        //BNivel2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void OnTriggerEnter(Collider colision)
    {
        if ((colision.name == "personaje") && (puntajepantalla.GetComponent<puntajenuevo>().valor >= 500))
        {
            // BNivel2.SetActive(true);
            can.enabled = true;
            
            //PruebaBoton.SetActive(true);
            anim.SetBool("Abrir", true);
            //anim.SetBool("Cerrar", false);
            Cursor.visible = true;
            //Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
       else
        {
            avisopasarNL1.enabled = true;

        }
    }
   private void OnTriggerExit(Collider colision)
    {
        if (colision.name == "personaje")
        {
            avisopasarNL1.enabled = false;
        }
    }
}
