using System;
using System.Collections;
using UnityEngine;

namespace Movement
{
	[Serializable] public class DashController
	{
		public bool Enabled { get; set; } = true;

		public MovementController MovementController { get; internal set; }

		public float dashForce;
		public float cooldown;
		public float duration;

		public void Dash(Action coroutine)
		{
			if (!Enabled) return;

			coroutine.Invoke();
			MovementController.Rigidbody.AddForceX(MovementController.Rigidbody.transform.right.x * dashForce, ForceMode2D.Impulse);
		}

		public IEnumerator Cooldown()
		{
			Enabled = MovementController.Enabled = false;

			yield return new WaitForSeconds(duration);

			MovementController.Enabled = true;

			yield return new WaitForSeconds(cooldown);

			Enabled = true;
		}
	}
}