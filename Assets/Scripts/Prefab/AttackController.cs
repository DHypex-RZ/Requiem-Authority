using System.Collections;
using Character;
using Mobility;
using UnityEngine;

namespace Prefab
{
	[RequireComponent(typeof(Collider2D))]
	public class AttackController: PrefabManager
	{
		protected float Damage { get; private set; }
		protected float Force { get; private set; }

		protected override void Start()
		{
			transform.position = Parent.transform.position + new Vector3(Parent.transform.rotation.y == 0 ? 1.25f : -1.25f, 0, 0);
			base.Start();
		}

		public void SetParameters(CharacterManager parent, float damage, float force, float duration)
		{
			base.SetParameters(parent, duration);
			Damage = damage;
			Force = force;
		}

		protected void OnTriggerEnter2D(Collider2D other)
		{
			if (other.gameObject == Parent.gameObject) return;

			if (other.TryGetComponent(out BulletController projectile))
			{
				projectile.Surpass = false;
				projectile.Parent = Parent;
				projectile.GetComponent<SpriteRenderer>().flipX = true;
				projectile.GetComponent<Rigidbody2D>().linearVelocity = projectile.transform.right * -projectile.Speed;
			}

			if (other.TryGetComponent(out ShieldController shield)) shield.HealthController.Damage(Damage);

			if (other.TryGetComponent(out CharacterManager character))
			{
				character.HealthController.Damage(Damage);
				DisablePrefab();
				StartCoroutine(Push(character.MoveController));
			}
		}

		IEnumerator Push(MobilityManager controller)
		{
			controller.Enabled = false;
			controller.Rigidbody2D.AddForceX(Parent.transform.right.x * Force, ForceMode2D.Impulse);

			yield return new WaitForSeconds(.15f);

			controller.Enabled = true;
			Destroy(gameObject);
		}
	}
}