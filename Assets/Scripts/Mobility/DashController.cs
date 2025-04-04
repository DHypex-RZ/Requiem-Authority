using System.Collections;
using UnityEngine;

namespace Mobility
{
	[RequireComponent(typeof(MoveController), typeof(JumpController))]
	public class DashController: MobilityManager
	{
		[SerializeField] float cooldown;
		[SerializeField] float duration;

		MoveController _moveController;
		JumpController _jumpController;


		protected override void Awake()
		{
			base.Awake();
			_moveController = GetComponent<MoveController>();
			_jumpController = GetComponent<JumpController>();
		}

		public void Dash()
		{
			if (!Enabled) return;

			StartCoroutine(Cooldown());
			Rigidbody2D.AddForceX(transform.right.x * force, ForceMode2D.Impulse);
		}

		IEnumerator Cooldown()
		{
			Enabled = false;
			_moveController.Enabled = _jumpController.Enabled = false;

			yield return new WaitForSeconds(duration);

			_moveController.Enabled = _jumpController.Enabled = true;

			yield return new WaitForSeconds(cooldown - duration);

			Enabled = true;
		}
	}
}