using Godot;
using System;

public partial class PlayerController : CharacterBody3D
{
	[Export] private float _mouseSensitivity = 0.20f;
	private int _maxRotation = 90;
	private int _minRotation = -90;

	private MovementStateMachine _msm;
	private Node3D _pov;

	public string[] BasicMoveActions = { "move_forward", "move_back", "move_left", "move_right" };

	public override void _Ready()
	{
		_msm = GetNode<MovementStateMachine>("MovementStateMachine");
		_msm._Ready();
		_pov = GetNode<Node3D>("Head");
		Input.MouseMode = Input.MouseModeEnum.Captured;
	}

	public override void _UnhandledInput(InputEvent @event)
	{
		
	}

	//handles mouse movement
	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventMouseMotion mouseMotion)
		{
			var yRotation = RotationDegrees.Y - mouseMotion.Relative.X * _mouseSensitivity;
			var xRotation = RotationDegrees.X - mouseMotion.Relative.Y * _mouseSensitivity;
			xRotation = Mathf.Clamp(xRotation, _minRotation, _maxRotation);
			RotationDegrees = new Vector3(xRotation, yRotation, RotationDegrees.Z);

		}
	}

	public override void _Process(double delta)
	{
		
	}


	public override void _PhysicsProcess(double delta)
	{

	}
}
