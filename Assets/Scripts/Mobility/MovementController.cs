using UnityEngine;

namespace Mobility
{
	public class MovementController: MonoBehaviour
	{
		[SerializeField] private float speed;
		[SerializeField] private float acceleration;
		[SerializeField] private float jumpForce;

		private Rigidbody2D _rb;

		public bool IsGrounded { get; private set; }
		public bool CanRun { get; set; }

		private void Start() { _rb = GetComponent<Rigidbody2D>(); }

		private void Update()
		{
			Debug.DrawRay(_rb.position, Vector2.down * 1.15f, Color.white);
			IsGrounded = Physics2D.Raycast(_rb.position, Vector2.down, 1.15f);
		}

		public void Move(float direction, bool isRunning = false)
		{
			_rb.transform.rotation = direction switch
			{
				> 0 => Quaternion.Euler(0, 0, 0), < 0 => Quaternion.Euler(0, 180, 0), _ => _rb.transform.rotation
			};

			float velocity = isRunning && CanRun ? acceleration : speed;
			_rb.velocity = new Vector2(direction * velocity, _rb.velocity.y);
		}

		public void Jump()
		{
			if (IsGrounded) _rb.AddForce(_rb.transform.up * jumpForce, ForceMode2D.Impulse);
		}
	}
}