using UnityEngine;

namespace Mobility
{
	[RequireComponent(typeof(JumpController))]
	public class MoveController: MobilityManager
	{
		static readonly Quaternion RotationLeft = Quaternion.Euler(0, 180, 0);
		static readonly Quaternion RotationRight = Quaternion.Euler(0, 0, 0);

		[SerializeField] float runForce;
		bool _running;

		JumpController _jumpController;

		protected override void Awake()
		{
			base.Awake();
			_jumpController = GetComponent<JumpController>();
		}

		public void Move(float direction, bool running = false)
		{
			if (!Enabled) return;

			transform.rotation = direction switch { < 0 => RotationLeft, > 0 => RotationRight, _ => transform.rotation };

			if (_jumpController.IsGrounded) _running = running;
			_running &= running;

			Rigidbody2D.linearVelocityX = direction * (_running ? runForce : force);
		}
	}
}