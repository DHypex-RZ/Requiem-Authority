using System;
using UnityEngine;

namespace Movement
{
	[Serializable] public class MovementController
	{
		static readonly Quaternion Left = Quaternion.Euler(0, 180, 0);
		static readonly Quaternion Right = Quaternion.Euler(0, 0, 0);

		public bool Enabled { get; set; } = true;
		public Rigidbody2D Rigidbody { get; set; }

		[Header("Move")]
		[SerializeField] float speed;

		[SerializeField] float runSpeed;
		bool _running;

		[Header("Jump")]
		[SerializeField] float jumpForce;

		[SerializeField] float groundCheckDistance = 1.225f;
		public bool Grounded { get; set; }

		public void Move(float direction, bool running = false)
		{
			if (!Enabled) return;

			Rigidbody.transform.rotation = direction switch { < 0 => Left, > 0 => Right, _ => Rigidbody.transform.rotation };
			_running = Grounded ? running : _running && running;
			Rigidbody.linearVelocityX = direction * (_running ? runSpeed : speed);
		}

		public void Jump()
		{
			if (Grounded && Enabled) Rigidbody.AddForceY(Rigidbody.transform.up.y * jumpForce, ForceMode2D.Impulse);
		}

		public float JumpForce { get => jumpForce; internal set => jumpForce = value; }
		public float GroundCheckDistance => groundCheckDistance;
	}
}