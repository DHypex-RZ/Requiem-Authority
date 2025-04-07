using System;
using System.Collections;
using Combat;
using Movement;
using Prefab.Shield;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Health.State;
using static UnityEngine.Input;
using static UnityEngine.KeyCode;

namespace Character.Player
{
	[RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D), typeof(Animator))]
	public class PlayerController: CharacterManager
	{
		[SerializeField] DashController dashController;
		[SerializeField] PhysicalController physicalController;
		[SerializeField] RangeController rangeController;

		Animator _animator;

		[Header("Ability parameters")]
		[Header("First ability")] [SerializeField]
		float healAmount;

		[Header("First ability")] [SerializeField]
		float attackMultiplier;

		[Header("Third ability")] [SerializeField]
		float shieldHealth;

		[Tooltip("Assigned for third ability")] [SerializeField]
		float shieldDuration;

		[Header("Fourth ability")] [SerializeField]
		float bulletMultiplier;

		[SerializeField] Ability[] abilities;

		public PhysicalController PhysicalController => physicalController;
		public Ability[] Abilities => abilities;

		protected override void Awake()
		{
			base.Awake();
			PlayerController controller = GetComponent<PlayerController>();

			dashController.MovementController = MovementController;
			physicalController.Character = rangeController.Character = controller;
			_animator = GetComponent<Animator>();

			abilities[0].Action = () => HealthController.Heal(healAmount);

			abilities[1].Action = () =>
			{
				PhysicalController.Multiplier = 1.35f;
				PhysicalController.Enabled = true;
				PhysicalController.Attack("PushUpAttack", () => StartCoroutine(physicalController.Cooldown()));
			};

			abilities[2].Action = () =>
			{
				GameObject prefab = Instantiate(Resources.Load<GameObject>(CombatManager.PREFAB_ROUTE + "Shield"));
				prefab.TryGetComponent(out ShieldController shield);
				shield.SetParameters(controller, shieldHealth, shieldDuration);
			};

			abilities[3].Action = () =>
			{
				rangeController.Multiplier = .5f;
				rangeController.Enabled = true;
				rangeController.Shoot("ParalyzeBullet", () => StartCoroutine(rangeController.Cooldown()));
			};
		}

		protected override void Update()
		{
			base.Update();

			if (HealthController.State == Dead) StartCoroutine(GoMenu());

			if (!Enabled) return;

			MovementController.Move(GetAxisRaw("Horizontal"), GetKey(LeftShift));
			if (GetKeyDown(KeyCode.Space)) MovementController.Jump();
			if (GetKeyDown(LeftControl)) dashController.Dash(() => StartCoroutine(dashController.Cooldown()));

			if (GetMouseButton(0))
			{
				rangeController.Multiplier = 1f;
				rangeController.Shoot("Bullet", () => StartCoroutine(rangeController.Cooldown()));
			}
			else if (GetMouseButton(1))
			{
				physicalController.Multiplier = 1f;
				physicalController.Attack("Attack", () => StartCoroutine(physicalController.Cooldown()));
			}

			foreach (Ability ability in abilities)
				if (GetKeyDown(ability.assignedKey))
					ability.RealizeAbility(() => StartCoroutine(ability.Cooldown()));

			_animator.SetBool("move", GetAxisRaw("Horizontal") != 0 && MovementController.IsGrounded);
			_animator.SetFloat("running", GetKey(LeftShift) ? 1.5f : 1);
		}

		static IEnumerator GoMenu()
		{
			yield return new WaitForSeconds(0.15f);

			SceneManager.LoadScene("Menu");
		}
	}

	[Serializable] public class Ability
	{
		public float cooldown;
		public KeyCode assignedKey;
		public Action Action { get; set; }
		public bool Enabled { get; set; } = true;

		public void RealizeAbility(Action coroutine)
		{
			if (!Enabled) return;

			coroutine.Invoke();
			Action.Invoke();
		}


		public IEnumerator Cooldown()
		{
			Enabled = false;

			yield return new WaitForSeconds(cooldown);

			Enabled = true;
		}
	}
}