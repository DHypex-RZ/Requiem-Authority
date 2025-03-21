using UnityEngine;
using static Player.InputController;

namespace Player.Animation
{
	public class PlayerAnimatorController: MonoBehaviour
	{
		private static readonly int Move = Animator.StringToHash("move");
		private static readonly int Run = Animator.StringToHash("run");

		private Animator _animator;

		private void Start() { _animator = GetComponent<Animator>(); }

		private void Update()
		{
			_animator.SetBool(Move, HorizontalInput != 0);
			_animator.SetFloat(Run, HorizontalInput != 0 && IsPressedShift ? 1.45f : 1f);
		}
	}
}