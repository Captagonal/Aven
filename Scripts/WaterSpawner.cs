using Godot;
using System;

public partial class WaterSpawner : Node3D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	public void SpawnWat()
	{
		RigidBody3D Water = GD.Load<PackedScene>("res://Scenes/water.tscn").Instantiate<RigidBody3D>();
		
		AddChild(Water);
		Water.Name = "Water";
		Water.AddToGroup("shop");
		Water.SetMeta("Price", 1);
		Water.GlobalPosition = GlobalPosition;
		Water.GetNode<CollisionShape3D>("CollisionShape3D").Disabled = true;
		Water.GravityScale = 0f;

		
		GetTree().CreateTimer(1).Timeout += () =>
			{
				Water.GetNode<CollisionShape3D>("CollisionShape3D").Disabled = false;
				Water.GravityScale = 3f;
			};
	}
}
