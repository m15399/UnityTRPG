using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : Ability {

	protected override void InitAbilityInfo (AbilityInfo info){
		info.name = "Basic Attack";
		info.description = "Get 'em.";
		info.isAttack = true;
		info.isTargetted = true;
		info.range = 1;
		info.damage = 1;
	}

	public override bool Cast (Coord targetCoord, Unit targetUnit)
	{
		return CommonAttack(targetUnit);
	}
}
