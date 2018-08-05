using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {

	GameInput gameInput;

	GameObject selectedHighlight;
	GameObject mousePointer;
	GameObject unitDetailsPane;

	Unit selectedUnit = null;

	public static Game Instance(){
		// TODO 
		return GameObject.Find("Game").GetComponent<Game>();
	}

	void Start () {
		gameInput = new GameInput(this);

		mousePointer = GameObject.Find("MousePointer");
		selectedHighlight = GameObject.Find("SelectedHighlight");

		//

		selectedHighlight.SetActive(false);
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
	}

	public void ClickedOnNothing(){
		selectedUnit = null;
		UnitDetails.Instance().Hide();
	}

	public void DoClickOnUnit(Unit unit){
		selectedUnit = unit;
		UnitDetails.Instance().ShowUnitDetails(unit);
	}

	public void DoCastAbility(Ability ability){
		ability.DoCast();
	}
}
