using System.Collections;
using Combat;
using Mobility;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Health.HealthState;
using static UnityEngine.Input;
using static UnityEngine.KeyCode;

namespace Character.Player
{
	public class PlayerController: CharacterManager
	{
		[SerializeField] float healingAmount;
		[SerializeField] float abilityQCooldown;
		[SerializeField] AbilityController[] abilities;

		DashController DashController { get; set; }
		PhysicalController PhysicalController { get; set; }
		RangeController RangeController { get; set; }
		ProtectionController ProtectionController { get; set; }
		public AbilityController[] Abilities => abilities;

		protected override void Awake()
		{
			base.Awake();
			DashController = GetComponent<DashController>();
			PhysicalController = GetComponent<PhysicalController>();
			RangeController = GetComponent<RangeController>();
			ProtectionController = GetComponent<ProtectionController>();
		}

		void Start() { base.Update(); }

		protected override void Update()
		{
			base.Update();

			if (HealthController.State == Dead || GetKeyDown(Escape)) StartCoroutine(ReturnToMenu());

			if (!Enabled) return;

			Animator.SetBool("move", GetAxisRaw("Horizontal") != 0 && JumpController.IsGrounded);
			Animator.SetFloat("running", GetKey(LeftShift) ? 1.65f : 1);

			if (GetMouseButton(0))
			{
				RangeController.Multiplier = 1f;
				RangeController.Shoot("Bullet");
			}
			else if (GetMouseButton(1))
			{
				PhysicalController.Multiplier = 1f;
				PhysicalController.Attack("Attack");
			}

			foreach (AbilityController ability in abilities)
				if (GetKey(ability.key) && !ability.InCooldown)
					switch (ability.key)
					{
						case Q:
							StartCoroutine(ability.Cooldown());
							HealthController.Heal(healingAmount);

							break;
						case W:
							StartCoroutine(ability.Cooldown());
							PhysicalController.Multiplier = 1.35f;
							PhysicalController.CheckCooldown = false;
							PhysicalController.Attack("SpecialAttack");

							break;
						case E:
							StartCoroutine(ability.Cooldown());
							ProtectionController.Protect("Shield");

							break;
						case R:
							StartCoroutine(ability.Cooldown());
							RangeController.Multiplier = .5f;
							RangeController.CheckCooldown = false;
							RangeController.Shoot("SleepBullet");
							RangeController.CheckCooldown = true;

							break;
					}

			MoveController.Move(GetAxisRaw("Horizontal"), GetKey(LeftShift));
			if (GetKeyDown(KeyCode.Space)) JumpController.Jump();
			if (GetKey(LeftControl)) DashController.Dash();
		}

		IEnumerator ReturnToMenu()
		{
			yield return new WaitForSeconds(1);

			SceneManager.LoadScene("Menu");
		}
	}
}