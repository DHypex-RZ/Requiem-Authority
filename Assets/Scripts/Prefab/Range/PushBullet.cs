using System.Collections;
using Character;
using Movement;
using UnityEngine;

namespace Prefab.Range
{
	[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
	public class PushBullet: NormalBullet
	{
		new void OnTriggerEnter2D(Collider2D other)
		{
			if (other.CompareTag("Ground")) Destroy(gameObject);

			if (Parent.gameObject == other.gameObject) return;

			if (other.TryGetComponent(out CharacterManager character))
			{
				character.HealthController.TakeDamage(Damage);
				DisablePrefab();
				StartCoroutine(Push(character.MovementController));
			}
		}

		IEnumerator Push(MovementController controller)
		{
			Vector3 right = transform.right;
			controller.Enabled = false;
			controller.Rigidbody.AddForce(right * (Speed * 0.45f), ForceMode2D.Impulse);

			yield return new WaitForSeconds(0.15f);

			controller.Enabled = true;
			Destroy(gameObject);
		}
	}
}