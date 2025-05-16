using UnityEngine;

namespace Character.Enemy.Animation
{
	[RequireComponent(typeof(Animator))]
	public class StickAnimator: MonoBehaviour
	{
		TankController _controller;
		Animator _animator;

		void Awake()
		{
			_animator = GetComponent<Animator>();
			_controller = transform.parent.GetComponent<TankController>();
		}

		void Update() { _animator.SetBool("attack", !_controller.PhysicalController.Enabled); }
	}
}