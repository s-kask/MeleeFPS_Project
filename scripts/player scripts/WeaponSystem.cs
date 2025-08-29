using Godot;
using System;
using Godot.Collections;

public partial class WeaponSystem : Node
{
    

    public ActionHandler ActionHandler;
    private WeaponData _currentWeapon;
    private Dictionary<string, WeaponData> _weaponDatabase;
    private AnimationPlayer _animationPlayer;
     

    public override void _Ready()
    {
        ActionHandler = GetParent().GetNode<ActionHandler>("ActionHandler");
        _animationPlayer = GetParent().GetNode<AnimationPlayer>("AnimationPlayer");
        _LoadWeaponDatabase();
        
        ActionHandler.RegisterAction("ATTACK", HandleAttack);
        ActionHandler.RegisterAction("SPECIAL", HandleSpecialAttack);
        ActionHandler.RegisterAction("RELOAD", HandleReload);
        ActionHandler.RegisterAction("BLOCK", HandleBlock);
        ActionHandler.RegisterAction("WEAPON_SWITCH", HandleWeaponSwitch);
    }

    private void _LoadWeaponDatabase()
    {
        _weaponDatabase = new Dictionary<string, WeaponData>();
        _weaponDatabase["sword"] = GD.Load<WeaponData>("res://scripts/weapon scripts/sword.tres");
    }

    public void EquipWeapon(string weaponType)
    {
        if (_weaponDatabase.ContainsKey(weaponType))
        {
            _currentWeapon = _weaponDatabase[weaponType];
            //update everything
        }
    }

    public void HandleAttack()
    {
        var animName = $"attack_{_currentWeapon}";
        _animationPlayer.Play(animName);
    }

    public void HandleSpecialAttack()
    {
        var animName = $"s_attack_{_currentWeapon}";
        _animationPlayer.Play(animName);
    }

    public void HandleReload()
    {
        var animName = $"reload_{_currentWeapon}";
        _animationPlayer.Play(animName);
    }

    public void HandleBlock()
    {
        var animName = $"block_{_currentWeapon}";
        _animationPlayer.Play(animName); 
    }
    
    public void HandleWeaponSwitch(){}
    
    
}
