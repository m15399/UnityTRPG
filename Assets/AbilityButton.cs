using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityButton : MonoBehaviour {
	public Ability ability;

	void Start () {

	}
	
	void Update () {
		
	}

	void DoClick(){
		Game.Instance().gameState.DoCastAbility(ability);
	}
}
