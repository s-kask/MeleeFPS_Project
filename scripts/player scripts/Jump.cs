using Godot;
using System;

public partial class Jump : State
{
    
    [Export] public float JumpVelocity = 15.0f;
    [Export] private float _floorCheckDelay = 0.2f;
    
    private Moving _moveState;

    private float _floorCheckTimer = 0.0f;
    private bool _canCheckFloor = false;

    public override void _Ready()
    {
        _moveState = GetNode<Moving>("../MOVING");
    }

    public override void Enter()
    {
        var velocity = PlayerRef.Velocity;
        velocity.Y = JumpVelocity;
        PlayerRef.Velocity = velocity;

        _floorCheckTimer = _floorCheckDelay;
        _canCheckFloor = false;
        
        GD.Print("Jump State");
    }

    public override void Update(float delta)
    {
        // Handle horizontal movement during jump
        Vector2 inputDir = Input.GetVector("move_left", "move_right", "move_forward", "move_back");
        
        if (inputDir != Vector2.Zero)
        {
            Vector3 velocity = PlayerRef.Velocity;
            Vector3 direction = (PlayerRef.Transform.Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();

            velocity.X = direction.X * _moveState.Speed;
            velocity.Z = direction.Z * _moveState.Speed;
            PlayerRef.Velocity = velocity;
        }
    }

    public override void PhysicsUpdate(float delta)
    {
        base.PhysicsUpdate(delta);

        if (!_canCheckFloor)
        {
            _floorCheckTimer -= delta;
            if (_floorCheckTimer <= 0)
            {
                _canCheckFloor = true;
            }
        }

        if (_canCheckFloor && PlayerRef.IsOnFloor())
        {
            msm.TransitionTo("IDLE");
        }
        
        if (_canCheckFloor && PlayerRef.Velocity.Y < 0 && !PlayerRef.IsOnFloor())
        {
            msm.TransitionTo("FALLING");
        }
    }
}
