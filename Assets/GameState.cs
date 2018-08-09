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

	public virtual void DoClickTargetSpace(ClickableSpace space){
		WrongState("DoClickAbilitySpace");
	}

	public virtual void DoCastAbility(Ability ability){
		Game.Instance().DoClickCastButton(ability);
	}


	protected void DefaultState(){
		Game.Instance().TransitionGameState(new GameStateDefault());
	}

	protected void WrongState(string s){
		Debug.LogError("State [" + this.ToString() + "] does not know how to handle [" + s + "]");
		DefaultState();
	}
}
