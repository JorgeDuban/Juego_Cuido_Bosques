using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class armacambiar : MonoBehaviour
{
    public GameObject xd, xd2;
    public bool xdd, xdd2;
    public GameObject pistola, pistola2;
    private LogicaArma daño;
    private LogicaArma daño2;


    // Start is called before the first frame update
    void Start()
    {

        xd = GameObject.Find("Pistol");
        xd.SetActive(true);
        xd2 = GameObject.Find("Pistol2");
        xd2.SetActive(false);
        xdd = true;
        xdd2 = false;
        pistola.SetActive(true);
        pistola2.SetActive(false);
        daño = GetComponent<LogicaArma>();
        daño.daño = 20;
        daño2 = GetComponent<LogicaArma>();
        daño2.daño2 = 0;

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Q))
        {
            validar();
        }
    }

    void validar()
    {
        if (xdd)   ///la verde
        {
            xd.SetActive(false);
            xd2.SetActive(true);
            xdd = false;
            xdd2 = true;
            pistola.SetActive(false);
            pistola2.SetActive(true);
            daño.daño = 0;  //enemigos rojos
            daño2.daño2 = 20;  //enemigos verdes
        }
        else
        {
            if (xdd2)   //la azul
            {

                xd.SetActive(true);
                xd2.SetActive(false);
                xdd = true;
                xdd2 = false;
                pistola.SetActive(true);
                pistola2.SetActive(false);
                daño.daño = 20;  //enemigos rojos
                daño2.daño2 = 0;  //enemigos verdes
            }
        }
    }
}
