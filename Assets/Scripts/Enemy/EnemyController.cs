using Combat;
using Enum;
using Health;
using Mobility;
using UnityEngine;
using static Enum.HealthState;


namespace Enemy
{
	public abstract class EnemyController: MonoBehaviour
	{
		[SerializeField] protected float maxDetectionDistance;

		protected EnemyState state;
		protected MovementController movement;
		protected PhysicCombatController physicCombat;
		protected RangeCombatController rangeCombat;
		protected RaycastHit2D target;
		protected Vector3 targetPosition;
		protected Quaternion targetRotation;
		protected float direction;

		private HealthController _health;
		private Transform _target;

		protected virtual void Start()
		{
			_health = GetComponent<HealthController>();
			movement = GetComponent<MovementController>();

			_target = GameObject.FindGameObjectWithTag("Player").transform;
		}

		protected virtual void Update()
		{
			if (_health.State == Dead)
			{
				gameObject.layer = LayerMask.NameToLayer("Background");
				gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
				gameObject.GetComponent<EnemyController>().enabled = false;
			}

			targetPosition = _target.position - transform.position;
			targetRotation = Quaternion.Euler(0, 0, Mathf.Atan2(targetPosition.y, targetPosition.x) * Mathf.Rad2Deg);
			direction = targetPosition.x switch { > 1 => 1, < -1 => -1, _ => 0 };
			Debug.DrawRay(transform.position, targetPosition, Color.red);
			target = Physics2D.Raycast(transform.position, targetPosition, maxDetectionDistance);
		}
	}
}