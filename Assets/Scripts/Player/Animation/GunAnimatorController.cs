using UnityEngine;
using static UnityEngine.Input;
using static UnityEngine.KeyCode;

namespace Player.Animation
{
	public class GunAnimatorController: MonoBehaviour
	{
		private Animator _animator;

		private static readonly int Shoot = Animator.StringToHash("shoot");

		private void Start() { _animator = GetComponent<Animator>(); }

		private void Update() { _animator.SetBool(Shoot, GetMouseButton(0) || GetKey(E)); }
	}
}