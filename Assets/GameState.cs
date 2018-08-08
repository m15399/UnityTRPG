using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameState {

	public virtual void Enter(){
		
	}

	public virtual void Exit(){
		
	}

	public virtual void Update(){
		
	}

	public virtual void DoClickOnUnit(Unit unit){
		DefaultState();
	}

	public virtual void DoClickOnNothing(){
		DefaultState();
	}

	public virtual void DoClickMoveSpace(ClickableSpace space){
		WrongState("DoClickMoveSpace");
	}

	public virtual void DoClickAttackSpace(ClickableSpace space){
		WrongState("DoClickAttackSpace");
	}

	public virtual void DoClickAbilitySpace(ClickableSpace space){
		WrongState("DoClickAbilitySpace");
	}

	public virtual void DoCastAbility(Ability ability){
		ability.DoCast();
		DefaultState();
	}


	protected void DefaultState(){
		Game.Instance().TransitionGameState(new GameStateDefault());
	}

	protected void WrongState(string s){
		Debug.LogError("State [" + this.ToString() + "] does not know how to handle [" + s + "]");
		DefaultState();
	}
}
