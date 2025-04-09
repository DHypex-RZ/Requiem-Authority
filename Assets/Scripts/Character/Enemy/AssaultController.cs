using Combat;
using UnityEngine;
using static Character.Enemy.State;

namespace Character.Enemy
{
	[RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D))]
	public class AssaultController: EnemyManager
	{
		[SerializeField] RangeController rangeController;

		protected override void Awake()
		{
			base.Awake();
			rangeController.Character = GetComponent<AssaultController>();
		}

		protected override void Update()
		{
			base.Update();

			if (!Enabled) return;

			State = Behaviour();

			if (State == InPatrol) return;

			if (!Hit || Hit.collider.CompareTag("Ground")) return;

			rangeController.Multiplier = State switch
			{
				InPatrol => 0f,
				InGuard => 0.5f,
				InDanger => 1.5f,
				_ => 1f
			};

			rangeController.Shoot("Bullet", Rotation);
		}

		void FixedUpdate()
		{
			if (!Enabled) return;

			MovementController.Move(Direction);
		}

		protected override State Behaviour()
		{
			float targetDistance = Mathf.Abs(Position.x);

			if (targetDistance > detectionDistance) return InPatrol;

			if (targetDistance < detectionDistance * 0.5f) return InDanger;

			if (targetDistance < detectionDistance * 0.75f) return InGuard;

			return InAlert;
		}
	}
}