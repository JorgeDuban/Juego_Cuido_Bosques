using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personaje : MonoBehaviour
{
    // Start is called before the first frame update
    enum STATES
    {
        IDLE, WALK, RUN, JUMP, COLLECT, SHOOT, BACK
    }
    STATES currentState;
    Animator anim;
    private Rigidbody rb;
  

    // public float numeroObjetos = 0f;
    void Start()
    {
        currentState = STATES.IDLE;
        anim = GetComponent<Animator>();
        //rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {

        checkConditions();
        move();
    }
    
    void checkConditions()
    {

        if (Input.GetKey(KeyCode.W))
        {
            currentState = STATES.WALK;
            if (Input.GetKey(KeyCode.LeftControl))
            {
            
            currentState = STATES.RUN;
            }
            if (Input.GetKey(KeyCode.Space))
            {
                 currentState = STATES.JUMP;
            }

            if (Input.GetKey(KeyCode.E))
            {
                currentState = STATES.COLLECT;
            }
            if (Input.GetKey(KeyCode.Mouse0))
            {
             currentState = STATES.SHOOT;
            }
                    
           
        }else if (Input.GetKey(KeyCode.Space))
        {              
                currentState = STATES.JUMP;
        }else if (Input.GetKey(KeyCode.E))
        {
            currentState = STATES.COLLECT;
        }else if (Input.GetKey(KeyCode.Mouse0))
        {
            currentState = STATES.SHOOT;

        }
        else
        {
            currentState = STATES.IDLE;
        }
    }

    void move()
    {

        switch (currentState)
        {
            case STATES.IDLE:
                Idle();
                Girar();
                break;
            case STATES.WALK:
                Walk();
                Girar();
                break;
            case STATES.RUN:
                Run();
                Girar();
                break;
            case STATES.JUMP:
                Jump();
                Girar();
                break;
            case STATES.COLLECT:
                Collect();
                Girar();
                break;
            case STATES.SHOOT:
                Shoot();
                Girar();
                break;
            case STATES.BACK:
                
                Girar();
                break;


        }
    }
    void Idle()
    {
        anim.SetBool("idle", true);
        anim.SetBool("walk", false);
        anim.SetBool("run", false);
        anim.SetBool("jump", false);
        anim.SetBool("collect", false);
        anim.SetBool("shoot", false);
        anim.SetBool("back", false);
    }
    void Walk()
    {
        anim.SetBool("idle", false);
        anim.SetBool("walk", true);
        anim.SetBool("run", false);
        anim.SetBool("jump", false);
        anim.SetBool("collect", false);
        anim.SetBool("shoot", false);
        anim.SetBool("back", false);
        transform.Translate(0, 0, 3 * Time.deltaTime);
    }
    void Run()
    {
        anim.SetBool("idle", false);
        anim.SetBool("walk", false);
        anim.SetBool("run", true);
        anim.SetBool("jump", false);
        anim.SetBool("collect", false);
        anim.SetBool("shoot", false);
        anim.SetBool("back", false);
        transform.Translate(0, 0, 4 * Time.deltaTime);
    }
    void Jump()
    {
        anim.SetBool("idle", false);
        anim.SetBool("walk", false);
        anim.SetBool("run", false);
        anim.SetBool("jump", true);
        anim.SetBool("collect", false);
        anim.SetBool("shoot", false);
        anim.SetBool("back", false);
        transform.Translate(0, 5 * Time.deltaTime, 3 * Time.deltaTime);
    }
    void Collect()
    {
        anim.SetBool("idle", false);
        anim.SetBool("walk", false);
        anim.SetBool("run", false);
        anim.SetBool("jump", false);
        anim.SetBool("collect", true);
        anim.SetBool("shoot", false);
        anim.SetBool("back", false);
    }
    void Shoot()
    {

        anim.SetBool("idle", false);
        anim.SetBool("walk", false);
        anim.SetBool("run", false);
        anim.SetBool("jump", false);
        anim.SetBool("collect", false);
        anim.SetBool("shoot", true);
        anim.SetBool("back", false);

    }

    void Girar()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(0, -2, 0 * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0, 2, 0 * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            anim.SetBool("idle", false);
            anim.SetBool("walk", false);
            anim.SetBool("run", false);
            anim.SetBool("jump", false);
            anim.SetBool("collect", false);
            anim.SetBool("shoot", false);
            anim.SetBool("back", true);
            transform.Translate(0, 0, -2 * Time.deltaTime);
        }

    }
}