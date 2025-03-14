using Combat;
using Health;
using UnityEngine;
using static Enum.EnemyState;
using static Enum.HealthState;

namespace Enemy
{
	public class TankController: EnemyController
	{
		[SerializeField] private HealthController enemyProtected;

		protected override void Start()
		{
			base.Start();

			physicCombat = GetComponent<PhysicCombatController>();
			movement.CanRun = true;
			if (enemyProtected) enemyProtected = enemyProtected.GetComponent<HealthController>();
		}

		protected override void Update()
		{
			base.Update();

			if (enemyProtected) state = enemyProtected.State == Dead ? InDanger : InGuard;
			else state = InDanger;

			if (Mathf.Abs(targetPosition.x) < maxDetectionDistance) physicCombat.RealizeAttack();
		}

		private void FixedUpdate() { movement.Move(direction, state == InDanger); }
	}
}