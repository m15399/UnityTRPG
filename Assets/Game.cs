using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {

	Vector2 mousePosition;
	GameObject mousePointer;
	GameObject mouseTarget = null;
	GameObject prevMouseTarget = null;

	bool prevHoveringObject = false;

	GameObject selectedHighlight;
	Unit selectedUnit = null;

	GameObject unitDetailsPane;

	public static Game Instance(){
		// TODO 
		return GameObject.Find("Game").GetComponent<Game>();
	}

	void Start () {
		mousePointer = GameObject.Find("MousePointer");
		selectedHighlight = GameObject.Find("SelectedHighlight");

		//

		selectedHighlight.SetActive(false);

	}
	
	void Update () {

		// Update mouse position
		//
		mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		if (mousePointer != null)
			mousePointer.transform.position = mousePosition;

		// Find mouse target
		//
		Collider2D mouseTargetCollider = Physics2D.OverlapPoint(mousePosition);
		mouseTarget = null;
		if (mouseTargetCollider)
			mouseTarget = mouseTargetCollider.gameObject;
		bool haveTarget = mouseTarget != null;

		bool clicking = Input.GetMouseButtonDown(0);
		bool clickingObject = clicking && haveTarget;
		bool clickingNothing = clicking && !haveTarget;

		bool hoveringObject = !clicking && haveTarget;
		bool hoveringSameObject = hoveringObject && !prevHoveringObject && prevMouseTarget == mouseTarget;
		bool hoveringNewObject = hoveringObject && !hoveringSameObject;
		bool stoppedHoveringPrevTarget = prevHoveringObject && !hoveringSameObject;
			
		if (clickingObject){
			mouseTarget.SendMessage("DoClick", null, SendMessageOptions.DontRequireReceiver);
		}

		if (clickingNothing){
			ClickedOnNothing();
		}

		if (stoppedHoveringPrevTarget){
			prevMouseTarget.SendMessage("DoStopHover", null, SendMessageOptions.DontRequireReceiver);
		}

		if (hoveringNewObject){
			mouseTarget.SendMessage("DoStartHover", null, SendMessageOptions.DontRequireReceiver);
		}

		// Highlight selected unit
		//
		if (selectedUnit != null){
			selectedHighlight.SetActive(true);
			selectedHighlight.transform.position = selectedUnit.transform.position + new Vector3(0, 0, 1);
		} else {
			selectedHighlight.SetActive(false);
		}

		// Update prev values
		//
		prevMouseTarget = mouseTarget;
		prevHoveringObject = hoveringObject;
	}

	void ClickedOnNothing(){
		selectedUnit = null;
		UnitDetails.Instance().Hide();
	}

	public void DoClickOnUnit(Unit unit){
		selectedUnit = unit;
		UnitDetails.Instance().ShowUnitDetails(unit);
	}

	public void DoCastAbility(int num){
		Ability[] abilities = selectedUnit.GetAbilities();
		Ability ability = abilities[num];
		ability.DoCast();
	}
}
