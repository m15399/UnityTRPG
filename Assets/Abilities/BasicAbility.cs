using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAbility : Ability {

	protected override void InitAbilityInfo (AbilityInfo info){
		info.name = "Basic Ability";
		info.description = "Do a thing.";
		info.isAttack = false;
		info.isTargetted = false;
		info.range = 3;
		info.damage = 1;
		info.radius = 2;
	}

	public override bool Cast (Coord targetCoord, Unit targetUnit)
	{
		return CommonDamageArea();
	}
}
