using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : MonoBehaviour {

	public class AbilityInfo {
		public string name = "(ability)";
		public string description = "(description)";
		public int damage = 1;
		public bool isAttack = false;                  // Just for target tile colors
		public bool isTargetted = false;
		public int range = 1;
		public int radius = 0;
		public bool damageSelf = false;
		public bool targetsGround = false;
		public bool targetsAllies = false;
	}

	protected AbilityInfo abilityInfo;
	protected Unit owner;

	public Ability(){
		abilityInfo = new AbilityInfo();
		InitAbilityInfo(abilityInfo);
	}

	public AbilityInfo GetInfo(){
		return abilityInfo;
	}

	protected abstract void InitAbilityInfo(AbilityInfo info);
	public abstract bool Cast(Coord targetCoord, Unit targetUnit);

	////////////////////////////////////////////////////////////////////////////

	void Start(){
		owner = GetComponent<Unit>();
	}

	// Click cast in the UI
	//
	public bool DoClickCastButton(){
//		Debug.Log("Casting " + abilityInfo.name + "!");

		bool casted = false;

		if (!abilityInfo.isTargetted){
			Cast(null, null);
			casted = true;
		}

		return casted;
	}

	// Click target tile
	//
	public bool DoClickTarget(Coord coord){
//		Debug.Log("Casting " + abilityInfo.name + " at " + coord.ToString() + "!");

		Utils.Assert(coord.Distance(owner.pos) <= abilityInfo.range, "Target was not in range of ability!");

		bool casted = false;
		Unit unit = Utils.FindUnit(coord);

		// Do some basic checks to save work in child classes.
		//
		bool canCast = false;

		if (abilityInfo.targetsGround){
			canCast = true;
		} else {
			// Requires target unit
			//
			if (unit == null){
				canCast = false;
			} else {
				canCast = true;
			}
		}

		if (canCast){
			casted = Cast(coord, unit);
		}

		return casted;
	}

	////////////////////////////////////////////////////////////////////////////
	// Common ability mechanics
	//

	protected bool CommonAttack(Unit target){
		int damage = abilityInfo.damage;

		Utils.Assert(target != null, "No target for CommonAttack");

		target.TakeDamage(damage);

		return true;
	}

	protected bool CommonDamageArea(Coord center){
		int damage = abilityInfo.damage;
		int radius = abilityInfo.radius;
		bool damageSelf = abilityInfo.damageSelf;

		Utils.Assert(radius > 0, "Must specify a radius > 0");

		// TODO don't damage allies
		List<Unit> units = Utils.FindUnitsInArea(center, radius);
		foreach (Unit unit in units){
			bool shouldDamage = true;

			if (!damageSelf && unit == owner){
				shouldDamage = false;
			}

			if (shouldDamage){
				unit.TakeDamage(damage);
			}
		}

		return true;
	}

	protected bool CommonDamageArea(){
		return CommonDamageArea(owner.pos);
	}

}
