using Godot;
using System;

public partial class Customer : CharacterBody3D
{
	private PathFollow3D pathFollow;
	private bool isAtStore = false;
	public override void _Ready()
	{
		pathFollow = GetParent<PathFollow3D>();
	}

	public override void _PhysicsProcess(double delta)
	{
		 if (isAtStore)
		 {
			 // Customer is at the store, do nothing or perform some action
			 GetTree().CreateTimer(10).Timeout += () => {
				 isAtStore = false;
			 };
			 return;
		 } else {
			 // Move the customer along the path
			 pathFollow.Progress += (float)delta * 3; // Adjust speed as needed

			 // Check if the customer has reached the store (you can adjust the offset value as needed)
			 if (pathFollow.ProgressRatio >= .47 && pathFollow.ProgressRatio <= .48) // Assuming 1000 is the offset where the store is located
			 {
				 isAtStore = true;
			 }
			 if (pathFollow.ProgressRatio >= .98) // Assuming 1000 is the offset where the store is located
			 {
				 QueueFree(); // Remove the customer from the scene
			 }
		 }
			 
		 

		
	}
}
