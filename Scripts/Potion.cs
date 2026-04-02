using Godot;
using System;

public partial class Potion : Node3D
{
	// Potion Spawning code 


	// Potion potion = GD.Load<PackedScene>("res://Scenes/Potion.tscn").Instantiate<Potion>();
	// GetParent().AddChild(potion);
	// 	potion.GlobalPosition = _camera.GlobalPosition + _camera.GlobalTransform.Basis.Z * -2;
	// 	potion.ChangeColour(Colors.Green);
	// 	potion.Scale = new Vector3(0.38f, 0.38f, 0.38f);

	RigidBody3D body;
	Vector3 _prev_velocity = Vector3.Zero;
	AudioStreamPlayer3D audio;
	public int damage = 0;
	public int knockback = 0;
	public override void _Ready()
	{
		body = GetNode<RigidBody3D>("Cone");
		audio = GetNode<AudioStreamPlayer3D>("AudioStreamPlayer3D");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

		if (body.GetContactCount() > 0)
		{
			var Velocitychange = body.LinearVelocity - _prev_velocity;
			var collision_impulse = body.Mass * -1.0f * Velocitychange;
			var impulse = collision_impulse.Length();
			if (impulse > 13f)
			{
				foreach (Node3D collision in body.GetCollidingBodies())
				{
					if (collision is EvilCustomer character)
					{
						character.TakeDamage(damage);
						character.takeKnockback(knockback);
					}
				}
				CpuParticles3D cpuParticles3D = body.GetNode<CpuParticles3D>("part");
				cpuParticles3D.Emitting = true;
				cpuParticles3D.OneShot = true;
				body.GetNode<Node3D>("Cone").Visible = false;
				body.GetNode<Node3D>("Cone_002").Visible = false;
				cpuParticles3D.Visible = true;
				audio.Play();
				GetTree().CreateTimer(5).Timeout += () =>
			{
				QueueFree();
			};

			}
		}
		_prev_velocity = body.LinearVelocity;
	}
	public void ChangeColour(Color colour)
	{
		MeshInstance3D mesh = body.GetNode<MeshInstance3D>("Cone_002");
		StandardMaterial3D mat = new StandardMaterial3D();
		mat.Transparency = StandardMaterial3D.TransparencyEnum.Alpha;
		mat.AlbedoColor = new Color(colour.R, colour.G, colour.B, 0.827f);
		mesh.MaterialOverride = mat;

		body.GetNode<CpuParticles3D>("part").MaterialOverride = mat;


	}
}
