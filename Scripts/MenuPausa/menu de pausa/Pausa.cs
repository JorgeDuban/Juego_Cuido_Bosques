using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pausa : MonoBehaviour
{
	public GameObject ObjPausa;
	// Start is called before the first frame update
	void Start()
    {
		ObjPausa.SetActive(false);
	}

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Cambio();


		}
		
	}

	public void Cambio()
	{
		if (Time.timeScale == 1) { 
			Pausear();
		}
		else if (Time.timeScale == 0) { 
			
		
			Continuar();
		}

	}

	public void Pausear()
	{
		ObjPausa.SetActive(true);
		Time.timeScale = 0;
	}

	public void Continuar()
	{
		ObjPausa.SetActive(false);
		Time.timeScale = 1;
		Cursor.lockState = CursorLockMode.Confined;
		Cursor.visible = false;


	}

}
