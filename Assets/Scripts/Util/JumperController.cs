using Character.Enemy;
using Character.Player;
using Prefab.Shield;
using UnityEngine;
using static Health.State;

namespace Util
{
	[RequireComponent(typeof(BoxCollider2D))]
	public class JumperController: MonoBehaviour
	{
		[SerializeField] float jumpForce;
		[SerializeField] EnemyManager activator;

		PlayerController _player;
		SpriteRenderer _sprite;
		Collider2D _collider;

		void Awake()
		{
			_collider = GetComponent<Collider2D>();
			_sprite = GetComponent<SpriteRenderer>();
			_player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
		}

		void Start()
		{
			if (!activator) return;

			_sprite.color = Color.gray;
			_collider.enabled = false;
		}

		void Update()
		{
			if (!activator || activator.HealthController.State == Alive) return;

			_sprite.color = Color.white;
			_collider.enabled = true;
		}


		void OnTriggerEnter2D(Collider2D other)
		{
			if (other.TryGetComponent(out ShieldController shield)) Destroy(shield.gameObject);

			if (!other.CompareTag("Player")) return;

			if (_player.HealthController.State == Dead) return;

			_player.MovementController.Rigidbody.linearVelocity = Vector2.zero;
			_player.MovementController.Rigidbody.AddRelativeForceY(jumpForce, ForceMode2D.Impulse);
		}
	}
}