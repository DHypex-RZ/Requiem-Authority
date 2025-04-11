using UnityEngine;
using UnityEngine.UI;

namespace Character.Enemy
{
	public abstract class EnemyManager: CharacterManager
	{
		protected State State { get; set; }
		[Header("Enemy")] [SerializeField] protected float detectionDistance;
		Transform _target;
		protected Vector2 Position { get; private set; }
		protected Quaternion Rotation { get; private set; }
		protected float Direction { get; set; }
		protected RaycastHit2D Hit { get; private set; }
		Image _healthBar;

		protected override void Awake()
		{
			base.Awake();
			_target = GameObject.FindGameObjectWithTag("Player").transform;
			Canvas canvas = Instantiate(Resources.Load<Canvas>("Prefabs/Util/HealthBar"), transform, false);
			_healthBar = canvas.transform.Find("Health").GetComponent<Image>();
		}

		protected override void Update()
		{
			base.Update();

			_healthBar.fillAmount = HealthController.CurrentHealth / HealthController.Health;

			if (!Enabled) return;

			Position = _target.position - transform.position;
			Rotation = Quaternion.Euler(0, 0, Mathf.Atan2(Position.y, Position.x) * Mathf.Rad2Deg);
			Direction = Position.x switch { < -1 => -1, > 1 => 1, _ => 0 };
			Hit = Physics2D.Raycast(transform.position, Position, detectionDistance, LayerMask.GetMask("Level"));
			#if UNITY_EDITOR
			Debug.DrawRay(transform.position, Position, Color.red);
			#endif
		}

		protected abstract State Behaviour();
	}

	public enum State { InPatrol, InGuard, InAlert, InDanger }
}