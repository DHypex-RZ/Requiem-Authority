using UnityEngine;

namespace Combat.Prefab
{
	public class ProjectileController: PrefabController
	{
		private Rigidbody2D _rb;

		internal float Speed { get; private set; }
		internal bool CanSurpassEnemies { get; set; }

		public void SetParameters(float damage, float lifetime, float speed, bool canSurpassEnemies)
		{
			base.SetParameters(damage, lifetime);
			Speed = speed;
			CanSurpassEnemies = canSurpassEnemies;
		}

		private void Start()
		{
			audio = gameObject.AddComponent<AudioSource>();
			audio.clip = Resources.Load<AudioClip>("Sounds/ShootSound");
			audio.volume = 0.2f;
			_rb = GetComponent<Rigidbody2D>();
			_rb.velocity = transform.right * Speed;
			audio.Play();
			Destroy(gameObject, Lifetime);
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.CompareTag("Ground")) Destroy(gameObject);
		}
	}
}