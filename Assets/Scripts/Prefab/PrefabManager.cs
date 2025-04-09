using Character;
using UnityEngine;

namespace Prefab
{
	public abstract class PrefabManager: MonoBehaviour
	{
		Collider2D _col;
		SpriteRenderer _sprite;
		public CharacterManager Parent { get; set; }
		float Duration { get; set; }


		void Awake()
		{
			_sprite = GetComponent<SpriteRenderer>();
			_col = GetComponent<Collider2D>();
		}

		protected virtual void Start()
		{
			_ = DisablePrefab(Duration);
			Destroy(gameObject, 10);
		}

		protected void SetParameters(CharacterManager parent, float duration)
		{
			Parent = parent;
			Duration = duration;
		}

		protected async Awaitable DisablePrefab(float waitTime = 0)
		{
			await Awaitable.WaitForSecondsAsync(waitTime);
			_col.enabled = false;
			_sprite.enabled = false;
			gameObject.layer = LayerMask.NameToLayer("DeadZone");
		}
	}
}