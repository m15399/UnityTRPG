using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {

	public TileLayer tileLayer;
	public Board board;
	public GameState gameState { get; set; }

	GameInput gameInput;

	GameObject selectedHighlight;
	GameObject mousePointer;
	GameObject unitDetailsPane;

	Unit selectedUnit = null;

	public static Game Instance(){
		// TODO singletons everywhere
		return GameObject.Find("Game").GetComponent<Game>();
	}

	public Game(){
		gameInput = new GameInput(this);
		tileLayer = new TileLayer();
		board = new Board();

		gameState = new GameStateDefault();
	}

	void Start () {

		mousePointer = GameObject.Find("MousePointer");
		selectedHighlight = GameObject.Find("SelectedHighlight");

		//

		selectedHighlight.SetActive(false);

		// Test code

	}
	
	void Update () {

		// Update input
		//
		gameInput.UpdateInput();

		// Highlight selected unit
		//
		if (selectedUnit != null){
			selectedHighlight.SetActive(true);
			selectedHighlight.transform.position = selectedUnit.transform.position + new Vector3(0, 0, 1);
		} else {
			selectedHighlight.SetActive(false);
		}

		// Move mouse pointer
		//
		if (mousePointer != null)
			mousePointer.transform.position = gameInput.mousePositionWorld;

		// Debugging
		//
		Utils.DebugText("Game state", gameState);

		board.CheckEntityPositions();
	}

	public void TransitionGameState(GameState newState){
		Utils.Assert(newState != null);

		gameState.Exit();
		gameState = newState;
		gameState.Enter();
	}

	public void SetSelectedUnit(Unit unit){
		selectedUnit = unit;
		if (unit != null){
			UnitDetails.Instance().ShowUnitDetails(unit);
		} else {
			UnitDetails.Instance().Hide();
		}
	}

	public Unit GetSelectedUnit(){
		return selectedUnit;
	}

	public void DoClickCastButton(Ability ability){

		bool casted = ability.DoClickCastButton();

		if (ability.GetInfo().isTargetted){
			TransitionGameState(new GameStateCast(ability));
		} else {
			TransitionGameState(new GameStateDefault());
		}
	}

}
