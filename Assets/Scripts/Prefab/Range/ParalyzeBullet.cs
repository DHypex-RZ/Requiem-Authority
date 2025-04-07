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
				character.HealthController.TakeDamage(Damage);
				DisablePrefab();
				StartCoroutine(Sleep(character));
			}
		}

		IEnumerator Sleep(CharacterManager character)
		{
			character.MovementController.Enabled = character.Enabled = false;
			character.MovementController.Rigidbody.linearVelocity = Vector2.zero;

			yield return new WaitForSeconds(2);

			character.MovementController.Enabled = character.Enabled = true;
			Destroy(gameObject);
		}
	}
}