using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : MonoBehaviour {

	public class AbilityInfo {
		public string name = "(ability)";
		public string description = "(description)";
		public bool isAttack = false;                  // Just for target tile colors
		public bool isTargetted = false;
		public int range = 1;
		public int damage = 0;
	}

	AbilityInfo abilityInfo;

	public Ability(){
		abilityInfo = new AbilityInfo();
		InitAbilityInfo(abilityInfo);
	}

	public AbilityInfo GetInfo(){
		return abilityInfo;
	}

	protected abstract void InitAbilityInfo(AbilityInfo info);

	////////////////////////////////////////////////////////////////////////////

	// Click cast in the UI
	//
	public virtual void DoClickCastButton(){
		Debug.Log("Casting " + abilityInfo.name + "!");
	}

	// Click target tile
	//
	public void DoClickTarget(Coord coord){
		Debug.Log("Casting " + abilityInfo.name + " at " + coord.ToString() + "!");
	}
}
