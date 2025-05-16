using Health;
using Movement;
using UnityEngine;
using static Health.State;

namespace Character
{
	public abstract class CharacterManager: MonoBehaviour
	{
		public bool Enabled { get; set; } = true;

		[SerializeField] HealthController healthController;
		[SerializeField] MovementController movementController;

		public HealthController HealthController => healthController;
		public MovementController MovementController => movementController;

		protected virtual void Awake()
		{
			MovementController.Rigidbody = GetComponent<Rigidbody2D>();
			HealthController.CurrentHealth = HealthController.Health;
		}

		protected virtual void Update()
		{
			if (HealthController.State == Dead)
			{
				movementController.Rigidbody.linearVelocityX = Vector2.zero.x;
				movementController.Enabled = false;

				gameObject.layer = LayerMask.NameToLayer("DeadZone");
				Enabled = false;

				return;
			}

			movementController.Grounded = Physics2D.Raycast(transform.position, Vector2.down, movementController.GroundCheckDistance, LayerMask.GetMask("Ground"));
			#if UNITY_EDITOR
			Debug.DrawRay(transform.position, Vector2.down * movementController.GroundCheckDistance, Color.yellow);
			#endif
		}
	}
}