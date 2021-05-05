using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class enemybueno : MonoBehaviour
{
    private GameObject target;
    private NavMeshAgent agente;
    private Vida vida;
    private Animator animator;
    private Collider collider;
    private Vida vidaJugador;
    private LogicaJugador logicaJugador;
    public bool Vida0 = false;
    public bool estaAtacando = false;
    public float speed = 1.0f;
    public float angularSpeed = 120;
    public float daño = 25;
    public bool mirando;

    public bool sumarPuntos = false;
   

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
        vida = GetComponent<Vida>();
        animator = GetComponent<Animator>();
        collider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
   
        RevisarAtaque();
        EstaDeFrenteAlJugador();
    }

    void EstaDeFrenteAlJugador()
    {
        Vector3 adelante = transform.forward;
        Vector3 targetJugador = (GameObject.Find("personaje").transform.position - transform.position).normalized;

        if (Vector3.Dot(adelante, targetJugador) < 0.6f)
        {
            mirando = false;

        }
        else
        {
            mirando = true;
        }

    }
  

    void RevisarAtaque()
    {
        if (Vida0) return;
        if (estaAtacando) return;
        if (logicaJugador.Vida0) return;
        float distanciaDelBlanco = Vector3.Distance(target.transform.position, transform.position);

        if (distanciaDelBlanco <= 3.0 && mirando)
        {
            esperar();
            Atacar();
        }

    }
    IEnumerator esperar()
    {
        yield return new WaitForSeconds(300);
    }

    void Atacar()
    {
       
        vidaJugador.RecibirDaño(daño);
        agente.speed = 3.5f;
        agente.angularSpeed = 120f;
        estaAtacando = true;
        animator.SetTrigger("atacar");
        Invoke("ReiniciarAtaque", 3f);
    }
    void ReiniciarAtaque()
    {
        estaAtacando = false;
        agente.speed = speed;
        agente.angularSpeed = angularSpeed;
    }


}
