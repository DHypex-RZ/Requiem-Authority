using UnityEngine;
using static UnityEngine.Input;

namespace Character.Player
{
	public class GunAnimator: MonoBehaviour
	{
		Animator _animator;
		void Awake() { _animator = GetComponent<Animator>(); }
		void FixedUpdate() { _animator.SetBool("shoot", GetMouseButton(0)); }
	}
}