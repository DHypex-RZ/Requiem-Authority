using Health;
using Mobility;
using UnityEngine;
using static Health.HealthState;

namespace Util
{
	[RequireComponent(typeof(Collider2D))]
	public class JumperController: MonoBehaviour
	{
		[SerializeField] float jumpForce;
		[SerializeField] HealthController activator;

		MoveController _player;
		SpriteRenderer _sprite;
		Collider2D _collider;

		void Awake()
		{
			_collider = GetComponent<Collider2D>();
			_sprite = GetComponent<SpriteRenderer>();
			_player = GameObject.FindWithTag("Player").GetComponent<MoveController>();
		}

		void Start()
		{
			if (!activator) return;

			_sprite.color = Color.red;
			_collider.enabled = false;
		}

		void Update()
		{
			if (!activator || activator.State == Alive) return;

			_sprite.color = Color.green;
			_collider.enabled = true;
		}


		void OnTriggerEnter2D(Collider2D other)
		{
			if (!other.CompareTag("Player")) return;

			_player.Rigidbody2D.AddForce(_player.Rigidbody2D.transform.up * jumpForce, ForceMode2D.Impulse);
		}
	}
}