using System.Collections;
using Character;
using UnityEngine;

namespace Prefab
{
	public abstract class PrefabManager: MonoBehaviour
	{
		SpriteRenderer _sprite;
		Collider2D _collider;
		public CharacterManager Parent { get; set; }
		float Duration { get; set; }

		protected virtual void Awake()
		{
			_collider = GetComponent<Collider2D>();
			_sprite = GetComponent<SpriteRenderer>();
		}

		protected virtual void Start()
		{
			StartCoroutine(Disable());
			Destroy(gameObject, 10);
		}

		public void SetParameters(CharacterManager parent, float duration)
		{
			Parent = parent;
			Duration = duration;
		}

		protected void DisablePrefab()
		{
			_sprite.enabled = false;
			_collider.enabled = false;
		}

		IEnumerator Disable()
		{
			yield return new WaitForSeconds(Duration);

			DisablePrefab();
		}
	}
}