using Combat;
using UnityEngine;
using static Enum.EnemyState;

namespace Enemy
{
	public class AssaultController: EnemyController
	{
		[SerializeField] private int chargerCapacity;
		[SerializeField] private float timeToReload;
		[SerializeField] private float intervalIntoShoots;


		private float _timer;
		private int _timesToShoot, _totalBulletsShooted, _bulletsCount;
		private bool _isLoaded = true;

		protected override void Start()
		{
			base.Start();
			rangeCombat = GetComponent<RangeCombatController>();
		}

		protected override void Update()
		{
			base.Update();

			_timer += Time.deltaTime;

			if (Mathf.Abs(targetPosition.x) > maxDetectionDistance) state = InGuard;
			else if (Mathf.Abs(targetPosition.x) < maxDetectionDistance / 2.5f) state = InDanger;
			else state = InAlert;

			_timesToShoot = state switch { InGuard => 1, InAlert => 3, InDanger => 5, _ => 0 };

			if (!_isLoaded)
			{
				if (_timer >= timeToReload) _isLoaded = true;

				return;
			}

			if (_totalBulletsShooted == chargerCapacity)
			{
				_isLoaded = false;
				_totalBulletsShooted = 0;

				return;
			}

			if (state == InGuard && target && target.collider.CompareTag("Enemy")) return;

			if (!target || target.collider.tag is not ("Player" or "Enemy" or "Shield")) return;

			if (_bulletsCount == _timesToShoot && _timer >= intervalIntoShoots)
			{
				_timer = _bulletsCount = 0;

				return;
			}

			if (_bulletsCount >= _timesToShoot) return;

			rangeCombat.Shoot(targetRotation);
			_bulletsCount++;
			_totalBulletsShooted++;
			_timer = 0;
		}

		private void FixedUpdate() { movement.Move(direction); }
	}
}