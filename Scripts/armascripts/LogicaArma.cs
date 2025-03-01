﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ModoDeDisparo
{
    SemiAuto,
    FullAuto
}


public class LogicaArma : MonoBehaviour
{


    protected Animator animator;
    protected AudioSource audioSource;

    public bool tiempoNoDisparo = false;
    public bool puedeDisparar = false;
    public bool recargando = false;

    [Header("Referencia de Objetos")]
    //public GameObject particula;
    //public Transform posicion;

    // public Camera camaraPrincipal;
    //public Transform puntoDeDisparo;
    public GameObject efectoDañoPrefab;

    [Header("Referencia de Sonidos")]
    public AudioClip sonDisparo;
    public AudioClip sonSinBalas;
    public AudioClip sonCartuchoEntra;
    public AudioClip sonCartuchoSale;
    public AudioClip sonVacio;
    public AudioClip sonDesenfundar;

    [Header("Atributos de Arma")]
    public ModoDeDisparo modoDeDisparo = ModoDeDisparo.FullAuto;
    public float daño = 0f;
    public float daño2 = 0f;
    public float ritmoDeDisparo = 0.3f;
    public int balasRestantes;
    public int balasEnCartucho;
    public int tamañoDelCartucho = 12;
    public int maximoDeBalas = 200;
    // public bool estaADS = false;
    //public Vector3 disCadera;
    //public Vector3 ADS;
    //public float tiempoApuntar;
    //public float zoom;
    //public float normal;


    // Start is called before the first frame update
    void Start()
    {

        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();


        balasEnCartucho = tamañoDelCartucho;
        balasRestantes = maximoDeBalas;


        Invoke("HabilitarArma", 0.5F);
    }

    // Update is called once per frame
    void Update()
    {

        if (modoDeDisparo == ModoDeDisparo.FullAuto && Input.GetMouseButton(1) && Input.GetButton("Fire1"))
        {

            RevisarDisparo();

        }
        else if (modoDeDisparo == ModoDeDisparo.SemiAuto && Input.GetMouseButton(1) && Input.GetButtonDown("Fire1"))
        {

            RevisarDisparo();
        }
        if (Input.GetButtonDown("Reload"))
        //if (Input.GetKey(KeyCode.R))
        {
            RevisarRecargar();

        }
        /*
        if (Input.GetMouseButton(1)) //1 es el clic derecho, 0 es e clic izquierdo
        {
            transform.localPosition = Vector3.Slerp(transform.localPosition, ADS, tiempoApuntar * Time.deltaTime);
            estaADS = true;
            camaraPrincipal.fieldOfView = Mathf.Lerp(camaraPrincipal.fieldOfView, zoom, tiempoApuntar * Time.deltaTime);
        }

        if (Input.GetMouseButtonUp(1))
        {
            estaADS = false;
        }
        if (estaADS == false)
        {
            transform.localPosition = Vector3.Slerp(transform.localPosition, disCadera, tiempoApuntar * Time.deltaTime);
            camaraPrincipal.fieldOfView = Mathf.Lerp(camaraPrincipal.fieldOfView, normal, tiempoApuntar * Time.deltaTime);
        }
        */

    }


    void HabilitarArma()
    {

        puedeDisparar = true;

    }
    void RevisarDisparo()
    {

        if (!puedeDisparar) return;
        if (tiempoNoDisparo) return;
        if (recargando) return;
        if (balasEnCartucho > 0)
        {
            Disparar();
        }
        else
        {
            SinBalas();
        }

    }

    void Disparar()
    {

        audioSource.PlayOneShot(sonDisparo);
        tiempoNoDisparo = true;
        //   Instantiate(particula, posicion.position, Quaternion.identity);
        ReproducirAnimacionDisparo();
        balasEnCartucho--;
        StartCoroutine(ReiniciarTiempoNoDisparo());
        DisparoDirecto();

    }


    public void crearEfectoDaño(Vector3 pos, Quaternion rot)
    {
        GameObject efectoDaño = Instantiate(efectoDañoPrefab, pos, rot);
        Destroy(efectoDaño, 1f);
    }


    void DisparoDirecto()
    {
        RaycastHit hit;
        // if(Physics.Raycast(puntoDeDisparo.position, puntoDeDisparo.forward, out hit))
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit))
        {
            if (hit.transform.CompareTag("Enemigo"))
            {
                Vida vida = hit.transform.GetComponent<Vida>();
                if (vida == null)
                {
                    throw new System.Exception("No se encontro el componente Vida del Enemigo");
                }
                else
                {
                    vida.RecibirDaño(daño);
                    crearEfectoDaño(hit.point, hit.transform.rotation);
                }
            }
            if (hit.transform.CompareTag("verde"))
            {
                Vida vida = hit.transform.GetComponent<Vida>();
                if (vida == null)
                {
                    throw new System.Exception("No se encontro el componente Vida del Enemigo");
                }
                else
                {
                    vida.RecibirDaño(daño2);
                    crearEfectoDaño(hit.point, hit.transform.rotation);
                }
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //   Gizmos.DrawRay(puntoDeDisparo.position, puntoDeDisparo.forward*100);
        Gizmos.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * 100);
    }

    public virtual void ReproducirAnimacionDisparo()
    {
        if (gameObject.name == "Police9mm")
        {
            if (balasEnCartucho > 1)
            {
                animator.CrossFadeInFixedTime("Fire", 0.1f);
            }
            else
            {
                animator.CrossFadeInFixedTime("FireLast", 0.1f);
            }
        }
        else
        {
            //  animator.CrossFadeInFixedTime("Fire", 0.1f);
        }
    }

    void SinBalas()
    {
        audioSource.PlayOneShot(sonSinBalas);
        tiempoNoDisparo = true;
        StartCoroutine(ReiniciarTiempoNoDisparo());
    }
    IEnumerator ReiniciarTiempoNoDisparo()
    {
        yield return new WaitForSeconds(ritmoDeDisparo);
        tiempoNoDisparo = false;
    }
    void RevisarRecargar()
    {
        if (balasRestantes > 0 && balasEnCartucho < tamañoDelCartucho)
        {
            Recargar();
        }
    }
    void Recargar()
    {
        if (recargando) return;
        recargando = true;
        animator.CrossFadeInFixedTime("Reload", 0.1f);

    }
    void RecargarMuniciones()
    {
        int balasParaRecargar = tamañoDelCartucho - balasEnCartucho;
        int restarBalas = (balasRestantes >= balasParaRecargar) ? balasParaRecargar : balasRestantes; //manera de escribir if, si condicion >= balasrecargar, sino, balas restantes

        balasRestantes -= restarBalas;
        balasEnCartucho += balasParaRecargar;

        recargando = false;


    }
    public void Desenfundar()  //evento de una animacion
    {
        //audioSource.PlayOneShot(sonDesenfundar);
    }
    public void CartuchoEntra()
    {
        audioSource.PlayOneShot(sonCartuchoEntra);
        RecargarMuniciones();

    }
    public void CartuchoSale()
    {
        audioSource.PlayOneShot(sonCartuchoSale);


    }
    public void Vacio()
    {
        //audioSource.PlayOneShot(sonVacio);
        Invoke("ReiniciarRecargar", 0.1f);
    }

    void ReiniciarRecargar()
    {
        recargando = false;
    }


    public void OnTriggerEnter(Collider other)
    {


        if (other.gameObject.name == "heroTree")
        {
            balasRestantes = balasRestantes + 100;
        }

    }
}