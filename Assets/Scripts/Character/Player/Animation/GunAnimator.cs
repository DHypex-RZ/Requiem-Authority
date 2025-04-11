using UnityEngine;
using static UnityEngine.Input;
using static UnityEngine.KeyCode;

namespace Character.Player.Animation
{
	[RequireComponent(typeof(Animator))]
	public class GunAnimator: MonoBehaviour
	{
		Animator _animator;
		void Awake() { _animator = GetComponent<Animator>(); }
		void Update() { _animator.SetBool("shoot", GetMouseButton(0) || GetKeyDown(R)); }
	}
}