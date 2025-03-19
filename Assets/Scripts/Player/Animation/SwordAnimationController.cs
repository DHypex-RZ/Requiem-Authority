using Combat;
using UnityEngine;

namespace Player.Animation
{
	public class SwordAnimationController: MonoBehaviour
	{
		private Animator _animator;
		private PhysicCombatController _combat;

		private static readonly int Attack = Animator.StringToHash("attack");

		private void Start()
		{
			_animator = GetComponent<Animator>();
			_combat = transform.parent.GetComponent<PhysicCombatController>();
		}

		private void Update() { _animator.SetBool(Attack, _combat.IsAttacking); }
	}
}