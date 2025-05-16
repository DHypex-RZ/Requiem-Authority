using System.Collections.Generic;
using Character.Enemy;
using UnityEngine;

namespace Util
{
	[RequireComponent(typeof(BoxCollider2D))]
	public class ActivatorController: MonoBehaviour
	{
		[SerializeField] List<EnemyManager> enemies;

		void Awake()
		{
			foreach (EnemyManager enemy in enemies) enemy.Enabled = false;
		}

		void OnTriggerEnter2D(Collider2D other)
		{
			if (!other.CompareTag("Player")) return;

			foreach (EnemyManager enemy in enemies) enemy.Enabled = true;
		}
	}
}