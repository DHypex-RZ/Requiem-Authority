using System.Collections;
using Character;
using Movement;
using Prefab.Range;
using UnityEngine;

namespace Prefab.Physical
{
	[RequireComponent(typeof(Collider2D))]
	public class NormalAttack: PrefabManager
	{
		protected float Damage { get; private set; }
		protected float Force { get; private set; }

		protected override void Start()
		{
			transform.position = Parent.transform.position + new Vector3(Parent.transform.rotation.y == 0 ? 1.35f : -1.35f, 0, 0);
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

			if (other.TryGetComponent(out NormalBullet bullet))
			{
				bullet.Surpass = false;
				bullet.Parent = Parent;
				bullet.GetComponent<SpriteRenderer>().flipX = true;
				bullet.GetComponent<Rigidbody2D>().linearVelocity = bullet.transform.right * -bullet.Speed;
			}

			if (other.TryGetComponent(out CharacterManager character))
			{
				character.HealthController.TakeDamage(Damage);
				DisablePrefab();
				StartCoroutine(Push(character.MovementController));
			}
		}

		IEnumerator Push(MovementController controller)
		{
			controller.Enabled = false;
			controller.Rigidbody.AddForceX(Parent.transform.right.x * Force, ForceMode2D.Impulse);

			yield return new WaitForSeconds(.15f);

			controller.Enabled = true;
			Destroy(gameObject);
		}
	}
}