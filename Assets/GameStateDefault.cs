using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateDefault : GameState {

	public override void Enter(){
		Game game = Game.Instance();
		game.SetSelectedUnit(null);
	}

	public override void DoClickOnUnit(Unit unit){
		Game game = Game.Instance();
		game.SetSelectedUnit(unit);
		game.TransitionGameState(new GameStateMove());
	}

	public override void DoClickOnNothing(){
		Game game = Game.Instance();
		game.SetSelectedUnit(null);
	}

}
