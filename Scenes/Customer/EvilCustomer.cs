using Godot;
using System;

public partial class EvilCustomer : CharacterBody3D
{
	public int health = 100;
	public float speed = 1;
	public PathFollow3D pathFollow;
	private bool isAtStore = false;
	private bool waiting = false;
	private bool beenAtStore = false;
	public Constants.CustomerTypes customerType = Constants.CustomerTypes.Bloblin; // Set the customer type to Bloblin for EvilCustomer
	public override void _Ready()
	{
		pathFollow = GetParent<PathFollow3D>();
	}

	public override void _PhysicsProcess(double delta)
	{

		// Move the customer along the path
		pathFollow.Progress += (float)delta * 3*speed; // Adjust speed as needed

		// Check if the customer has reached the store (you can adjust the offset value as needed)
		if (pathFollow.ProgressRatio >= .47 && pathFollow.ProgressRatio <= .48 && !beenAtStore) // Assuming 1000 is the offset where the store is located
		{
			Constants.TakeDamage(1,GetParent().GetParent()); // Reduce player's health by 1 when the customer reaches the store
			
			GetParent().QueueFree();
		}
		if (pathFollow.ProgressRatio >= .98)
		{
			GetParent().QueueFree(); // Remove the Evil customer from the scene
		}
	}

	public void TakeDamage(int damage)
	{
		health -= damage;
		if (health <= 0)
		{
			GetParent().QueueFree(); // Remove the Evil customer from the scene
		}
	}

	public void takeKnockback(float knockback)
	{
		pathFollow.Progress -= knockback; // Move the customer back along the path by the knockback amount
	}
}
