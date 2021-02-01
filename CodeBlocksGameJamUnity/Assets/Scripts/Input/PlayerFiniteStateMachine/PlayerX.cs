using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerX : MonoBehaviour
{
    public PlayerStateMachineX StateMachine { get; private set; }

    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }

    public PlayerInputHandler InputHandler { get; private set; }
    public Animator Anim { get; private set; }

    public Vector2 CurrentVelocity { get; private set; }
    public bool facingRight { get; private set; }
    public Rigidbody2D RB { get; private set; }

    [SerializeField] private PlayerData playerData;

    private void Awake()
    {
        StateMachine = new PlayerStateMachineX();

        IdleState = new PlayerIdleState(this, StateMachine, playerData, "idle");
        MoveState =  new PlayerMoveState(this, StateMachine, playerData, "move");
    }
    private void Start()
    {
        Anim = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();
        RB = GetComponent<Rigidbody2D>();

        facingRight = false;
        
        StateMachine.Initialize(IdleState);
    }
    private void Update()
    {
        CurrentVelocity = RB.velocity;
        StateMachine.CurrentState.LogicUpdate();
    }
    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }
    public void SetVelocity(Vector2 velocity)
    {
        RB.velocity = velocity;
        CurrentVelocity = velocity;
    }
    public void CheckIfFlip()
    {
        if(RB.velocity.x >0 && !facingRight || RB.velocity.x < 0 && facingRight)
        {
            Flip();
        }
    }
    private void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }
}
