using System.Collections;
using Combat.Prefab;
using UnityEngine;

namespace Combat
{
	public class PhysicCombatController: CombatController
	{
		public bool IsAttacking { get; private set; }

		private void Start() { prefab = Resources.Load<GameObject>(ROUTE + "Attack"); }

		public void RealizeAttack()
		{
			if (CheckTimer()) return;

			StartCoroutine(AttackRoutine());
			GameObject attack = Instantiate(prefab, transform.position, Quaternion.identity);
			AttackController controller = attack.GetComponent<AttackController>();
			controller.SetParameters(damage, lifetime, gameObject);
		}

		private IEnumerator AttackRoutine()
		{
			IsAttacking = true;

			yield return new WaitForSeconds(lifetime);

			IsAttacking = false;
		}
	}
}