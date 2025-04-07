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
		public float speed;

		public float runSpeed;
		bool Running { get; set; }

		[Header("Jump")]
		public float jumpForce;

		public float groundCheckDistance = 1.225f;
		public bool IsGrounded { get; set; }

		public void Move(float direction, bool running = false)
		{
			if (!Enabled) return;

			Rigidbody.transform.rotation = direction switch { < 0 => Left, > 0 => Right, _ => Rigidbody.transform.rotation };
			if (IsGrounded) Running = running;
			Running &= running;
			Rigidbody.linearVelocityX = direction * (Running ? runSpeed : speed);
		}

		public void Jump()
		{
			if (IsGrounded && Enabled) Rigidbody.AddForceY(Rigidbody.transform.up.y * jumpForce, ForceMode2D.Impulse);
		}
	}
}