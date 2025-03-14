using Combat;
using Mobility;
using UnityEngine;

namespace Player.Animation
{
	public class PlayerAnimatorController: MonoBehaviour // todo: quedan animaciones por hacer
	{
		private static readonly int Walk = Animator.StringToHash("walk");
		private static readonly int Run = Animator.StringToHash("run");
		private static readonly int Jump = Animator.StringToHash("jump");
		private static readonly int Dash = Animator.StringToHash("dash");
		private static readonly int Attack = Animator.StringToHash("attack");
		private static readonly int Shoot = Animator.StringToHash("shoot");
		private static readonly int Barrier = Animator.StringToHash("barrier");
		private static readonly int Snipe = Animator.StringToHash("snipe");
		private static readonly int Heal = Animator.StringToHash("heal");

		private Animator _animator;
		private InputController _input;
		private MovementController _movement;
		private DashController _dash;
		private PhysicCombatController _physicCombat;

		private void Start()
		{
			_movement = GetComponent<MovementController>();
			_input = GetComponent<InputController>();
			_animator = GetComponent<Animator>();
			_dash = GetComponent<DashController>();
			_physicCombat = GetComponent<PhysicCombatController>();
		}

		private void Update()
		{
			_animator.SetBool(Walk, _input.HorizontalInput != 0);
			_animator.SetBool(Run, _input.IsPressedShift && _input.HorizontalInput != 0 && _movement.CanRun);
			_animator.SetBool(Jump, _input.IsPressedSpace && _movement.IsGrounded);
			_animator.SetBool(Dash, _dash.IsDashing); // todo: Por hacer

			_animator.SetBool(Attack, _physicCombat.IsAttacking);
			_animator.SetBool(Shoot, _input.IsClickedLeft); // todo: Por hacer

			/*_animator.SetBool(Heal, _abilities.IsHealing); // todo: Por hacer todas
			_animator.SetBool(Barrier, _abilities.IsUsingBarrier);
			_animator.SetBool(Snipe, _abilities.IsUsingSniper);*/
		}
	}
}