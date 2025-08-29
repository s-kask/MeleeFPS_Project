using Godot;
using System;

[GlobalClass]
public partial class WeaponData : Resource
{
    [Export] private string _type = "";
    public string Type => _type;
    
    [Export] private string _name = "";
    public string Name => _name;
    
    [Export] private int _dmg = 0;
    public int Dmg => _dmg;
    
    [Export] private int _ammoCount = 0;
    public int AmmoCount => _ammoCount;
    
    [Export] private float _attackSpeed = 1f;
    public float AttackSpeed => _attackSpeed;
    
    [Export] private float _blockPower = 5f;
    public float BlockPower => _blockPower;
    
    [Export] private float _parryWindow = 0.2f;
    public float ParryWindow => _parryWindow;
    
    [Export] private Mesh _mesh;
    public Mesh Mesh => _mesh;
}
