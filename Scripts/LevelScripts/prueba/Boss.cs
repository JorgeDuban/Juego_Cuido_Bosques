using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss : MonoBehaviour
{
    // Start is called before the first frame update

    enum States
    {
        IDLE, FOLLOW, ATTACK

    }
    public GameObject particula;
    public Transform posicion;
    NavMeshAgent agente;
    public GameObject objetivo;
    public float distanciaactual;
    public float distanciareferencia;
    private States currentstate;
    Animator anim;
    public bool mirando;


    /////////////////

    public bool Vida0 = false;
    private Vida vida;
    public bool sumarPuntos = false;
    public GameObject puntajePantalla;
    private Collider collider;
    private Animator animator;
    private LogicaJugador logicaJugador;
    private GameObject target;
    private Vida vidaJugador;
    public float daño = 25;



    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("personaje");
        vidaJugador = target.GetComponent<Vida>();
        if (vidaJugador == null)
        {
            throw new System.Exception("El objeto Jugador no tiene componente Vida");
        }

        logicaJugador = target.GetComponent<LogicaJugador>();

        if (logicaJugador == null)
        {
            throw new System.Exception("El objeto Jugador no tiene componente LogicaJugador");
        }

        agente = GetComponent<NavMeshAgent>();
        currentstate = States.IDLE;
        anim = GetComponent<Animator>();
        vida = GetComponent<Vida>();
        animator = GetComponent<Animator>();
        collider = GetComponent<Collider>();

    }

    // Update is called once per frame
    void Update()
    {
        behaviour();
        checkconditions();
        RevisarVida();



    }

    void checkconditions()
    {
        distanciaactual = Vector3.Distance(transform.position, objetivo.transform.position);

        if (distanciaactual < distanciareferencia && distanciaactual > 4)
        {
            //agente.SetDestination(objetivo.transform.position);
            currentstate = States.FOLLOW;
        }
        else if (distanciaactual <= 4)
        {
            currentstate = States.ATTACK;

        }

        else
        {
            //agente.SetDestination(transform.position);
            currentstate = States.IDLE;

        }
    }


    void behaviour()
    {
        switch (currentstate)
        {
            case States.IDLE:
                idleState();
                break;
            case States.FOLLOW:
                followState();
                break;
            case States.ATTACK:
                attackState();
                break;
            default:
                break;
        }
    }
    void idleState()
    {
        agente.SetDestination(transform.position);

        anim.SetBool("idle", true);
        anim.SetBool("correr", false);
        anim.SetBool("atacar", false);
    }
    void followState()
    {
        agente.SetDestination(objetivo.transform.position);

        anim.SetBool("idle", false);
        anim.SetBool("correr", true);
        anim.SetBool("atacar", false);
    }
    void attackState()
    {
        agente.SetDestination(objetivo.transform.position);

        anim.SetBool("idle", false);
        anim.SetBool("correr", false);
        anim.SetBool("atacar", true);
        // vidaJugador.RecibirDaño(daño);




    }

    public void particulas()
    {
        Instantiate(particula, posicion.position, Quaternion.identity);
    }


    void RevisarVida()
    {
        if (Vida0) return;
        if (vida.valor <= 0)
        {
            sumarPuntos = true;
            if (sumarPuntos)
            {
                puntajePantalla.GetComponent<puntajenuevo>().valor += 50;
                sumarPuntos = false;
            }
            Vida0 = true;
            agente.isStopped = true;
            collider.enabled = false;
            animator.CrossFadeInFixedTime("morir", 0.1f);
            Destroy(gameObject, 3f);
        }
    }




}
