using Godot;
using System;
using System.Linq;

public partial class Idle : State
{
    private Jump _jumpState;
    private Moving _moveState;
    public override void _Ready()
    {
        _jumpState = GetNode<Jump>("../JUMP");
        _moveState = GetNode<Moving>("../MOVING");
    }

    public override void Enter()
    {
        var velocity = PlayerRef.Velocity;
        velocity.X = Mathf.MoveToward(PlayerRef.Velocity.X, 0, _moveState.Speed);
        velocity.Z = Mathf.MoveToward(PlayerRef.Velocity.Z, 0, _moveState.Speed);
        PlayerRef.Velocity = velocity;
        GD.Print("Idle State");
    }
    
    public override void Exit()
    {
        
    }

    public override void Update(float delta)
    {
        if (PlayerRef.BasicMoveActions.Any(action => Input.IsActionJustPressed(action)))
        {
            msm.TransitionTo("MOVING");
        }
        if (PlayerRef.Velocity.Y < 0 && !PlayerRef.IsOnFloor())
        {
            msm.TransitionTo("FALLING");
        }
    }

    public override void PhysicsUpdate(float delta)
    {
        base.PhysicsUpdate(delta);
        var velocity = PlayerRef.Velocity;
        velocity.X = Mathf.MoveToward(PlayerRef.Velocity.X, 0, _moveState.Speed);
        velocity.Z = Mathf.MoveToward(PlayerRef.Velocity.Z, 0, _moveState.Speed);
        PlayerRef.Velocity = velocity;
    }

    public override void HandleInput(InputEvent @event)
    {
        if (Input.IsActionJustPressed("jump") && PlayerRef.IsOnFloor())
        {
            msm.TransitionTo("JUMP");
        }
        
    }
}
