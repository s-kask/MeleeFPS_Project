using Godot;
using System;
using System.Collections.Generic;

public partial class ActionHandler : Node
{
	private Dictionary<string, Action> _actions;

	public override void _Ready()
	{
		_actions = new Dictionary<string, Action>();
	}


	public void RegisterAction(string actionName, Action action)
	{
		if (string.IsNullOrEmpty(actionName))
		{
			GD.Print("actionName empty or null");
			return;
		}

		if (action == null)
		{
			GD.Print("cant register null action");
			return;
		}

		if (!_actions.ContainsKey(actionName))
		{
			GD.Print($"registered {actionName}");
			_actions[actionName] = action;
		}
	}

	public void UnregisterAction(string actionName)
	{
		if (string.IsNullOrEmpty(actionName))
		{
			GD.Print("Action name cant me null/empty");
			return;
		}
		
		if (_actions.ContainsKey(actionName))
		{
			_actions.Remove(actionName);
			GD.Print($"Action: {actionName}, removed");
		}
		else
		{
			GD.Print($"Cannot unregister {actionName}, since it doesnt exist");
		}
	}

	public void HandleInput(string actionName)
	{
		if (_actions.ContainsKey(actionName))
		{
			_actions[actionName].Invoke();
		}
	}

	public Boolean IsActionRegistered(string actionName)
	{
		return _actions.ContainsKey(actionName);
	}
}
