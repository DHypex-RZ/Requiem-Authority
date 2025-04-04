using System.Collections;
using Character;
using UnityEngine;

namespace Combat
{
	[RequireComponent(typeof(CharacterManager))]
	public abstract class CombatManager: MonoBehaviour
	{
		protected const string PREFAB_ROUTE = "Prefabs/Combat/";
		[SerializeField] protected float damage;
		[SerializeField] float cooldown;
		[SerializeField] protected float duration;
		public float Multiplier { get; set; } = 1f;
		protected CharacterManager character;
		public bool CheckCooldown { get; set; }


		protected virtual void Awake() { character = GetComponent<CharacterManager>(); }


		private protected IEnumerator Cooldown()
		{
			CheckCooldown = true;

			yield return new WaitForSeconds(cooldown);

			CheckCooldown = false;
		}
	}
}