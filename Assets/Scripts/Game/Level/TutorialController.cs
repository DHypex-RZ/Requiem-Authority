using System.Linq;
using Character.Enemy;
using UnityEngine;
using UnityEngine.UI;
using static Health.State;

namespace Game.Level
{
	public class TutorialController: LevelController
	{
		[SerializeField] EnemyManager[] enemies;
		Text _text;

		void Awake() { _text = GameObject.FindWithTag("Finish").GetComponent<Text>(); }

		void Update()
		{
			bool win = enemies.Aggregate(true, (current, enemy) => current & (enemy.HealthController.State == Dead));

			if (!win) return;

			_text.text = "!HAS TERMINADO EL TUTORIAL¡";
			StartCoroutine(Menu());
		}

		public override void LoseCondition()
		{
			_text.text = "TEN MAS CUIDADO, USA TODAS TUS HABILIDADES";
			StartCoroutine(Menu());
		}
	}
}