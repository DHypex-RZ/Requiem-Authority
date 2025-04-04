using Combat;
using UnityEngine;
using static Character.Enemy.EnemyState;

namespace Character.Enemy
{
	public class AssaultController: EnemyManager
	{
		RangeController RangeController { get; set; }

		protected override void Awake()
		{
			base.Awake();
			RangeController = GetComponent<RangeController>();
		}

		protected override void Update()
		{
			base.Update();

			State = Behaviour();

			if (!Enabled) return;

			if (State == InPatrol) return;

			if (!Hit || Hit.collider.CompareTag("Ground")) return;

			RangeController.Multiplier = State switch
			{
				InPatrol => 0f,
				InGuard => 0.5f,
				InDanger => 1.5f,
				_ => 1f
			};

			RangeController.Shoot("Bullet", TargetRotation);
		}

		void FixedUpdate()
		{
			if (!Enabled) return;

			MoveController.Move(Direction);
		}

		protected override EnemyState Behaviour()
		{
			float targetDistance = Mathf.Abs(TargetPosition.x);

			if (targetDistance > detectionDistance) return InPatrol;

			if (targetDistance < detectionDistance * 0.5f) return InDanger;

			if (targetDistance < detectionDistance * 0.75f) return InGuard;

			return InAlert;
		}
	}
}