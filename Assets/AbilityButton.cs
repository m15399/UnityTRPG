using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityButton : MonoBehaviour {
	const float defaultAlpha = .2f;
	const float hoverAlpha = .3f;

	public Ability ability;

	SpriteRenderer sr;

	void Start () {
		sr = GetComponent<SpriteRenderer>();
		DoStopHover();
	}
	
	void Update () {
		
	}

	void DoClick(){
		Game.Instance().gameState.DoCastAbility(ability);
	}

	void DoStartHover(){
		Color c = sr.color;
		c.a = hoverAlpha;
		sr.color = c;
	}

	void DoStopHover(){
		Color c = sr.color;
		c.a = defaultAlpha;
		sr.color = c;
	}
}
