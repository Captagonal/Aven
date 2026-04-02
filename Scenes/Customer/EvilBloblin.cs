using Godot;
using System;

public partial class EvilBloblin : EvilCustomer
{
	public override void _Ready()
	{
		base._Ready();
		customerType = Constants.CustomerTypes.Bloblin; // Set the customer type to Bloblin for EvilBloblin
		health = 150; // Set the health of the EvilBloblin to 150
		speed = .75f; // Set the speed of the EvilBloblin to 0.75 (slower than regular customers)
	}
}
