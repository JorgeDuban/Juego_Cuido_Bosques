using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LogicaJugador : MonoBehaviour
{
    public Vida vida;
    public bool Vida0 = false;
    public Canvas can, canarbol, canbasu;
    GameObject datos;
    public string nombre;


    //  [SerializeField] private Animator animadorPerder;
    public Puntaje puntaje;
    // Start is called before the first frame update
    private NetworkManager m_networkManager = null;
    void Start()
    {

        m_networkManager = GameObject.FindObjectOfType<NetworkManager>();
        vida = GetComponent<Vida>();
        puntaje.valor = 0;
        can.enabled = false;
        canarbol.enabled = false;
        canbasu.enabled = false;
        nombre = datos.GetComponent<GuardodoDeombre>().nom;

    }

    // Update is called once per frame
    void Update()
    {
        RevisarVida();
    }


    void RevisarVida()
    {
        if (Vida0) return;
        if (vida.valor <= 0)
        {
            AudioListener.volume = 0f;
            //  animadorPerder.SetTrigger("Mostrar");
            Vida0 = true;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            can.enabled = true;
            // puntajes();

            //Invoke("ReiniciarJuego", 2f); //2f
        }
    }


    private void Awake()
    {
        datos = GameObject.Find("NombreUsuario");

        m_networkManager = GameObject.FindObjectOfType<NetworkManager>();
    }
    public void puntajes()
    {
        Debug.Log(nombre);
        m_networkManager.UpdatePuntaje("gamer", puntaje.valor.ToString(), delegate (Response response)
        {
            Debug.Log("Ejecutado");
            if (response.message.Equals("Puntaje insertado exitosamente"))
            {
                Debug.Log(response.message);
            }


        });

    }
    void ReiniciarJuego()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        // SceneManager.LoadScene("nivel 1");
        puntaje.valor = 0;
        AudioListener.volume = 1f;
    }
    public void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "vida")
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
            canarbol.enabled = true;
        }
       
    

       

        if (other.gameObject.tag == "basu")
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
            canbasu.enabled = true;
            
        }


    }
   

}
