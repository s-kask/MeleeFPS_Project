using Godot;
using System;

public partial class Falling : State
{
    private Moving _moveState;
    
    public override void _Ready()
    {
        _moveState = GetNode<Moving>("../MOVING");
    }
    
    public override void Enter()
    {
        GD.Print("Fall State");
    }

    public override void Update(float delta)
    {
        // Handle horizontal movement during fall
        Vector2 inputDir = Input.GetVector("move_left", "move_right", "move_forward", "move_back");
        
        if (inputDir != Vector2.Zero)
        {
            Vector3 velocity = PlayerRef.Velocity;
            Vector3 direction = (PlayerRef.Transform.Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();

            velocity.X = direction.X * _moveState.Speed;
            velocity.Z = direction.Z * _moveState.Speed;
            PlayerRef.Velocity = velocity;
        }

        if (PlayerRef.IsOnFloor() && PlayerRef.Velocity.Length() != 0)
        {
            msm.TransitionTo("MOVING");
        }

        if (PlayerRef.IsOnFloor() && PlayerRef.Velocity.Length() == 0)
        {
            msm.TransitionTo("IDLE");
        }
    }

    public override void PhysicsUpdate(float delta)
    {
        Vector3 velocity = PlayerRef.Velocity;

        if (!PlayerRef.IsOnFloor())
        {
            velocity += PlayerRef.GetGravity() * (float)delta;
        }

        PlayerRef.Velocity = velocity;
        PlayerRef.MoveAndSlide();
    }
}
