using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PasarEscena : MonoBehaviour
{
    public Canvas canarbol;
    public Canvas canbasu;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void CambiarEscenaInicio()
    {
        SceneManager.LoadScene("Menu");
    }
    public void CambiarEscenaNivel1()
    {
        SceneManager.LoadScene("Nivel 1");

    }
    public void CambiarEscenaNivel2()
    {
        SceneManager.LoadScene("Nivel 2");

    }
    public void CambiarEscenaNivel3()
    {
        SceneManager.LoadScene("Nivel 3");

    }
    public void CambiarEscenaHistoria()
    {
        SceneManager.LoadScene("historia");

    }
    public void CambiarEscenaInstrucciones2()
    {
        SceneManager.LoadScene("Instrucciones2");

    }
    public void CambiarEscenaInstrucciones3()
    {
        SceneManager.LoadScene("Instrucciones3");

    }
    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Has Salido");

    }
    public void vidaarbol()
    {
        canarbol.enabled = false;
        Time.timeScale = 1;
        Destroy(canarbol.gameObject);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.None;
    }
    public void basur()
    {
        canbasu.enabled = false;
        Time.timeScale = 1;
        Destroy(canbasu.gameObject);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.None;
    }

}

