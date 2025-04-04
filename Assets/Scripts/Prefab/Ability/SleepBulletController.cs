using System.Collections;
using Character;
using UnityEngine;

namespace Prefab.Ability
{
	public class SleepBulletController: BulletController
	{
		new void OnTriggerEnter2D(Collider2D other)
		{
			if (other.CompareTag("Ground")) Destroy(gameObject);

			if (other.TryGetComponent(out CharacterManager character))
			{
				character.HealthController.Damage(Damage);
				DisablePrefab();
				StartCoroutine(Sleep(character));
			}
		}

		IEnumerator Sleep(CharacterManager character)
		{
			character.Enabled = false;
			character.MoveController.Rigidbody2D.linearVelocity = Vector2.zero;

			yield return new WaitForSeconds(2);

			character.Enabled = true;
			Destroy(gameObject);
		}
	}
}