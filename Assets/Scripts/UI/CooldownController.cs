using Character.Player;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.KeyCode;

namespace UI
{
	public class CooldownController: MonoBehaviour
	{
		AbilityController[] _abilities;
		Image _q;
		float _qTimer;
		Image _w;
		float _wTimer;
		Image _e;
		float _eTimer;
		Image _r;
		float _rTimer;

		void Awake()
		{
			_abilities = GameObject.FindWithTag("Player").GetComponent<PlayerController>().Abilities;
			_q = transform.Find("Q").GetComponent<Image>();
			_w = transform.Find("W").GetComponent<Image>();
			_e = transform.Find("E").GetComponent<Image>();
			_r = transform.Find("R").GetComponent<Image>();

			foreach (AbilityController ability in _abilities)
				switch (ability.key)
				{
					case Q: _q.transform.Find("Text").GetComponent<Text>().text = ability.key.ToString(); break;
					case W: _w.transform.Find("Text").GetComponent<Text>().text = ability.key.ToString(); break;
					case E: _e.transform.Find("Text").GetComponent<Text>().text = ability.key.ToString(); break;
					case R: _r.transform.Find("Text").GetComponent<Text>().text = ability.key.ToString(); break;
				}
		}

		void Update()
		{
			foreach (AbilityController ability in _abilities)
				switch (ability.key)
				{
					case Q:
						_q.fillAmount = _qTimer / ability.cooldown;

						if (ability.InCooldown) _qTimer += Time.deltaTime;
						else _qTimer = 0;

						break;
					case W:
						_w.fillAmount = _wTimer / ability.cooldown;

						if (ability.InCooldown) _wTimer += Time.deltaTime;
						else _wTimer = 0;

						break;
					case E:
						_e.fillAmount = _eTimer / ability.cooldown;

						if (ability.InCooldown) _eTimer += Time.deltaTime;
						else _eTimer = 0;

						break;
					case R:
						_r.fillAmount = _rTimer / ability.cooldown;

						if (ability.InCooldown) _rTimer += Time.deltaTime;
						else _rTimer = 0;

						break;
				}
		}
	}
}