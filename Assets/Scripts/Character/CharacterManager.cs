using Health;
using Mobility;
using UnityEngine;
using static Health.HealthState;

namespace Character
{
	[RequireComponent(typeof(HealthController), typeof(MoveController), typeof(JumpController))]
	public class CharacterManager: MonoBehaviour
	{
		public HealthController HealthController { get; private set; }
		public MoveController MoveController { get; private set; }
		public JumpController JumpController { get; private set; }
		protected Animator Animator { get; private set; }
		public bool Enabled { get; set; } = true;


		protected virtual void Awake()
		{
			HealthController = GetComponent<HealthController>();
			MoveController = GetComponent<MoveController>();
			JumpController = GetComponent<JumpController>();
			Animator = GetComponent<Animator>();
		}

		protected virtual void Update()
		{
			if (HealthController.State == Alive) return;

			gameObject.layer = LayerMask.NameToLayer("DeadZone");
			enabled = false;
		}
	}
}