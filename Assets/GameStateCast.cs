using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateCast : GameState {

	Ability ability;

	public GameStateCast(Ability ability){
		this.ability = ability;
	}

	public override void Enter(){
		Game game = Game.Instance();
		Unit unit = game.GetSelectedUnit();

		int x = unit.pos.x;
		int y = unit.pos.y;
		int range = ability.GetInfo().range;

		ClickableSpace.Type tileType = ability.GetInfo().isAttack ?
			ClickableSpace.Type.Attack :
			ClickableSpace.Type.Ability;

		game.tileLayer.LayTiles(x, y, range, tileType);
	}

	public override void Exit(){
		Game.Instance().tileLayer.ClearTiles();
	}

	public override void DoClickOnUnit(Unit unit){
		// Try to cast on tile unit is standing on
		//
		ClickableSpace space = Game.Instance().tileLayer.FindTile(unit.pos);
		if (space != null){
			DoClickTargetSpace(space);
		} else {
			// Clicked on unit out of range/untargettable
			//

			// Fall back to default behavior?
			//
//			base.DoClickOnUnit(unit);
		}
	}

	public override void DoClickTargetSpace(ClickableSpace space){
		bool casted = ability.DoClickTarget(space.pos);
		if (casted){
			DefaultState();
		}
	}
}
