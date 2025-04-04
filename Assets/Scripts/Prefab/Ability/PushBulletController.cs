using System.Collections;
using Character;
using Mobility;
using UnityEngine;

namespace Prefab.Ability
{
	public class PushBulletController: BulletController
	{
		new void OnTriggerEnter2D(Collider2D other)
		{
			if (other.CompareTag("Ground")) Destroy(gameObject);

			if (Parent.gameObject == other.gameObject) return;

			if (other.TryGetComponent(out CharacterManager character))
			{
				character.HealthController.Damage(Damage);
				DisablePrefab();
				StartCoroutine(Push(character.MoveController));
			}
		}

		IEnumerator Push(MobilityManager controller)
		{
			Vector3 right = transform.right;
			controller.Enabled = false;
			controller.Rigidbody2D.AddForce(right * (Speed * 0.5f), ForceMode2D.Impulse);

			yield return new WaitForSeconds(0.15f);

			controller.Enabled = true;
			Destroy(gameObject);
		}
	}
}