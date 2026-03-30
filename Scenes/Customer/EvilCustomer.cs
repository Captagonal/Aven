using Godot;
using System;

public partial class EvilCustomer : CharacterBody3D
{
	public int health = 100;
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
		pathFollow.Progress += (float)delta * 3; // Adjust speed as needed

		// Check if the customer has reached the store (you can adjust the offset value as needed)
		if (pathFollow.ProgressRatio >= .47 && pathFollow.ProgressRatio <= .48 && !beenAtStore) // Assuming 1000 is the offset where the store is located
		{
			Constants.health -= 1; // Decrease health by 1 when the Evil customer reaches the store
			if (Constants.health <= 0){
				Constants.health = 0; // Ensure health doesn't go below 0
				// You can add additional logic here for when the player runs out of health, such as ending the game or showing a game over screen.
				GD.Print("Game Over! The Evil customer has depleted your health.");
				// For example, you could emit a signal to notify other parts of the game about the game over state.
				// EmitSignal(nameof(GameOver));
				// Or you could change the scene to a game over screen:
				// GetTree().ChangeScene("res://Scenes/GameOver.tscn");
			}
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
