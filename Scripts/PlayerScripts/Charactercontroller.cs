using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Charactercontroller : MonoBehaviour
{
        enum STATES
        {
            IDLE, WALK, RUN, JUMP, COLLECT, SHOOT, BACK
        }
        //Contador personaje 
        public float Contador=0f;
        //Variables de barra de vida
        public int Hp;
        public int Hpmax = 100;
        public Slider barravida;
        STATES currentState;
        Animator anim;
        //Variables movimiento
        float horizontalMove;
        float verticalMove;
        private Vector3 playerInput;
        public CharacterController player;
        public float playerSpeed;
        public float gravity;
        public float fallVelocity;
        public float jumpForce;
        public float runForce;
        //Varaibles movimiento relativo a camara
        public Camera mainCamera;
        private Vector3 camForward;
        private Vector3 camRight;
        private Vector3 movePlayer;
        //Variables deslizamiento en pendientes
        public bool isOnSlope = false;
        private Vector3 hitNormal;
        public float slideVelocity;
        public float slopeForceDown;
    //Puntaje
    public float contadorObjetos = 10f;
    public Text contPuntage;
    

    // Cargamos el componente CharacterController en la variable player al iniciar el script


    void Start()
    {
            player = GetComponent<CharacterController>();
            currentState = STATES.IDLE;
            anim = GetComponent<Animator>();
            Hp =Hpmax;
       
    }
    // Bucle de juego que se ejecuta en cada frame
    void Update()
        {
        
            contPuntage.text = "Puntaje "+ contadorObjetos.ToString();
            // barra de vida 
            barravida.value = Hp; 
            if (Hp > Hpmax)
            {
                Hp = Hpmax;
            }
            else if (Hp < 0)
            {
                Hp = 0;
            }
        checkConditions();
            move();
            //Guardamos el valor de entrada horizontal y vertical para el movimiento
            horizontalMove = Input.GetAxis("Horizontal");
            verticalMove = Input.GetAxis("Vertical");
            playerInput = new Vector3(horizontalMove, 0, verticalMove); //los almacenamos en un Vector3
            playerInput = Vector3.ClampMagnitude(playerInput, 1); //Y limitamos su magnitud a 1 para evitar aceleraciones en movimientos diagonales
            CamDirection(); //Llamamos a la funcion CamDirection()
            movePlayer = playerInput.x * camRight + playerInput.z * camForward;  //Almacenamos en movePlayer el vector de movimiento corregido con respecto a la posicion de la camara.
            movePlayer = movePlayer * playerSpeed;  //Y lo multiplicamos por la velocidad del jugador "playerSpeed"
            player.transform.LookAt(player.transform.position + movePlayer); //Hacemos que nuestro personaje mire siempre en la direccion en la que nos estamos moviendo.
            SetGravity(); //Llamamos a la funcion SetGravity() para aplicar la gravedad
            PlayerSkills(); //Llamamos a la funcion PlayerSkills() para invocar las habilidades de nuestro personaje

            player.Move(movePlayer * Time.deltaTime); //Y por ultimo trasladamos los datos de movimiento a nuestro jugador * Time.deltaTime 
                                                      //De este modo mantenemos unos FPS estables independientemente de la potencia del equipo.
                                                      //Debug.Log("Tocando el suelo: " + player.isGrounded); //Descomenta esta linea si quieres monitorizar si estas tocando el suelo en la consola de depuracion
        }
        //Funcion para determinar la direccion a la que mira la camara. 
        public void CamDirection()
        {
            //Guardamos los vectores correspondientes a la posicion/rotacion de la carama tanto hacia delante como hacia la derecha.
            camForward = mainCamera.transform.forward;
            camRight = mainCamera.transform.right;
            //Asignamos los valores de "y" a 0 para no crear conflictos con otras operaciones de movimiento.
            camForward.y = 0;
            camRight.y = 0;
            //Y normalizamos sus valores.
            camForward = camForward.normalized;
            camRight = camRight.normalized;
        }
        //Funcion para las habilidades de nuestro jugador.
        public void PlayerSkills()
        {
        isOnSlope = Vector3.Angle(Vector3.up, hitNormal) <= player.slopeLimit;  
        //Si estamos tocanto el suelo y pulsamos el boton "Jump"
        if (player.isGrounded && Input.GetKey(KeyCode.Space))
        {
                fallVelocity = jumpForce; //La velocidad de caida pasa a ser igual a la velocidad de salto
                movePlayer.y = fallVelocity; //Y pasamos el valor a movePlayer.y
                //anim.SetTrigger("jump 0");
           /* if (isOnSlope) //Si isOnSlope es VERDADERO
            {
                //movemos a nuestro jugador en los ejes "x" y "z" mas o menos deprisa en proporcion al angulo de la pendiente.
                movePlayer.x += ((1f + hitNormal.y) * hitNormal.x) * jumpForce;
                movePlayer.z += ((1f + hitNormal.y) * hitNormal.z) * jumpForce;
                //y aplicamos una fuerza extra hacia abajo para evitar saltos al caer por la pendiente.
                movePlayer.y += jumpForce;
            }*/
        }
            //Run
        if (Input.GetKey(KeyCode.LeftShift))
            {
            //Almacenamos en movePlayer el vector de movimiento corregido con respecto a la posicion de la camara.
            movePlayer = playerInput.x * camRight + playerInput.z * camForward;
            movePlayer = movePlayer * runForce;
            movePlayer.y = fallVelocity;
        }
        }
        //Funcion para la gravedad.
        public void SetGravity()
        {
            //Si estamos tocando el suelo
            if (player.isGrounded)
            {
                //La velocidad de caida es igual a la gravedad en valor negativo * Time.deltaTime.
                fallVelocity = -gravity * Time.deltaTime;
                movePlayer.y = fallVelocity;
            }
            else //Si no...
            {
                //aceleramos la caida cada frame restando el valor de la gravedad * Time.deltaTime.
                fallVelocity -= gravity * Time.deltaTime;
                movePlayer.y = fallVelocity;
            }
           // SlideDown(); //Llamamos a la funcion SlideDown() para comprobar si estamos en una pendiente
        }
        //Esta funcion detecta si estamos en una pendiente muy pronunciada y nos desliza hacia abajo.
        public void SlideDown()
        {
            //si el angulo de la pendiente en la que nos encontramos es mayor o igual al asignado en player.slopeLimit, isOnSlope es VERDADERO
            isOnSlope = Vector3.Angle(Vector3.up, hitNormal) >= player.slopeLimit;
            if (isOnSlope) //Si isOnSlope es VERDADERO
            {
                //movemos a nuestro jugador en los ejes "x" y "z" mas o menos deprisa en proporcion al angulo de la pendiente.
                movePlayer.x += ((1f - hitNormal.y) * hitNormal.x) * slideVelocity;
                movePlayer.z += ((1f - hitNormal.y) * hitNormal.z) * slideVelocity;
                //y aplicamos una fuerza extra hacia abajo para evitar saltos al caer por la pendiente.
                movePlayer.y += slopeForceDown;
            }
        }
        //Esta funcion detecta cuando colisinamos con otro objeto mientras nos movemos
        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            //Almacenamos la normal del plano contra el que hemos chocado en hitNormal.
            hitNormal = hit.normal;
        }
    void checkConditions()
    {

        if (Input.GetKey(KeyCode.W))
        {
            currentState = STATES.WALK;
            if (Input.GetKey(KeyCode.LeftShift))
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


        }
       else if (Input.GetKey(KeyCode.Space))
        {
            currentState = STATES.JUMP;
        }
        else if (Input.GetKey(KeyCode.E))
        {
            currentState = STATES.COLLECT;
        }
        else if (Input.GetKey(KeyCode.Mouse0))
        {
            currentState = STATES.SHOOT;

        }
        else
        {
            currentState = STATES.IDLE;
        }
    }
  /*void verificarcontador()
    {
        if (contadorObjetos == 100)
        {
            //aqui pone la animacion de la puerta para que se abra

            PruebaElementos.SetActive(false);

        }
    }*/
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
        if(Input.GetKey(KeyCode.A))
        {
            anim.SetBool("idle", false);
            anim.SetBool("walk", true);
            anim.SetBool("run", false);
            anim.SetBool("jump", false);
            anim.SetBool("collect", false);
            anim.SetBool("shoot", false);
            anim.SetBool("back", false);
            if (Input.GetKey(KeyCode.LeftShift))
            {
                anim.SetBool("idle", false);
                anim.SetBool("walk", false);
                anim.SetBool("run", true);
                anim.SetBool("jump", false);
                anim.SetBool("collect", false);
                anim.SetBool("shoot", false);
                anim.SetBool("back", false);
            }
        }
        if(Input.GetKey(KeyCode.D))
        {
            anim.SetBool("idle", false);
            anim.SetBool("walk", true);
            anim.SetBool("run", false);
            anim.SetBool("jump", false);
            anim.SetBool("collect", false);
            anim.SetBool("shoot", false);
            anim.SetBool("back", false);
            if (Input.GetKey(KeyCode.LeftShift))
            {
                anim.SetBool("idle", false);
                anim.SetBool("walk", false);
                anim.SetBool("run", true);
                anim.SetBool("jump", false);
                anim.SetBool("collect", false);
                anim.SetBool("shoot", false);
                anim.SetBool("back", false);
            }
        }

        if (Input.GetKey(KeyCode.S))
        {
            anim.SetBool("idle", false);
            anim.SetBool("walk", true);
            anim.SetBool("run", false);
            anim.SetBool("jump", false);
            anim.SetBool("collect", false);
            anim.SetBool("shoot", false);
            anim.SetBool("back", false);
            if (Input.GetKey(KeyCode.LeftShift))
            {
                anim.SetBool("idle", false);
                anim.SetBool("walk", false);
                anim.SetBool("run", true);
                anim.SetBool("jump", false);
                anim.SetBool("collect", false);
                anim.SetBool("shoot", false);
                anim.SetBool("back", false);
            }
        }
        if(Input.GetKey(KeyCode.W)&& Input.GetKey(KeyCode.A)&&Input.GetKey(KeyCode.Space))
        {
            anim.SetBool("idle", false);
            anim.SetBool("walk", false);
            anim.SetBool("run", false);
            anim.SetBool("jump", true);
            anim.SetBool("collect", false);
            anim.SetBool("shoot", false);
            anim.SetBool("back", false);
        }
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.Space))
        {
            anim.SetBool("idle", false);
            anim.SetBool("walk", false);
            anim.SetBool("run", false);
            anim.SetBool("jump", true);
            anim.SetBool("collect", false);
            anim.SetBool("shoot", false);
            anim.SetBool("back", false);
        }
        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.Space))
        {
            anim.SetBool("idle", false);
            anim.SetBool("walk", false);
            anim.SetBool("run", false);
            anim.SetBool("jump", true);
            anim.SetBool("collect", false);
            anim.SetBool("shoot", false);
            anim.SetBool("back", false);
        }
        if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.Space))
        {
            anim.SetBool("idle", false);
            anim.SetBool("walk", false);
            anim.SetBool("run", false);
            anim.SetBool("jump", true);
            anim.SetBool("collect", false);
            anim.SetBool("shoot", false);
            anim.SetBool("back", false);
        }
        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.Space))
        {
            anim.SetBool("idle", false);
            anim.SetBool("walk", false);
            anim.SetBool("run", false);
            anim.SetBool("jump", true);
            anim.SetBool("collect", false);
            anim.SetBool("shoot", false);
            anim.SetBool("back", false);
        }
            
    }
    public void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.name == "ObjetoMalo")
        {
            Hp = Hp - 20;
            Destroy(other.gameObject);
        }
        if (other.gameObject.name == "bomba")
        {
            Hp = Hp - 100;
            Destroy(other.gameObject);
        }

        if (other.gameObject.name == "VidaMax")
        {
            Hp = Hp + 100;
            Destroy(other.gameObject);
        }
       
    }
}
