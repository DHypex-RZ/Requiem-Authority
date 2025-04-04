using Character;
using Prefab;
using UnityEngine;

namespace Combat
{
	[RequireComponent(typeof(CharacterManager))]
	public class ProtectionController: MonoBehaviour
	{
		CharacterManager _character;
		[SerializeField] float duration;
		ShieldController _controller;

		void Awake() { _character = GetComponent<CharacterManager>(); }

		public void Protect(string typeShield)
		{
			GameObject shield = Instantiate(Resources.Load<GameObject>("Prefabs/Combat/" + typeShield));
			if (shield.TryGetComponent(out _controller)) _controller.SetParameters(_character, duration);
		}
	}
}