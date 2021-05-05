using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class apuntar : MonoBehaviour
{
	// Use this for initialization
	public Transform objeto;
	void Start()
	{

	}

	// Update is called once per frame
	void LateUpdate()
	{
		if (Input.GetMouseButton(1))
		{
			transform.rotation = objeto.rotation;
		}
	}
}
