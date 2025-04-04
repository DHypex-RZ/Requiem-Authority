using Combat;
using UnityEngine;
using static Character.Enemy.EnemyState;
using static Health.HealthState;

namespace Character.Enemy
{
	public class TankController: EnemyManager
	{
		[SerializeField] float attackDistance;
		[SerializeField] EnemyManager enemyProtected;
		PhysicalController PhysicalController { get; set; }

		protected override void Awake()
		{
			base.Awake();
			PhysicalController = GetComponent<PhysicalController>();
		}

		protected override void Update()
		{
			base.Update();
			State = Behaviour();

			if (!Enabled) return;

			if (Mathf.Abs(TargetPosition.x) < attackDistance) PhysicalController.Attack("Attack");
		}

		void FixedUpdate()
		{
			if (!Enabled) return;

			MoveController.Move(Direction, State == InDanger);
		}

		protected override EnemyState Behaviour()
		{
			if (Mathf.Abs(TargetPosition.x) > detectionDistance) return InGuard;

			if (enemyProtected) return enemyProtected.HealthController.State == Dead ? InDanger : InGuard;

			return InDanger;
		}
	}
}