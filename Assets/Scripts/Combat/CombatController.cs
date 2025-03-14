using UnityEngine;

namespace Combat
{
	public abstract class CombatController: MonoBehaviour
	{
		[SerializeField] protected float damage;
		[SerializeField] protected float lifetime;
		[SerializeField] protected float cooldown;

		protected const string ROUTE = "Prefabs/combat/";

		protected GameObject prefab;
		private float _timer;

		private void Update() { _timer += Time.deltaTime; }

		protected bool CheckTimer()
		{
			if (_timer < cooldown) return true;

			_timer = 0;

			return false;
		}
	}
}