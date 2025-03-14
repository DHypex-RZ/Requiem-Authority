using System.Collections;
using UnityEngine;

namespace Mobility
{
	public class DashController: MonoBehaviour
	{
		[SerializeField] private float dashForce;
		[SerializeField] private float duration;
		[SerializeField] private float cooldown;

		private Rigidbody2D _rb;
		private bool _inCooldown;

		public bool IsDashing { get; private set; }

		private void Start() { _rb = GetComponent<Rigidbody2D>(); }

		public void RealizeDash()
		{
			if (_inCooldown) return;

			StartCoroutine(DashRoutine());
			_rb.AddForce(_rb.transform.right * dashForce, ForceMode2D.Impulse);
		}

		private IEnumerator DashRoutine()
		{
			IsDashing = _inCooldown = true;

			yield return new WaitForSeconds(duration);

			IsDashing = false;

			yield return new WaitForSeconds(cooldown - duration);

			_inCooldown = false;
		}
	}
}