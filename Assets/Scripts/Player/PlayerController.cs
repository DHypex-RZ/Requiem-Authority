using Combat;
using Health;
using Mobility;
using UnityEngine;

namespace Player //todo: Hacer AbilityController
{
	public class PlayerController: MonoBehaviour
	{
		private InputController _input;
		private HealthController _health;
		private MovementController _movement;
		private DashController _dash;
		private PhysicCombatController _physicCombat;
		private RangeCombatController _rangeCombat;

		private void Start()
		{
			_input = GetComponent<InputController>();
			_health = GetComponent<HealthController>();
			_movement = GetComponent<MovementController>();
			_dash = GetComponent<DashController>();
			_physicCombat = GetComponent<PhysicCombatController>();
			_rangeCombat = GetComponent<RangeCombatController>();
		}

		private void Update()
		{
			if (_input.IsClickedLeft) _rangeCombat.Shoot();
			else if (_input.IsClickedRight) _physicCombat.RealizeAttack();
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