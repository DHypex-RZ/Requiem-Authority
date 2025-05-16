using Combat;
using UnityEngine;
using static Character.Enemy.State;

namespace Character.Enemy
{
	[RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D))]
	public class SniperController: EnemyManager
	{
		[SerializeField] float platformLimitDistance;
		[SerializeField] RangeController rangeController;
		float _initialPosition;
		int _platformDirection = 1;

		protected override void Awake()
		{
			base.Awake();
			rangeController.Character = GetComponent<SniperController>();
			_initialPosition = transform.position.x;
		}

		protected override void Update()
		{
			base.Update();

			if (!Enabled) return;

			State = Behaviour();

			if (transform.position.x >= _initialPosition + platformLimitDistance) _platformDirection = -1;
			else if (transform.position.x <= _initialPosition - platformLimitDistance) _platformDirection = 1;

			if ((Direction > 0 && transform.position.x >= _initialPosition + platformLimitDistance) ||
				(Direction < 0 && transform.position.x <= _initialPosition - platformLimitDistance)) Direction = 0;

			animator.SetBool("move", Direction != 0 && State != InDanger);

			if (State == InPatrol) return;

			if (!Hit || Hit.collider.CompareTag("Ground")) return;

			rangeController.Multiplier = State == InDanger ? 1.5f : 1f;
			rangeController.Shoot(State == InDanger ? "PushBullet" : "Bullet", Rotation);
		}


		void FixedUpdate()
		{
			if (!Enabled) return;

			MovementController.Move(State switch { InPatrol => _platformDirection, InAlert => Direction, _ => 0 });
		}

		protected override State Behaviour()
		{
			return Mathf.Abs(Position.x) > detectionDistance
					   ? InPatrol
					   : Mathf.Abs(Position.x) <= platformLimitDistance * 2
						   ? InDanger
						   : InAlert;
		}
	}
}