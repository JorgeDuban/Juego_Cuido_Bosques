using UnityEngine;
using System.Collections.Generic;
using System.Collections;

// MoveBehaviour inherits from GenericBehaviour. This class corresponds to basic walk and run behaviour, it is the default behaviour.
public class MoveBehaviour : GenericBehaviour
{
	public float walkSpeed = 0.15f;                 // Default walk speed.
	public float runSpeed = 1.0f;                   // Default run speed.
	public float sprintSpeed = 2.0f;                // Default sprint speed.
	public float speedDampTime = 0.1f;              // Default damp time to change the animations based on current speed.
	public string jumpButton = "Jump";              // Default jump button.
	public float jumpHeight = 1.5f;                 // Default jump height.
	public float jumpIntertialForce = 10f;          // Default horizontal inertial force when jumping.
	public string recogerButton = "Recollect"; 
	

	private float speed, speedSeeker;               // Moving speed.
	private int jumpBool;                           // Animator variable related to jumping.
	private int groundedBool;                       // Animator variable related to whether or not the player is on ground.
	private bool jump;                              // Boolean to determine whether or not the player started a jump.
	public bool isColliding;                       // Boolean to determine if the player has collided with an obstacle.
	private int collectBool;
	private bool collect;
	public CapsuleCollider capsulapj;
	public float capsuliniheight = 1.8f;
	public float capsulcolheight = 1.28f;

    private Animator animator;
    //public AudioClip Caminar;
    //AudioSource audioSource;
    //public AudioClip sonCaminar;

    // Start is always called after any Awake functions.
    /*private void Awake()
	{
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
	}*/
    void Start()
	{
       

        AudioListener.volume = 1f;
        // Set up the references.
        collectBool = Animator.StringToHash("Collect");
		jumpBool = Animator.StringToHash("Jump");
		groundedBool = Animator.StringToHash("Grounded");
		behaviourManager.GetAnim.SetBool(groundedBool, true);

		// Subscribe and register this behaviour as the default behaviour.
		behaviourManager.SubscribeBehaviour(this);
		behaviourManager.RegisterDefaultBehaviour(this.behaviourCode);
		speedSeeker = runSpeed;
        //capsulapj = GetComponent<CapsuleCollider>();

        //	audioSource = GetComponent<AudioSource>();

        animator = GetComponent<Animator>();
    }

	// Update is used to set features regardless the active behaviour.
	void Update()
	{
		
		// Get jump input.
		if (!jump && Input.GetButtonDown(jumpButton) && behaviourManager.IsCurrentBehaviour(this.behaviourCode) && !behaviourManager.IsOverriding())
		{
			
			jump = true;			
		}
      
		if (!collect && Input.GetButtonDown(recogerButton) && behaviourManager.IsCurrentBehaviour(this.behaviourCode) && !behaviourManager.IsOverriding() )

        {
           
            
           // collect = true;
		}

        
        if (Input.GetMouseButtonDown(2)) //|| Input.GetMouseButtonDown(1))
		{
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}
		if (Input.GetKeyDown("escape"))
		{
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}
        }

	// LocalFixedUpdate overrides the virtual function of the base class.
	public override void LocalFixedUpdate()
	{
		// Call the basic movement manager.
		MovementManagement(behaviourManager.GetH, behaviourManager.GetV);
		// Call the jump manager.
		JumpManagement();
		//call the collect manager.
		CollectManagement();
	}

	// Execute the idle and walk/run jump movements.
	void JumpManagement()
	{
		// Start a new jump.
		if (jump && !behaviourManager.GetAnim.GetBool(jumpBool) && behaviourManager.IsGrounded())
		{
//_____________audioSource.Play();
			// Set jump related parameters.
			behaviourManager.LockTempBehaviour(this.behaviourCode);
			behaviourManager.GetAnim.SetBool(jumpBool, true);
			// Is a locomotion jump?
			if (behaviourManager.GetAnim.GetFloat(speedFloat) > 0.1)
			{
				
				// Temporarily change player friction to pass through obstacles.
				GetComponent<CapsuleCollider>().material.dynamicFriction = 0f;
				GetComponent<CapsuleCollider>().material.staticFriction = 0f;
				// Remove vertical velocity to avoid "super jumps" on slope ends.
				RemoveVerticalVelocity();
				// Set jump vertical impulse velocity.
				float velocity = 2f * Mathf.Abs(Physics.gravity.y) * jumpHeight;
				velocity = Mathf.Sqrt(velocity);
				behaviourManager.GetRigidBody.AddForce(Vector3.up * velocity, ForceMode.VelocityChange);
			}
		}
		// Is already jumping?
		else if (behaviourManager.GetAnim.GetBool(jumpBool))
		{
			// Keep forward movement while in the air.
			if (!behaviourManager.IsGrounded() && !isColliding && behaviourManager.GetTempLockStatus())
			{
				behaviourManager.GetRigidBody.AddForce(transform.forward * jumpIntertialForce * Physics.gravity.magnitude * sprintSpeed, ForceMode.Acceleration);
				
			}
			// Has landed?
			if ((behaviourManager.GetRigidBody.velocity.y < 0) && behaviourManager.IsGrounded())
			{
				behaviourManager.GetAnim.SetBool(groundedBool, true);
				
				// Change back player friction to default.
				GetComponent<CapsuleCollider>().material.dynamicFriction = 0.6f;
				GetComponent<CapsuleCollider>().material.staticFriction = 0.6f;
				// Set jump related parameters.
				jump = false;
				behaviourManager.GetAnim.SetBool(jumpBool, false);
				behaviourManager.UnlockTempBehaviour(this.behaviourCode);
			}
		}
	}
	void CollectManagement()
	{
		/*// Start a new collect.
		if (collect && !behaviourManager.GetAnim.GetBool(collectBool) && behaviourManager.IsGrounded())
		{
			// Set collet related parameters.
			behaviourManager.LockTempBehaviour(this.behaviourCode);
			behaviourManager.GetAnim.SetBool(collectBool, true);
			// Is a locomotion collect?
			behaviourManager.GetRigidBody.useGravity = true;
			capsulapj.height = capsulcolheight;
			new WaitForSeconds(4f);

		}
		// Is already collect?
		else if (behaviourManager.GetAnim.GetBool(collectBool))
		{
			// Keep forward movement while in the air.

			// Has landed?
			
				behaviourManager.GetAnim.SetBool(groundedBool, true);

//_______________ACA AUDIO RECOGERaudioSource.Play();

			// Change back player friction to default.

			// Set jump related parameters.
			capsulapj.height = capsuliniheight;
			collect = false;
			
			behaviourManager.GetAnim.SetBool(collectBool, false);
		    behaviourManager.UnlockTempBehaviour(this.behaviourCode);
		}*/
		if (collect && !behaviourManager.GetAnim.GetBool(collectBool))
		{
			//_____________audioSource.Play();
			// Set jump related parameters.
			behaviourManager.LockTempBehaviour(this.behaviourCode);
			behaviourManager.GetAnim.SetBool(collectBool, true);
		}
		// Is already jumping?
		else if (behaviourManager.GetAnim.GetBool(collectBool))
		{
			
			if ((behaviourManager.GetRigidBody.velocity.y < 0) && behaviourManager.IsGrounded())
			{
				behaviourManager.GetAnim.SetBool(groundedBool, true);

				// Change back player friction to default.
				GetComponent<CapsuleCollider>().material.dynamicFriction = 0.6f;
				GetComponent<CapsuleCollider>().material.staticFriction = 0.6f;
				// Set jump related parameters.
				collect = false;
				behaviourManager.GetAnim.SetBool(collectBool, false);
				behaviourManager.UnlockTempBehaviour(this.behaviourCode);
			}
		}
	}

	// Deal with the basic player movement
	void MovementManagement(float horizontal, float vertical)
	{
		
		// On ground, obey gravity.
		if (behaviourManager.IsGrounded())
			behaviourManager.GetRigidBody.useGravity = true;

		// Avoid takeoff when reached a slope end.
		else if (!behaviourManager.GetAnim.GetBool(jumpBool) && behaviourManager.GetRigidBody.velocity.y > 0)
		{

			RemoveVerticalVelocity();
		}

		// Call function that deals with player orientation.
		Rotating(horizontal, vertical);

		// Set proper speed.
		Vector2 dir = new Vector2(horizontal, vertical);
		speed = Vector2.ClampMagnitude(dir, 1f).magnitude;
		// This is for PC only, gamepads control speed via analog stick.
		speedSeeker += Input.GetAxis("Mouse ScrollWheel");
		speedSeeker = Mathf.Clamp(speedSeeker, walkSpeed, runSpeed);
		speed *= speedSeeker;
		if (behaviourManager.IsSprinting())
		{
			speed = sprintSpeed;
		}

		behaviourManager.GetAnim.SetFloat(speedFloat, speed, speedDampTime, Time.deltaTime);
	}

	// Remove vertical rigidbody velocity.
	private void RemoveVerticalVelocity()
	{
		Vector3 horizontalVelocity = behaviourManager.GetRigidBody.velocity;
		horizontalVelocity.y = 0;
		behaviourManager.GetRigidBody.velocity = horizontalVelocity;
	}

	// Rotate the player to match correct orientation, according to camera and key pressed.
	Vector3 Rotating(float horizontal, float vertical)

	{
		// Get camera forward direction, without vertical component.
		Vector3 forward = behaviourManager.playerCamera.TransformDirection(Vector3.forward);

		// Player is moving on ground, Y component of camera facing is not relevant.
		forward.y = 0.0f;
		forward = forward.normalized;

		// Calculate target direction based on camera forward and direction key.
		Vector3 right = new Vector3(forward.z, 0, -forward.x);
		Vector3 targetDirection;
		targetDirection = forward * vertical + right * horizontal;

		// Lerp current direction to calculated target direction.
		if ((behaviourManager.IsMoving() && targetDirection != Vector3.zero))
		{
			Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

			Quaternion newRotation = Quaternion.Slerp(behaviourManager.GetRigidBody.rotation, targetRotation, behaviourManager.turnSmoothing);
			behaviourManager.GetRigidBody.MoveRotation(newRotation);
			behaviourManager.SetLastDirection(targetDirection);
		}
		// If idle, Ignore current camera facing and consider last moving direction.
		if (!(Mathf.Abs(horizontal) > 0.9 || Mathf.Abs(vertical) > 0.9))
		{
			behaviourManager.Repositioning();
//________________________--ACA SONIDO PARA CAMINAR
	        //audioSource.PlayOneShot(Caminar);
		}

		return targetDirection;
	}

	// Collision detection.
	private void OnCollisionStay(Collision collision)
	{
		isColliding = true;
		// Slide on vertical obstacles
		if (behaviourManager.IsCurrentBehaviour(this.GetBehaviourCode()) && collision.GetContact(0).normal.y <= 0.1f)
		{
			float vel = behaviourManager.GetAnim.velocity.magnitude;
			Vector3 tangentMove = Vector3.ProjectOnPlane(transform.forward, collision.GetContact(0).normal).normalized * vel;
			behaviourManager.GetRigidBody.AddForce(tangentMove, ForceMode.VelocityChange);
		}
		
	}
	/*public void OnCollisionEnter(Collider collision)
	{
		if (collision.gameObject.tag == "basura" && Input.GetKey(KeyCode.E))
		{
			
				Debug.Log("xdxdxdxd");
				animator.CrossFadeInFixedTime("recoger", 0.1f);
		
		}
		
	}*/


	private void OnCollisionExit(Collision collision)
	{
		isColliding = false;
	}
}
