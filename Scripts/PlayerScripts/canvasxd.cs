
using UnityEngine;
using UnityEngine.UI;
public class canvasxd : MonoBehaviour
{
    // Barra vida
    public float Hp;
    public float Hpmax = 100;
    public Slider barravida;
    GameObject go;
    //Puntaje
    // public float contadorObjetos = 0.0f;
    // public Text contPuntage;

    // Start is called before the first frame update

    void Start()
    {
        Hp = Hpmax;
        go = GameObject.Find("Datos");
        
    }

    // Update is called once per frame
    void Update()
    {
       //morir();
        // contPuntage.text = "Puntaje: " + contadorObjetos.ToString();
        // barra de vida 
        barravida.value = Hp;
       go.GetComponent<Guardado>().vida=Hp;
        if (Hp > Hpmax)
        {
            Hp = Hpmax;
           // if (this.GetComponent<Vida>().danoxd == 1)
            /*{
                contadorObjetos = contadorObjetos + 50;
            }*/
        }
        else if (Hp < 0)
        {
            Hp = 0;
        }
        
   }
    
  /* private NetworkManager m_networkManager = null;

   private void Awake()
   {
       m_networkManager = GameObject.FindObjectOfType<NetworkManager>();
   }
   public void morir()
   {
       Debug.Log("Ejecutar morir");
       m_networkManager.UpdatePuntaje("Jorge", "203", delegate (Response response)
       {

           if (response.message.Equals("Puntaje insertado exitosamente"))
           {
               Debug.Log(response.message);
           }


       });

   }*/
    public void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.name == "ObjetoMalo")
        {
            Hp = Hp - 20;
            Destroy(other.gameObject);
        }
        if (other.gameObject.name == "bomba")
        {
            Hp = Hp - 100;
            Destroy(other.gameObject);
        }

        if (other.gameObject.name == "VidaMax")
        {
            Hp = Hp + 100;
            Destroy(other.gameObject);
        }
        if (other.gameObject.name == "heroTree(1)")
        {
            Hp = Hp + 80;
        }

    }
}
