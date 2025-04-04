﻿using Character;
using UnityEngine;

namespace Prefab
{
	public class BulletController: PrefabManager
	{
		protected float Damage { get; private set; }
		internal float Speed { get; private set; }
		internal bool Surpass { get; set; }

		protected override void Start()
		{
			GetComponent<Rigidbody2D>().linearVelocity = transform.right * Speed;
			base.Start();
		}

		public void SetParameters(CharacterManager parent, float damage, float speed, bool surpass, float duration)
		{
			base.SetParameters(parent, duration);
			Damage = damage;
			Speed = speed;
			Surpass = surpass;
		}

		protected void OnTriggerEnter2D(Collider2D other)
		{
			if ((Surpass && Parent.CompareTag(other.tag)) || Parent.gameObject == other.gameObject) return;

			if (other.CompareTag("Ground")) Destroy(gameObject);

			if (other.TryGetComponent(out ShieldController shield))
			{
				if (Parent.CompareTag("Player")) return;

				shield.HealthController.Damage(Damage);
				Destroy(gameObject);
			}

			if (other.TryGetComponent(out CharacterManager character))
			{
				character.HealthController.Damage(Damage);
				Destroy(gameObject);
			}
		}
	}
}