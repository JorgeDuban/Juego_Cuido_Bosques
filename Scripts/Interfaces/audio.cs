using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audio : MonoBehaviour
{
    public AudioClip caminar, correr, saltar, recoger;
    AudioSource fuente;
    // Start is called before the first frame update
    void Start()
    {
        fuente = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            fuente.clip = caminar;
            fuente.Play();
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            fuente.clip = caminar;
            fuente.Stop();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            fuente.clip = caminar;
            fuente.Play();
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            fuente.clip = caminar;
            fuente.Stop();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            fuente.clip = caminar;
            fuente.Play();
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            fuente.clip = caminar;
            fuente.Stop();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            fuente.clip = caminar;
            fuente.Play();
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            fuente.clip = caminar;
            fuente.Stop();
        }
        if (Input.GetKeyDown(KeyCode.W) && Input.GetKeyDown(KeyCode.LeftShift
))
        {
            fuente.clip = correr;
            fuente.Play();
        }
        if (Input.GetKeyUp(KeyCode.W) && Input.GetKeyUp(KeyCode.LeftShift
))
        {
            fuente.clip = correr;
            fuente.Stop();
        }
        
        if (Input.GetKeyDown(KeyCode.W) && Input.GetKeyDown(KeyCode.Space
))
        {
            fuente.clip = correr;
            fuente.Play();
        }
        if (Input.GetKeyUp(KeyCode.W) && Input.GetKeyUp(KeyCode.Space
))
        {
            fuente.clip = correr;
            fuente.Stop();

        }
        /*if (Input.GetKeyDown(KeyCode.E))
        {
            fuente.clip = recoger;
            fuente.Play();
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            fuente.clip = recoger;
            fuente.Play();
        }*/
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "basura")
            
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                fuente.clip = recoger;
                fuente.Play();
            }
            if (Input.GetKeyUp(KeyCode.E))
            {
                fuente.clip = recoger;
                fuente.Play();
            }
        }
    }
}
