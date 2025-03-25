using Mobility;
using UnityEngine;
using static UnityEngine.Input;

namespace Game
{
	public class JumpPointController: MonoBehaviour
	{
		[SerializeField] private float jumpForce;

		private MovementController _player;
		private Rigidbody2D _playerBody;

		private void Start()
		{
			GameObject player = GameObject.FindGameObjectWithTag("Player");

			_player = player.GetComponent<MovementController>();
			_playerBody = player.GetComponent<Rigidbody2D>();
		}

		private void FixedUpdate()
		{
			if (_player.IsGrounded && GetMouseButton(0)) _playerBody.AddForce(_playerBody.transform.up * jumpForce, ForceMode2D.Impulse);
		}
	}
}