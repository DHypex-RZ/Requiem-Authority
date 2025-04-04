using UnityEngine;
using UnityEngine.UI;

namespace Character.Enemy
{
	public abstract class EnemyManager: CharacterManager
	{
		[SerializeField] protected float detectionDistance;
		protected EnemyState State { get; set; }
		Transform _target;
		protected Vector3 TargetPosition { get; private set; }
		protected Quaternion TargetRotation { get; private set; }
		protected float Direction { get; set; }
		protected RaycastHit2D Hit { get; private set; }
		Image _healthBar;

		protected override void Awake()
		{
			base.Awake();
			_target = GameObject.FindWithTag("Player").transform;
			_healthBar = transform.Find("HealthBar").Find("Health").GetComponent<Image>();
		}

		protected override void Update()
		{
			base.Update();

			TargetPosition = _target.position - transform.position;
			TargetRotation = Quaternion.Euler(0, 0, Mathf.Atan2(TargetPosition.y, TargetPosition.x) * Mathf.Rad2Deg);
			Direction = TargetPosition.x switch { < -1 => -1, > 1 => 1, _ => 0 };
			Hit = Physics2D.Raycast(transform.position, TargetPosition, detectionDistance);
			_healthBar.fillAmount = HealthController.CurrentHealth / HealthController.Health;
			#if UNITY_EDITOR
			Debug.DrawRay(transform.position, TargetPosition, Color.red);
			#endif
		}

		protected abstract EnemyState Behaviour();
	}

	public enum EnemyState { InPatrol, InGuard, InAlert, InDanger }
}