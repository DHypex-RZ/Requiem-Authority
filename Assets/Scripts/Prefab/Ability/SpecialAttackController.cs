using System.Collections;
using Character;
using UnityEngine;

namespace Prefab.Ability
{
	public class SpecialAttackController: AttackController
	{
		protected new void OnTriggerEnter2D(Collider2D other)
		{
			if (other.gameObject == Parent.gameObject) return;

			if (other.TryGetComponent(out CharacterManager character))
			{
				character.HealthController.Damage(Damage);
				StartCoroutine(Push(character));
			}
		}

		IEnumerator Push(CharacterManager character)
		{
			Parent.MoveController.Rigidbody2D.AddForceY(Force * 1.25f, ForceMode2D.Impulse);
			character.MoveController.Rigidbody2D.linearVelocity = Vector2.zero;
			character.MoveController.Rigidbody2D.AddForceY(Force * 1.25f, ForceMode2D.Impulse);
			character.Enabled = false;

			yield return new WaitForSeconds(0.15f);
			yield return new WaitUntil(() => character.JumpController.IsGrounded);

			character.Enabled = true;
			Destroy(gameObject);
		}
	}
}