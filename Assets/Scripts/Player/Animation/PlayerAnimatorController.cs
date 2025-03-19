using UnityEngine;

namespace Player.Animation
{
	public class PlayerAnimatorController: MonoBehaviour
	{
		private static readonly int Move = Animator.StringToHash("move");
		private static readonly int Run = Animator.StringToHash("run");

		private Animator _animator;
		private InputController _input;

		private void Start()
		{
			_animator = GetComponent<Animator>();
			_input = GetComponent<InputController>();
		}

		private void Update()
		{
			_animator.SetBool(Move, _input.HorizontalInput != 0);
			_animator.SetFloat(Run, _input.HorizontalInput != 0 && _input.IsPressedShift ? 1.45f : 1f);
		}
	}
}