using System;
using System.Collections.Generic;
using Character.Player;
using UnityEngine;

namespace UI.Ability
{
	public class CooldownUI: MonoBehaviour
	{
		Character.Player.Ability[] _abilities;
		[SerializeField] float initialPosition;
		List<GameObject> _viewers;

		void Awake()
		{
			_abilities = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().Abilities;
			#if UNITY_EDITOR
			Array.Reverse(_abilities);
			#endif
			float position = initialPosition;

			foreach (var ability in _abilities)
			{
				GameObject obj = Instantiate(Resources.Load<GameObject>("Prefabs/Util/AbilityViewer"), transform, false);
				obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(position, 0);
				ViewerController viewer = obj.GetComponent<ViewerController>();
				viewer.Ability = ability;
				viewer.Start();
				position += 150;
			}
		}
	}
}