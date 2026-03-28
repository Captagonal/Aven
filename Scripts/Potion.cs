using Godot;
using System;

public partial class Potion : Node3D
{
	// Called when the node enters the scene tree for the first time.
	RigidBody3D body;
	Vector3 _prev_velocity = Vector3.Zero;
	public override void _Ready()
	{
		body = GetNode<RigidBody3D>("Cone");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
		if (body.GetContactCount() > 0){
			var Velocitychange = body.LinearVelocity - _prev_velocity;
			var collision_impulse = body.Mass * -1.0f * Velocitychange;
			var impulse = collision_impulse.Length();
			if (impulse > 16f)
			{
				CpuParticles3D cpuParticles3D = body.GetNode<CpuParticles3D>("part");
				cpuParticles3D.Emitting = true;
				cpuParticles3D.OneShot = true;
				body.GetNode<Node3D>("Cone").Visible = false;
				body.GetNode<Node3D>("Cone_002").Visible = false;
				cpuParticles3D.Visible = true;
				

			}
		}		
		_prev_velocity = body.LinearVelocity;
	}
	
}
