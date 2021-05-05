using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PantallaVida : MonoBehaviour
{
    public Text texto;
    public Vida vida;
    public Slider slider;
    public float vidamax = 100;
    public GameObject particulas;


    public CapsuleCollider collideer;
    // Start is called before the first frame update
    void Start()
    {
        collideer.GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {

        texto.text = vida.valor + "/100";
        slider.value = vida.valor;

        if (vida.valor > vidamax)
        {
            vida.valor = vidamax;
            // if (this.GetComponent<Vida>().danoxd == 1)
            /*{
                contadorObjetos = contadorObjetos + 50;
            }*/
        }
        
        else if (vida.valor < 0)
        {
            vida.valor = 0;
        }

    }
    public void OnTriggerEnter(Collider other)
    {

        
        if (other.gameObject.name == "heroTree")
        {

            if (vida.valor >= 100)
            {
                vida.valor = vida.valor + 0;
              
            }
            else
            {
                vida.valor = vida.valor + 80;
                collideer.enabled = false;
                Destroy(particulas.gameObject);

               

            }
        }

    }
    

}
