using Godot;
using System;

public partial class Kids : Customer
{
	
	public override void _Ready()
	{
		customerType = Constants.CustomerTypes.Kids;
		pathFollow = GetParent<PathFollow3D>();
	}
}
