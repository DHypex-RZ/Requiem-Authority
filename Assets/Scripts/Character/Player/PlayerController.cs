using System;
using System.Collections;
using Combat;
using Game.Level;
using Movement;
using Prefab.Shield;
using UnityEngine;
using static Health.State;
using static UnityEngine.Input;
using static UnityEngine.KeyCode;

namespace Character.Player
{
	[RequireComponent(typeof(CapsuleCollider2D), typeof(Rigidbody2D), typeof(Animator))]
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
		LevelController _levelController;

		public PhysicalController PhysicalController => physicalController;
		public Ability[] Abilities => abilities;

		protected override void Awake()
		{
			_levelController = GameObject.FindWithTag("GameController").GetComponent<LevelController>();
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
				PhysicalController.Attack("PushUpAttack");
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
				rangeController.Shoot("ParalyzeBullet");
			}; 
		}

		protected override void Update()
		{
			base.Update();

			if (HealthController.State == Dead)
			{
				_animator.SetBool("dead", true);
				_levelController.LoseCondition();
			}

			if (!Enabled) return;

			MovementController.Move(GetAxisRaw("Horizontal"), GetKey(LeftShift));

			if (GetKeyDown(KeyCode.Space))
			{
				MovementController.Jump();
				_animator.SetBool("jump", true);
				StartCoroutine(nameof(StopAnimationJump));
			}

			if (GetKeyDown(LeftControl)) dashController.Dash();

			if (GetMouseButton(0))
			{
				rangeController.Multiplier = 1f;
				rangeController.Shoot("Bullet");
			}
			else if (GetMouseButton(1))
			{
				physicalController.Multiplier = 1f;
				physicalController.Attack("Attack");
			}

			foreach (Ability ability in abilities)
				if (GetKeyDown(ability.assignedKey))
					_ = ability.RealizeAbility();

			_animator.SetBool("move", GetAxisRaw("Horizontal") != 0 && MovementController.Grounded);
			_animator.SetFloat("running", GetKey(LeftShift) ? 1.5f : 1);
		}

		IEnumerator StopAnimationJump()
		{
			yield return new WaitForSeconds(0.1f);
			yield return new WaitUntil(() => MovementController.Grounded);

			_animator.SetBool("jump", false);
		}
	}

	[Serializable] public class Ability
	{
		public float cooldown;
		public KeyCode assignedKey;
		public Sprite icon;
		public Action Action { get; set; }
		public bool Enabled { get; set; } = true;

		public async Awaitable RealizeAbility()
		{
			if (!Enabled) return;

			Enabled = false;
			Action.Invoke();
			await Awaitable.WaitForSecondsAsync(cooldown);
			Enabled = true;
		}
	}
}