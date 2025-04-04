using Combat;
using UnityEngine;

namespace Character.Player
{
	public class SwordAnimator: MonoBehaviour
	{
		PhysicalController _controller;
		Animator _animator;

		void Awake()
		{
			_animator = GetComponent<Animator>();
			_controller = transform.parent.GetComponent<PhysicalController>();
		}

		void FixedUpdate() { _animator.SetBool("attack", _controller.CheckCooldown); }
	}
}