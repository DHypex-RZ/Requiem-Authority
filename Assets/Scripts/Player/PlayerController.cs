using Combat;
using Mobility;
using UnityEngine;

namespace Player
{
	public class PlayerController: MonoBehaviour
	{
		private InputController _input;
		private AbilityController _ability;
		private MovementController _movement;
		private DashController _dash;
		private PhysicCombatController _physicCombat;
		private RangeCombatController _rangeCombat;

		private void Start()
		{
			_input = GetComponent<InputController>();
			_ability = GetComponent<AbilityController>();
			_movement = GetComponent<MovementController>();
			_dash = GetComponent<DashController>();
			_physicCombat = GetComponent<PhysicCombatController>();
			_rangeCombat = GetComponent<RangeCombatController>();
		}

		private void Update()
		{
			if (_input.IsClickedLeft) _rangeCombat.Shoot();
			else if (_input.IsClickedRight) _physicCombat.RealizeAttack();

			if (_input.IsPressedQ) StartCoroutine(_ability.Heal());
			if (_input.IsPressedW) StartCoroutine(_ability.Shield());
			if (_input.IsPressedE) StartCoroutine(_ability.Sniper());
		}

		private void FixedUpdate()
		{
			if (_movement.IsGrounded) _movement.CanRun = _input.IsPressedShift;
			_movement.CanRun &= _input.IsPressedShift;
			if (!_dash.IsDashing) _movement.Move(_input.HorizontalInput, _movement.CanRun);
			if (_input.IsPressedSpace) _movement.Jump();
			if (_input.IsPressedCtrl) _dash.RealizeDash();
		}
	}
}