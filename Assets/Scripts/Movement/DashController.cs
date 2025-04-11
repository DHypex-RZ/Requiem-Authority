using System;
using UnityEngine;

namespace Movement
{
	[Serializable] public class DashController
	{
		public bool Enabled { get; set; } = true;

		public MovementController MovementController { get; internal set; }

		[SerializeField] float dashForce;
		[SerializeField] float cooldown;
		[SerializeField] float duration;

		public void Dash()
		{
			if (!Enabled) return;

			Cooldown();
			MovementController.Rigidbody.AddForceX(MovementController.Rigidbody.transform.right.x * dashForce, ForceMode2D.Impulse);
		}

		async void Cooldown()
		{
			Enabled = MovementController.Enabled = false;

			await Awaitable.WaitForSecondsAsync(duration);

			MovementController.Enabled = true;

			await Awaitable.WaitForSecondsAsync(cooldown - duration);

			Enabled = true;
		}
	}
}