using Combat;
using Mobility;
using UnityEngine;
using static Player.InputController;

namespace Player
{
	public class PlayerController: MonoBehaviour
	{
		private AbilityController _ability;
		private MovementController _movement;
		private DashController _dash;
		private PhysicCombatController _physicCombat;
		private RangeCombatController _rangeCombat;

		private void Start()
		{
			_ability = GetComponent<AbilityController>();
			_movement = GetComponent<MovementController>();
			_dash = GetComponent<DashController>();
			_physicCombat = GetComponent<PhysicCombatController>();
			_rangeCombat = GetComponent<RangeCombatController>();
		}

		private void Update()
		{
			if (IsClickedLeft) _rangeCombat.Shoot();
			else if (IsClickedRight) _physicCombat.RealizeAttack();

			if (IsPressedQ) StartCoroutine(_ability.Heal());
			if (IsPressedW) StartCoroutine(_ability.Shield());
			if (IsPressedE) StartCoroutine(_ability.Sniper());
		}

		private void FixedUpdate()
		{
			if (_movement.IsGrounded) _movement.CanRun = IsPressedShift;
			_movement.CanRun &= IsPressedShift;
			if (!_dash.IsDashing) _movement.Move(HorizontalInput, _movement.CanRun);
			if (IsPressedSpace) _movement.Jump();
			if (IsPressedCtrl) _dash.RealizeDash();
		}
	}
}