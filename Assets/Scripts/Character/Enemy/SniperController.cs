using Combat;
using UnityEngine;
using static Character.Enemy.EnemyState;

namespace Character.Enemy
{
	public class SniperController: EnemyManager
	{
		[SerializeField] float platformLimitDistance;
		float _initialPosition;
		int _platformDirection = 1;
		RangeController RangeController { get; set; }

		protected override void Awake()
		{
			base.Awake();
			RangeController = GetComponent<RangeController>();
			_initialPosition = transform.position.x;
		}

		protected override void Update()
		{
			base.Update();
			State = Behaviour();

			if (!Enabled) return;

			if (transform.position.x >= _initialPosition + platformLimitDistance) _platformDirection = -1;
			else if (transform.position.x <= _initialPosition - platformLimitDistance) _platformDirection = 1;

			if ((Direction > 0 && transform.position.x >= _initialPosition + platformLimitDistance) ||
				(Direction < 0 && transform.position.x <= _initialPosition - platformLimitDistance)) Direction = 0;

			if (State == InPatrol) return;

			if (!Hit || Hit.collider.CompareTag("Ground")) return;

			RangeController.Multiplier = State == InDanger ? 1.5f : 1f;
			RangeController.Shoot(State == InDanger ? "PushBullet" : "Bullet", TargetRotation);
		}


		void FixedUpdate()
		{
			if (!Enabled) return;

			MoveController.Move(State switch { InPatrol => _platformDirection, InAlert => Direction, _ => 0 });
		}

		protected override EnemyState Behaviour()
		{
			float targetDistance = Mathf.Abs(TargetPosition.x);

			if (targetDistance > detectionDistance) return InPatrol;

			return targetDistance < detectionDistance * 0.3f ? InDanger : InAlert;
		}
	}
}