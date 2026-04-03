using Godot;
using System;

public partial class Settings : Control
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// if (Input.IsActionJustPressed("ui_cancel"))
		// {
		// 	Input.MouseMode = Input.MouseModeEnum.Captured;
		// 	GetParent().GetNode<Control>("Settings").Visible = false;
		// 	GetTree().Paused = false;
		// // }
	}

	public void musicVol(float value){
		float convertedValue = ((value - 50)/50) * 10;
		AudioServer.SetBusVolumeDb(AudioServer.GetBusIndex("Music"), convertedValue);
	}

	public void masterVol(float value){
				float convertedValue = ((value - 50)/50) * 10;

		AudioServer.SetBusVolumeDb(AudioServer.GetBusIndex("Master"), convertedValue);
	}

	public void _on_button_pressed()
	{
		Input.MouseMode = Input.MouseModeEnum.Captured;
		GetParent().GetNode<Control>("Settings").Visible = false;
		GetTree().Paused = false;
	}
}
