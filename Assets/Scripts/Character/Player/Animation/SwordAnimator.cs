using UnityEngine;

namespace Character.Player.Animation
{
	[RequireComponent(typeof(Animator))]
	public class SwordAnimator: MonoBehaviour
	{
		PlayerController _controller;
		Animator _animator;

		void Awake()
		{
			_animator = GetComponent<Animator>();
			_controller = transform.parent.GetComponent<PlayerController>();
		}

		void Update() { _animator.SetBool("attack", !_controller.PhysicalController.Enabled); }
	}
}