using System.Linq;
using Character.Enemy;
using UnityEngine;
using UnityEngine.UI;
using static Health.State;

namespace Game.Level
{
	public class MainLevelController: LevelController
	{
		[SerializeField] EnemyManager[] enemies;
		Text _text;

		void Awake() { _text = GameObject.FindWithTag("Finish").GetComponent<Text>(); }

		void Update()
		{
			bool win = enemies.Aggregate(true, (current, enemy) => current & (enemy.HealthController.State == Dead));

			if (!win) return;

			_text.text = "!!HAS GANADO EL NIVEL¡¡";
			StartCoroutine(Menu());
		}

		public override void LoseCondition()
		{
			_text.text = "HAS MUERTO";
			StartCoroutine(Menu());
		}
	}
}