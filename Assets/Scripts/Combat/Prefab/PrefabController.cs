using UnityEngine;

namespace Combat.Prefab
{
	public abstract class PrefabController: MonoBehaviour
	{
		public float Damage { get; private set; }
		protected float Lifetime { get; private set; }

		protected void SetParameters(float damage, float lifetime)
		{
			Damage = damage;
			Lifetime = lifetime;
		}
	}
}