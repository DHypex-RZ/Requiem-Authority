using System.Collections;
using Character;
using UnityEngine;

namespace Prefab.Physical
{
	[RequireComponent(typeof(Collider2D))]
	public class PushUpAttack: NormalAttack
	{
		protected new void OnTriggerEnter2D(Collider2D other)
		{
			if (other.gameObject == Parent.gameObject) return;

			if (other.TryGetComponent(out CharacterManager character))
			{
				Instantiate(particle, transform.position, particle.transform.rotation);
				character.HealthController.TakeDamage(Damage);
				StartCoroutine(PushUp(character));
			}
		}

		IEnumerator PushUp(CharacterManager character)
		{
			const float parentJumpForce = 8.35f;
			const float forceMultiplier = 1.5f;
			Parent.MovementController.JumpForce *= forceMultiplier;
			character.MovementController.Rigidbody.linearVelocity = Vector2.zero;
			character.MovementController.Enabled = character.Enabled = false;
			character.MovementController.Rigidbody.AddForceY(Force * forceMultiplier, ForceMode2D.Impulse);

			yield return new WaitForSeconds(0.15f);
			yield return new WaitUntil(() => character.MovementController.Grounded);

			Parent.MovementController.JumpForce = parentJumpForce;
			character.MovementController.Enabled = character.Enabled = true;
			Destroy(gameObject);
		}
	}
}