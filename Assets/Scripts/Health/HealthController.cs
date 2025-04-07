using System;
using static Health.State;

namespace Health
{
	[Serializable] public class HealthController
	{
		public float health;

		public float CurrentHealth { get; set; }
		public State State { get; set; } = Alive;

		public void TakeDamage(float damage)
		{
			CurrentHealth -= damage;
			if (CurrentHealth <= 0) State = Dead;
		}

		public void Heal(float heal) { CurrentHealth = CurrentHealth + heal > health ? health : CurrentHealth + heal; }
	}

	public enum State { Alive, Dead }
}