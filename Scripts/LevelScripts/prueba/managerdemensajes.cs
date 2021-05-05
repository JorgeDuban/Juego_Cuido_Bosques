using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class managerdemensajes : MonoBehaviour
{
	// Start is called before the first frame update
	public string message = "";
	public string message2 = "";
	public KeyCode changeMessageKey;

	public GameObject player;
	public bool used = false;

	private mensajedebasura manager;

	void Awake()
	{
		player = GameObject.FindGameObjectWithTag("personaje");
		manager = GameObject.FindGameObjectWithTag("Controladormensajes").GetComponent<mensajedebasura>();
	}

	void OnTriggerEnter(Collider other)
	{
		if ((other.gameObject == player) && !used)
		{
			manager.SetShowMsg(true);
			manager.SetMessage(message);
			used = true;
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject == player)
		{
			manager.SetShowMsg(false);
			Destroy(gameObject);
		}
	}

	private void OnTriggerStay(Collider other)
	{
		if (message2 != "" && other.gameObject == player && Input.GetKeyDown(changeMessageKey))
		{
			manager.SetMessage(message2);
		}
	}
}