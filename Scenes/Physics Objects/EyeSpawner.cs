using Godot;
using System;

public partial class EyeSpawner : Node3D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void SpawnEye()
	{
		RigidBody3D eye = GD.Load<PackedScene>("res://Scenes/Physics Objects/Ball.tscn").Instantiate<RigidBody3D>();
		AddChild(eye);
		eye.Name = "Eye";
		eye.AddToGroup("shop");
		eye.SetMeta("Price", 1);
		eye.GlobalPosition = GlobalPosition;
		eye.GetNode<CollisionShape3D>("CollisionShape3D").Disabled = true;
		eye.GravityScale = 0f;
		GetTree().CreateTimer(1).Timeout += () =>
			{
				eye.GetNode<CollisionShape3D>("CollisionShape3D").Disabled = false;
				eye.GravityScale = 3f;
			};
	}
}
