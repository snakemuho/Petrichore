using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerStateMachine StateMachine { get; private set; }
    public PlayerIdleState IdleState { get; private set; }
    public PlayerWalkingState WalkingState { get; private set; }
    //public PlayerJumpState JumpState { get; private set; }
    //public PlayerDoubleJumpState DoubleJumpState { get; private set; }
    public PlayerAttackState AttackState { get; private set; }
    public PlayerAirState AirState { get; private set; }

    Vector3 desiredVelocity;
    [SerializeField] PlayerData playerData;
    public Rigidbody MyRB;
    public Animator Anim, AnimUmbrella;
    public PlayerInputHandler InputHandler;
    Transform cam;
    float horizontalInput, verticalInput;
    public CapsuleCollider myCollider;

    public AudioSource myAudioSource { get; private set; }
    [SerializeField] AudioClip[] footSound, jumpSound, doubleJumpSound, dashSound, attackSound;

    public int numberOfJumps = 1;
    public int numberOfDashes = 1;

    public float jumpCD = 0.2f;

    float rayGroundDistance = .2f;
    RaycastHit hitBL, hitFL, hitBR, hitFR;
    bool hitDis, hitFdis, hitBdis, hitRdis, hitLdis;
    float distanceToGround;
    private void Awake()
    {
        myAudioSource = GetComponent<AudioSource>();
        StateMachine = new PlayerStateMachine();
        IdleState = new PlayerIdleState(this, StateMachine, playerData, "idle");
        WalkingState = new PlayerWalkingState(this, StateMachine, playerData, "walk");
        AttackState = new PlayerAttackState(this, StateMachine, playerData, "attack");
        AirState = new PlayerAirState(this, StateMachine, playerData, "air");
        //JumpState = new PlayerJumpState(this, StateMachine, playerData, "jump");
        //DoubleJumpState = new PlayerDoubleJumpState(this, StateMachine, playerData, "doublejump");
        MyRB = GetComponent<Rigidbody>();

    }
    void Start()
    {
        StateMachine.Initialize(IdleState);
        distanceToGround = myCollider.bounds.extents.y - myCollider.center.y + rayGroundDistance;
        cam = Camera.main.transform;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    private void Update()
    {
        StateMachine.CurrentState.LogicUpdate();
        if (jumpCD < 0.2f)
            jumpCD -= Time.deltaTime;
        if (jumpCD <= 0)
            jumpCD = 0.2f;
    }

    void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
        horizontalInput = InputHandler.MovementInput.x;
        verticalInput = InputHandler.MovementInput.y;
        //TurnWithCam();
        //Walk();
    }
    public bool GroundCheck()
    {
        Ray rayFR = new Ray(transform.position + transform.forward * .5f + transform.right * .5f + transform.up * .1f, -transform.up * rayGroundDistance);
        Ray rayFL = new Ray(transform.position + transform.forward * .5f - transform.right * .5f + transform.up * .1f, -transform.up * rayGroundDistance);
        Ray rayBL = new Ray(transform.position - transform.forward * .5f - transform.right * .5f + transform.up * .1f, -transform.up * rayGroundDistance);
        Ray rayBR = new Ray(transform.position - transform.forward * .5f + transform.right * .5f + transform.up * .1f, -transform.up * rayGroundDistance);

        Debug.DrawRay(transform.position + transform.forward * .5f + transform.right * .5f + transform.up * .1f, -transform.up * rayGroundDistance);
        Debug.DrawRay(transform.position + transform.forward * .5f - transform.right * .5f + transform.up * .1f, -transform.up * rayGroundDistance);
        Debug.DrawRay(transform.position - transform.forward * .5f - transform.right * .5f + transform.up * .1f, -transform.up * rayGroundDistance);
        Debug.DrawRay(transform.position - transform.forward * .5f + transform.right * .5f + transform.up * .1f, -transform.up * rayGroundDistance);

        Physics.Raycast(rayFR, out hitFR, 1f, ~3);
        Physics.Raycast(rayBL, out hitBL, 1f, ~3);
        Physics.Raycast(rayFL, out hitFL, 1f, ~3);
        Physics.Raycast(rayBR, out hitBR, 1f, ~3);

        hitFdis = hitFR.distance != 0 && hitFR.distance <= distanceToGround;
        hitBdis = hitBL.distance != 0 && hitBL.distance <= distanceToGround;
        hitLdis = hitFL.distance != 0 && hitFL.distance <= distanceToGround;
        hitRdis = hitBR.distance != 0 && hitBR.distance <= distanceToGround;

        return hitFdis || hitBdis || hitLdis || hitRdis;
    }
    public void Walk()
    {
        Vector3 camForward = cam.forward;
        Vector3 camRight = cam.right;
        camForward.y = 0;
        camRight.y = 0;

        desiredVelocity = (camForward.normalized * verticalInput + camRight.normalized * horizontalInput) * playerData.maxWalkSpeed;
        desiredVelocity.y = MyRB.velocity.y;
        float _maxAccel = playerData.walkAcceleration * Time.deltaTime;
        MyRB.velocity = GroundCheck() 
            ? Vector3.MoveTowards(MyRB.velocity, desiredVelocity, _maxAccel)
            : Vector3.MoveTowards(MyRB.velocity, desiredVelocity, _maxAccel / 2);

        if ((horizontalInput != 0 || verticalInput != 0) && !InputHandler.AimInput)
            transform.rotation = Quaternion.Slerp(transform.rotation, 
            Quaternion.LookRotation(new Vector3(desiredVelocity.x, 0, desiredVelocity.z)), playerData.turnSpeed);


    }

    public void Jump()
    {
        print("cd: " + jumpCD);
        print("dashes: " + numberOfDashes);
        Anim.SetBool("jump", true);
        jumpCD -= Time.deltaTime;
        //InputHandler.UseJumpInput();

        if (!InputHandler.AimInput)
        {
            if (numberOfJumps > 0)
            {
                //if (MyRB.velocity.y < 0)
                    MyRB.velocity = new Vector3(MyRB.velocity.x, 0, MyRB.velocity.z);
                MyRB.velocity += Vector3.up * playerData.jumpSpeed;
                numberOfJumps--;
            }
            else
            {
                MyRB.velocity = new Vector3(MyRB.velocity.x, MyRB.velocity.y / 4, MyRB.velocity.z);
                MyRB.velocity += Vector3.up * playerData.jumpSpeed * 0.05f;
            }
        }
        else
        {
            if (numberOfDashes > 0)
            {
                myAudioSource.PlayOneShot(dashSound[Random.Range(0, dashSound.Length - 1)], 0.4f);
                MyRB.velocity = new Vector3(MyRB.velocity.x, 0, MyRB.velocity.z);
                if (Mathf.Abs(MyRB.velocity.x) > 15)
                    MyRB.velocity = new Vector3(0, MyRB.velocity.y, MyRB.velocity.z);
                if (Mathf.Abs(MyRB.velocity.z) > 15)
                    MyRB.velocity = new Vector3(MyRB.velocity.x, MyRB.velocity.y, 0);

                if (!GroundCheck())
                    MyRB.velocity += transform.forward * playerData.jumpSpeed * 2f + Vector3.up * playerData.jumpSpeed;
                else MyRB.velocity += transform.forward * playerData.jumpSpeed * 2f + Vector3.up * playerData.jumpSpeed / 2;
                numberOfDashes--;
            }
            else myAudioSource.PlayOneShot(jumpSound[Random.Range(0, jumpSound.Length - 1)], 0.6f);
        }
        print("did jump");
        //MyRB.AddForce(Vector3.up * playerData.jumpSpeed, ForceMode.Impulse);
    }
    public void ResetVelocity()
    {
        MyRB.AddForce(-MyRB.velocity * 0.6f, ForceMode.Impulse);
    }
    public void UmbrellaAttack()
    {
        AnimUmbrella.SetBool("attack", true);
        AnimUmbrella.SetBool("air", GroundCheck());

    }
    void TurnWithCam()
    {
        //transform.eulerAngles = new Vector3(0, cam.rotation.eulerAngles.y, 0);
    }

    public void PlayFootstepSound()
    {
        myAudioSource.PlayOneShot(footSound[Random.Range(0, footSound.Length - 1)], 0.2f);
    }
    public void PlayAttackSound()
    {
        myAudioSource.PlayOneShot(attackSound[Random.Range(0, attackSound.Length - 1)], 0.6f);
    }
    public void PlayJumpSound()
    {
        if (!InputHandler.AimInput)
            myAudioSource.PlayOneShot(jumpSound[Random.Range(0, jumpSound.Length - 1)], 0.6f);
    }
    public void PlayDoubleJumpSound()
    {
        if (!InputHandler.AimInput)
            myAudioSource.PlayOneShot(doubleJumpSound[Random.Range(0, doubleJumpSound.Length - 1)], 0.4f);
    }
}
