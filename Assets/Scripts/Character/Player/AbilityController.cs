using System;
using System.Collections;
using UnityEngine;

namespace Character.Player
{
	[Serializable]
	public class AbilityController
	{
		public float cooldown;
		public KeyCode key;
		public bool InCooldown { get; private set; }

		public IEnumerator Cooldown()
		{
			InCooldown = true;

			yield return new WaitForSeconds(cooldown);

			InCooldown = false;
		}
	}
}