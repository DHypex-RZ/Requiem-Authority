using Combat;
using Mobility;
using UnityEngine;
using static UnityEngine.Input;
using static UnityEngine.KeyCode;

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
			if (GetMouseButton(0)) _rangeCombat.Shoot();
			else if (GetMouseButton(1)) _physicCombat.RealizeAttack();

			if (GetKey(Q)) StartCoroutine(_ability.Heal());
			if (GetKey(W)) StartCoroutine(_ability.Shield());
			if (GetKey(E)) StartCoroutine(_ability.Sniper());
		}

		private void FixedUpdate()
		{
			if (_movement.IsGrounded) _movement.CanRun = GetKey(LeftShift);
			_movement.CanRun &= GetKey(LeftShift);
			if (!_dash.IsDashing) _movement.Move(GetAxisRaw("Horizontal"), _movement.CanRun);
			if (GetKey(KeyCode.Space)) _movement.Jump();
			if (GetKey(LeftControl)) _dash.RealizeDash();
		}
	}
}