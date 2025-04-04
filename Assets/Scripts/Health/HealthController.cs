using UnityEngine;
using static Health.HealthState;

namespace Health
{
	public class HealthController: MonoBehaviour
	{
		[SerializeField] float health;
		public float Health => health;
		public float CurrentHealth { get; private set; }
		public HealthState State { get; private set; } = Alive;

		void Awake() { CurrentHealth = health; }

		public void Awake(float newHealth)
		{
			health = newHealth;
			Awake();
		}

		public void Damage(float damage)
		{
			CurrentHealth -= damage;
			State = CurrentHealth <= 0 ? Dead : Alive;
		}

		public void Heal(float heal) { CurrentHealth = CurrentHealth + heal > health ? health : CurrentHealth + heal; }
	}

	public enum HealthState { Alive, Dead }
}