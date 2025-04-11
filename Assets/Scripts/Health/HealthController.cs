using System;
using UnityEngine;
using static Health.State;

namespace Health
{
	[Serializable] public class HealthController
	{
		[SerializeField] float health;

		public float CurrentHealth { get; set; }
		public State State { get; set; } = Alive;

		public void TakeDamage(float damage)
		{
			CurrentHealth -= damage;
			if (CurrentHealth <= 0) State = Dead;
		}

		public void Heal(float heal) { CurrentHealth = CurrentHealth + heal > health ? health : CurrentHealth + heal; }

		public float Health { get => health; set => CurrentHealth = value; }
	}

	public enum State { Alive, Dead }
}