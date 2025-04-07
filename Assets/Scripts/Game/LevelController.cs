using System.Linq;
using Character.Enemy;
using UnityEngine;
using static Health.State;

namespace Game
{
	public class LevelController: MonoBehaviour
	{
		[SerializeField] EnemyManager[] enemies;

		void Update()
		{
			bool win = enemies.Aggregate(true, (current, enemy) => current & (enemy.HealthController.State == Dead));

			if (win) Debug.Log("!!HAS GANADO EL NIVEL¡¡");
		}
	}
}