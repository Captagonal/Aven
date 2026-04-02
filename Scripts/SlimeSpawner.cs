using Godot;
using System;

public partial class SlimeSpawner : Node3D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	
	public void SpawnSlime()
	{
		RigidBody3D Slime = GD.Load<PackedScene>("res://Scenes/Physics Objects/Box.tscn").Instantiate<RigidBody3D>();
		AddChild(Slime);
		Slime.Name = "Slime";
		Slime.AddToGroup("shop");
		Slime.SetMeta("Price", 1);
		Slime.GlobalPosition = GlobalPosition;
		Slime.GetNode<CollisionShape3D>("CollisionShape3D").Disabled = true;
		Slime.GravityScale = 0f;

		MeshInstance3D mesh = Slime.GetNode<MeshInstance3D>("MeshInstance3D");
		StandardMaterial3D mat = new StandardMaterial3D();
		mat.Transparency = StandardMaterial3D.TransparencyEnum.Alpha;
		mat.AlbedoColor = Color.FromHtml("#84ff00d9");
		
		mesh.MaterialOverride = mat;

		GetTree().CreateTimer(1).Timeout += () =>
			{
				Slime.GetNode<CollisionShape3D>("CollisionShape3D").Disabled = false;
				Slime.GravityScale = 3f;
			};
	}
}
