using Godot;
using System;

public partial class State : Node
{
	private PlayerController _playerRef;
	public MovementStateMachine msm;
	
	protected PlayerController PlayerRef
	{
		get
		{
			if (_playerRef == null)
				_playerRef = GetNode<PlayerController>("../..");
			return _playerRef;
		}
	}

	public virtual void Enter() {}
	public virtual void Exit() {}
    
	public virtual void Ready(){}
	public virtual void Update(float delta){}

	public virtual void PhysicsUpdate(float delta)
	{
		Vector3 velocity = PlayerRef.Velocity;

		if (!PlayerRef.IsOnFloor())
		{
			velocity += PlayerRef.GetGravity() * (float)delta;
		}

		PlayerRef.Velocity = velocity;
		PlayerRef.MoveAndSlide();
	}

	public virtual void HandleInput(InputEvent @event) {}
}
