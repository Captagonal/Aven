using System.Collections.Generic;
using Godot;

public partial class Player : CharacterBody3D
{
	// How fast the player moves in meters per second.
	[Export]
	public int Speed { get; set; } = 7;
	// The downward acceleration when in the air, in meters per second squared.
	[Export]
	public int FallAcceleration { get; set; } = 30;

	[Export]
	public int JumpImpulse { get; set; } = 12;

	[Export]
	public float CameraSensitivity { get; set; } = .006f;

	private Node3D _head, _camera, pickedUpObject;
	private RayCast3D _lookingAt;
	private Vector3 _targetVelocity = Vector3.Zero;
	private float bounceCooldown = 0f;
	public override void _Ready()
	{
		//-----Get Nodes-----
		_head = GetNode<Node3D>("Head");
		_camera = _head.GetNode<Node3D>("Camera3D");
		_lookingAt = _camera.GetNode<RayCast3D>("RayCast3D");
		pickedUpObject = null;

		//-----Set Mouse Mode-----
		Input.MouseMode = Input.MouseModeEnum.Captured;
	

	}

	public override void _PhysicsProcess(double delta)
	{

		if (bounceCooldown > 0f)
		{
			bounceCooldown -= (float)delta;
		}

		if (!IsOnFloor())
			_targetVelocity.Y -= FallAcceleration * (float)delta;
		if (Input.IsActionJustPressed("jump") && IsOnFloor())
			_targetVelocity.Y = JumpImpulse;

		if (bounceCooldown <= 0)
		{
			Vector2 input = Input.GetVector("move_left", "move_right", "move_forward", "move_back");
			Vector3 direction = (_head.GlobalTransform.Basis * new Vector3(input.X, 0, input.Y)).Normalized();
			
			if (direction != Vector3.Zero)
			{
				_targetVelocity.X = direction.X * Speed;
				_targetVelocity.Z = direction.Z * Speed;
			}
			else
			{
				_targetVelocity.X = Mathf.MoveToward(_targetVelocity.X, 0, Speed * 3.5f * (float)delta);
				_targetVelocity.Z = Mathf.MoveToward(_targetVelocity.Z, 0, Speed * 3.5f * (float)delta);
			}
		}

		Velocity = _targetVelocity;

		MoveAndSlide();

		//-----Check for Bounce Collisions-----
		for (int i = 0; i < GetSlideCollisionCount(); i++)
		{
			KinematicCollision3D collision = GetSlideCollision(i);
			// Check if the collider is in the "Bouncy" group
			if (collision.GetCollider() is StaticBody3D collider && collider.IsInGroup("Bouncy"))
			{
				Vector3 bounce = _targetVelocity.Bounce(collision.GetNormal().Normalized()) * .9f;
				if (bounce.Length() > 5f)
				{
					_targetVelocity = bounce;
					bounceCooldown = 0.2f;
					break;
				}
				else
				{
					bounceCooldown = 0f;
				}


			}
		}

		//-----Camera Control for controller-----
		Vector2 inputCam = Input.GetVector("camera_left", "camera_right", "camera_up", "camera_down");
		if (inputCam != Vector2.Zero)
		{
			_head.RotateY(-inputCam.X * CameraSensitivity * 13);
			_camera.RotateX(-inputCam.Y * CameraSensitivity * 13);
			Vector3 camRotation = _camera.Rotation;

			camRotation.X = Mathf.Clamp(camRotation.X, Mathf.DegToRad(-80f), Mathf.DegToRad(80f));

			_camera.Rotation = camRotation;
		}

		if (_lookingAt.IsColliding()){
			Node3D lookingAtNode = _lookingAt.GetCollider() as Node3D;
			
			if (pickedUpObject == null && lookingAtNode != null && lookingAtNode.IsInGroup("Grabable") && Input.IsActionJustPressed("grab"))
			{
				if (lookingAtNode.IsInGroup("shop"))
				{
					if ((int)lookingAtNode.GetMeta("Price") > Constants.coins)
					{
						GD.Print("Not Enough Coins");
						return;
					}
					else
					{
						Constants.SpendCoin((int)lookingAtNode.GetMeta("Price"), this);
						// Constants.coins -= (int)lookingAtNode.GetMeta("Price");
						lookingAtNode.RemoveFromGroup("shop");
						if (lookingAtNode.Name.ToString().Contains("Eye"))
						{
							lookingAtNode.GetParent<EyeSpawner>().SpawnEye();
						}
						else if (lookingAtNode.Name.ToString().Contains("Slime"))
						{
							lookingAtNode.GetParent<SlimeSpawner>().SpawnSlime();
						}
						else if (lookingAtNode.Name.ToString().Contains("Flower"))
						{
							// lookingAtNode.GetParent<FlowerSpawner>().SpawnFlower();
						}
						else if (lookingAtNode.Name.ToString().Contains("Water"))
						{
							lookingAtNode.GetParent<WaterSpawner>().SpawnWat();
						}

						
						GD.Print("Bought Item for " + lookingAtNode.GetMeta("Price") + " coins, " + Constants.coins + " coins remaining");
					}
				}
				Generic6DofJoint3D joint = _camera.GetNode<Node3D>("Holding").GetNode<Generic6DofJoint3D>("joint");
					lookingAtNode.GlobalPosition = _camera.GetNode<Node3D>("Holding").GlobalPosition;
					joint.NodeB = lookingAtNode.GetPath();

					var rigidBody = lookingAtNode as RigidBody3D;
					lookingAtNode.GetTree().CreateTimer(0.1f).Timeout += () =>
					{
						pickedUpObject = lookingAtNode;
					};
					rigidBody.AddCollisionExceptionWith(this);
			}
			if (lookingAtNode!= null && lookingAtNode.IsInGroup("Button") && Input.IsActionJustPressed("interact"))
			{
				
				if (lookingAtNode.GetParent().GetParent() is ButtonConsole console)
				{
					console.PressButton(lookingAtNode.GetParent().Name);
				}
				if (lookingAtNode.GetParent().GetParent().GetParent() is Cauldron console2)
				{
					console2.pushButton(lookingAtNode);
				}
			}
			
		}
		///-----Grabbing Objects-----
		
		
			// Potion potion = GD.Load<PackedScene>("res://Scenes/Potion.tscn").Instantiate<Potion>();
			
			// GetParent().AddChild(potion);
			// 	potion.GlobalPosition = _camera.GlobalPosition + _camera.GlobalTransform.Basis.Z * -2;
			// 	potion.ChangeColour(Colors.Green);
			// 	potion.Scale = new Vector3(0.38f, 0.38f, 0.38f);
		
		

		if (pickedUpObject != null)
		{
			var rigidBody = pickedUpObject as RigidBody3D;
			Generic6DofJoint3D joint = _camera.GetNode<Node3D>("Holding").GetNode<Generic6DofJoint3D>("joint");

			// Drop Held Object by removing the joint and allowing physics to take over again
			if (Input.IsActionJustPressed("grab"))
			{
				joint.NodeB = null;
				rigidBody.RemoveCollisionExceptionWith(this);

				pickedUpObject.GetTree().CreateTimer(0.1f).Timeout += () =>
				{
					pickedUpObject = null;
				};

			}


			if (Input.IsActionJustPressed("throw"))
			{
				// Throw Held Object by removing the joint and applying an impulse in the direction the camera is facing
				joint.NodeB = null;
				rigidBody.RemoveCollisionExceptionWith(this);
				rigidBody.ApplyCentralImpulse(_camera.GlobalTransform.Basis.Z * -13);
				pickedUpObject.GetTree().CreateTimer(0.1f).Timeout += () =>
				{
					pickedUpObject = null;
				};
			}
		}
	}

	public void emptyHand(){
		_camera.GetNode<Node3D>("Holding").GetNode<Generic6DofJoint3D>("joint").NodeB = null;
		pickedUpObject = null;
	}

	public override void _Input(InputEvent @event)
	{
		Vector2 inputCam = Input.GetVector("camera_left", "camera_right", "camera_up", "camera_down");
		if (@event is InputEventMouseMotion mouseMotion && (inputCam == Vector2.Zero))
		{


			_head.RotateY(-mouseMotion.Relative.X * CameraSensitivity);
			_camera.RotateX(-mouseMotion.Relative.Y * CameraSensitivity);
			Vector3 camRotation = _camera.Rotation;

			camRotation.X = Mathf.Clamp(camRotation.X, Mathf.DegToRad(-80f), Mathf.DegToRad(80f));

			_camera.Rotation = camRotation;
		}
		else if (@event is InputEventKey keyEvent && keyEvent.IsPressed() && keyEvent.Keycode == Key.Escape)
		{
			Input.MouseMode = Input.MouseModeEnum.Visible;
		}
	}
}
