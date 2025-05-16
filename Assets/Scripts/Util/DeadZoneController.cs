using Character;
using UnityEngine;
using static Health.State;

namespace Util
{
	public class DeadZoneController: MonoBehaviour
	{
		void OnTriggerEnter2D(Collider2D other)
		{
			if (other.TryGetComponent(out CharacterManager character)) character.HealthController.State = Dead;
		}
	}
}