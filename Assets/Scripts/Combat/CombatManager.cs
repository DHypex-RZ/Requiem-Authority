using System.Collections;
using Character;
using UnityEngine;

namespace Combat
{
	public abstract class CombatManager
	{
		protected internal const string PREFAB_ROUTE = "Prefabs/Combat/";
		public CharacterManager Character { get; set; }


		[Header("Combat")]
		public float damage;

		public float cooldown;
		public float prefabDuration;

		public float Multiplier { get; set; } = 1;
		public bool Enabled { get; set; } = true;

		public IEnumerator Cooldown()
		{
			Enabled = false;

			yield return new WaitForSeconds(cooldown);

			Enabled = true;
		}
	}
}