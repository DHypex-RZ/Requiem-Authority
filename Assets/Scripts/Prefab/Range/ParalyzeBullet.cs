using System.Collections;
using Character;
using UnityEngine;

namespace Prefab.Range
{
	[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
	public class ParalyzeBullet: NormalBullet
	{
		new void OnTriggerEnter2D(Collider2D other)
		{
			if (other.CompareTag("Ground")) Destroy(gameObject);

			if (other.TryGetComponent(out CharacterManager character))
			{
				Instantiate(particle, character.transform, false);
				character.HealthController.TakeDamage(Damage);
				_ = DisablePrefab();
				StartCoroutine(Sleep(character));
			}
		}

		IEnumerator Sleep(CharacterManager character)
		{
			character.MovementController.Enabled = character.Enabled = false;
			character.MovementController.Rigidbody.linearVelocity = Vector2.zero;

			yield return new WaitForSeconds(2);

			particle.Stop();
			character.MovementController.Enabled = character.Enabled = true;
			Destroy(gameObject);
		}
	}
}