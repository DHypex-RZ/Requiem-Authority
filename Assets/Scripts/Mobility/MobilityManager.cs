using UnityEngine;

namespace Mobility
{
	[RequireComponent(typeof(Rigidbody2D))]
	public abstract class MobilityManager: MonoBehaviour
	{
		[SerializeField] protected float force;
		public Rigidbody2D Rigidbody2D { get; private set; }

		public bool Enabled { get; set; } = true;
		protected virtual void Awake() { Rigidbody2D = GetComponent<Rigidbody2D>(); }
	}
}