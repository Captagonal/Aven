using Godot;
using System;

public partial class Bloblin : Customer
{
	
	public override void _Ready()
	{
		customerType = Constants.CustomerTypes.Bloblin;
		pathFollow = GetParent<PathFollow3D>();
	}
}
