using UnityEngine;

namespace Mobility
{
	public class JumpController: MobilityManager
	{
		static readonly float Distance = 1.5f;
		internal bool IsGrounded { get; private set; }

		void Update()
		{
			#if UNITY_EDITOR
			Debug.DrawRay(transform.position, Vector2.down * Distance, Color.yellow);
			#endif
			IsGrounded = Physics2D.Raycast(transform.position, Vector2.down, Distance, LayerMask.GetMask("Ground"));
		}

		public void Jump()
		{
			if (IsGrounded && Enabled) Rigidbody2D.AddForceY(transform.up.y * force, ForceMode2D.Impulse);
		}
	}
}