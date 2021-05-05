using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuertaNl1 : MonoBehaviour
{
    public Canvas can;
    public Animator anim;
    public GameObject puntajepantalla;
    public Text avisopasarNL1;
    //GameObject BNivel2, PruebaT;
    // Start is called before the first frame update
    void Start()
    {
        can.enabled = false;
        anim.GetComponent<Animator>();
        avisopasarNL1.enabled = false;
        //BNivel2=GameObject.Find("BNivel2");
        //PruebaT = GameObject.Find("PruebaT");
        //BNivel2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void OnTriggerEnter(Collider colision)
    {
        if ((colision.name == "personaje"&& (puntajepantalla.GetComponent<Puntaje>().valor >= 200)))
        {
            // BNivel2.SetActive(true);
            can.enabled = true;
            Cursor.visible = true;
            //Cursor.visible = true;
           Cursor.lockState = CursorLockMode.None;

            //PruebaT.SetActive(true);
            anim.SetBool("Abrir", true);
            //anim.SetBool("Cerrar", false);
            // Cursor.lockState = CursorLockMode.Locked;
            
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
