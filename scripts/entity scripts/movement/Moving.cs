using Godot;
using System;

public partial class Moving : State
{
    [Export] public float Speed = 10.0f;

    public override void Enter()
    {
        GD.Print("Move State");
    }

    public override void Update(float delta)
    {
        if (Input.IsActionJustPressed("jump"))
        {
            msm.TransitionTo("JUMP");
        }
    }

    public override void PhysicsUpdate(float delta)
    {
        Vector3 velocity = PlayerRef.Velocity;
        
        if (PlayerRef.Velocity.Y < 0 && !PlayerRef.IsOnFloor())
        {
            msm.TransitionTo("FALLING");
        }
        
        Vector2 inputDir = Input.GetVector("move_left", "move_right", "move_forward", "move_back");
        Vector3 direction = (PlayerRef.Transform.Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();

        if (direction != Vector3.Zero)
        {
            velocity.X = direction.X * Speed;
            velocity.Z = direction.Z * Speed;
        }
        else
        {
            msm.TransitionTo("IDLE");
        }
        
        PlayerRef.Velocity = velocity;
        PlayerRef.MoveAndSlide();
    }
}
