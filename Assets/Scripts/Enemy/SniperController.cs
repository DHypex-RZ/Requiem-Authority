using Combat;
using UnityEngine;
using static Enum.EnemyState;

namespace Enemy
{
	public class SniperController: EnemyController
	{
		[SerializeField] private float platformLimitDistance;

		private float _initialPosition;
		private int _platformDirection = 1;


		protected override void Start()
		{
			base.Start();
			rangeCombat = GetComponent<RangeCombatController>();
			_initialPosition = transform.position.x;
		}

		protected override void Update()
		{
			base.Update();

			state = Mathf.Abs(targetPosition.x) > maxDetectionDistance ? InPatrol : InAlert;

			if (transform.position.x >= _initialPosition + platformLimitDistance) _platformDirection = -1;
			else if (transform.position.x <= _initialPosition - platformLimitDistance) _platformDirection = 1;

			if ((direction > 0 && transform.position.x >= _initialPosition + platformLimitDistance) ||
				(direction < 0 && transform.position.x <= _initialPosition - platformLimitDistance)) direction = 0;

			if (!target || target.collider.tag is not ("Player" or "Enemy")) return;

			rangeCombat.Shoot(targetRotation);
		}

		private void FixedUpdate() { movement.Move(state switch { InPatrol => _platformDirection, InAlert => direction, _ => 0 }); }
	}
}