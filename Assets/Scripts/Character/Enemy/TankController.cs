using Combat;
using UnityEngine;
using static Character.Enemy.State;
using static Health.State;

namespace Character.Enemy
{
	[RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D))]
	public class TankController: EnemyManager
	{
		[SerializeField] float attackDistance;
		[SerializeField] EnemyManager enemyProtected;
		[SerializeField] PhysicalController physicalController;

		protected override void Awake()
		{
			base.Awake();
			physicalController.Character = GetComponent<TankController>();
		}

		protected override void Update()
		{
			base.Update();

			if (!Enabled) return;

			State = Behaviour();

			if (Mathf.Abs(Position.x) < attackDistance) physicalController.Attack("Attack");
		}

		void FixedUpdate() { MovementController.Move(Direction, State == InDanger && Hit && Hit.collider.tag is "Player" or "Shield"); }

		protected override State Behaviour()
		{
			if (Mathf.Abs(Position.x) > detectionDistance) return InGuard;

			if (enemyProtected) return enemyProtected.HealthController.State == Dead ? InDanger : InGuard;

			return InDanger;
		}
	}
}