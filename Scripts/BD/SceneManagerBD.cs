using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneManagerBD : MonoBehaviour
{
    [Header ("Login")]
    [SerializeField] private InputField m_loginPasswordInput = null;
    [SerializeField] private InputField m_loginUserNameInput = null;

    [Header("Register")]

    [SerializeField] private GameObject m_registerUI =      null;
    [SerializeField] private GameObject m_loginUI =         null;
    [SerializeField] private Text m_text =             null;
    [SerializeField] private InputField m_userNameInput =   null;
    [SerializeField] private InputField m_password =        null;
    [SerializeField] private InputField m_reEnterpassword = null;

    [Header("UpdatePuntaje")]
    [SerializeField] private InputField m_userNameInputPuntaje = null;

    private NetworkManager m_networkManager = null;

    private void Awake ()
    {
        m_networkManager = GameObject.FindObjectOfType<NetworkManager>();
    }
 public void SubmitLogin()
    {
        if (m_loginUserNameInput.text == "" || m_loginPasswordInput.text == "")
        {
            m_text.text = "Completa todos los campos";
            return;
        } 
        m_text.text = "Procesando...";

        m_networkManager.CheckUser(m_loginUserNameInput.text, m_loginPasswordInput.text, delegate (Response response)
        {
           
            if(response.message.Equals("Usuario o contraseña incorrectos"))
            {
                m_text.text = response.message;
            }
            else
            {
                SceneManager.LoadScene("Menu");
            }

        });
        
    }

    public void SumitRegister ()
    {
        if(m_userNameInput.text == "" || m_password.text == "" || m_reEnterpassword.text == "")
        {
            m_text.text = "Completa todos los campos";
            return;
            }
     if (m_password.text == m_reEnterpassword.text)
        {
            m_text.text = "Procesando...";

            m_networkManager.CreaterUser(m_userNameInput.text, m_password.text, delegate(Response response)
            {
                m_text.text = response.message;
            });
        }
        else
        {
            m_text.text = "Contraseñas no coinciden";
        }
    }


    public void puntaje()
    {
        Debug.Log("Ejecutar morir");
        m_networkManager.UpdatePuntaje(m_userNameInputPuntaje.text, "203", delegate (Response response)
        {

            if (response.message.Equals("Puntaje insertado exitosamente"))
            {
                Debug.Log(response.message);
            }


        });

    }

public void ShowLogin ()
    {
        m_registerUI.SetActive(false);
        m_loginUI.SetActive(true);
        m_text.text = " ";
    }
    public void ShowRegister()
    {
        m_registerUI.SetActive(true);
        m_loginUI.SetActive(false);
        m_text.text = " ";
    }
}
