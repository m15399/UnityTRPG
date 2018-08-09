using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateMove : GameState {

	public override void Enter(){
		Game game = Game.Instance();
		Unit unit = game.GetSelectedUnit();

		int x = unit.pos.x;
		int y = unit.pos.y;
		int speed = unit.currentStats.speed;

		game.tileLayer.LayTiles(x, y, speed, ClickableSpace.Type.Move);
	}

	public override void Exit(){
		Game.Instance().tileLayer.ClearTiles();
	}

	public override void DoClickTargetSpace(ClickableSpace space){
		Game game = Game.Instance();
		Unit unit = game.GetSelectedUnit();

		unit.DoMoveTo(space.pos);

		DefaultState();
	}

}
