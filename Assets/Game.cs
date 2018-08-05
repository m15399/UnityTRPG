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

		// Test code
//		ClickableSpace o = ClickableSpace.Create(1, 2, ClickableSpace.Type.None);
//		o = ClickableSpace.Create(2, 2, ClickableSpace.Type.Attack);
//		o = ClickableSpace.Create(3, 1, ClickableSpace.Type.Move);
//		o = ClickableSpace.Create(2, 1, ClickableSpace.Type.Ability);

		LayTiles(4, 2, 4, ClickableSpace.Type.Move);
	}

	class Coord {
		public int x, y;
		public Coord(){
			x = y = 0;
		}
		public Coord(int x, int y){
			this.x = x;
			this.y = y;
		}
	}

	enum CoordState {
		None,
		Visited
	}

	void AddIfNotVisited(CoordState[,] coords, List<Coord> q, int x, int y){
		if (Utils.InArray(coords, x, y) && coords[x,y] != CoordState.Visited){
			q.Add(new Coord(x, y));
			coords[x,y] = CoordState.Visited;
		}
	}

	int LayTiles(int x, int y, int r, ClickableSpace.Type type){
		int numCreated = 0;

		// TODO size?
		const int size = 64;
		CoordState[,] coords = new CoordState[size,size];

		List<Coord> results = new List<Coord>();

		List<Coord> q = new List<Coord>();

		// Algorithm #1
		//
		q.Add(new Coord(x, y));
		coords[x, y] = CoordState.Visited;

		for(int i = 0; i <= r; i++){

			List<Coord> newQ = new List<Coord>();

			foreach(Coord p in q){
				if (i != 0){
					results.Add(p);
				}

				if (i != r){
					AddIfNotVisited(coords, newQ, p.x + 1, p.y);
					AddIfNotVisited(coords, newQ, p.x, p.y + 1);
					AddIfNotVisited(coords, newQ, p.x - 1, p.y);
					AddIfNotVisited(coords, newQ, p.x, p.y - 1);
				}
			}

			q = newQ;
		}

		foreach(Coord p in results){
			ClickableSpace.Create(p.x, p.y, type);
			numCreated++;
		}

		return numCreated;
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

	public void DoClickMoveSpace(ClickableSpace space){
		Debug.Log("Clicked move " + space.x + ", " + space.y);
	}

	public void DoClickAttackSpace(ClickableSpace space){
		Debug.Log("Clicked attack " + space.x + ", " + space.y);
	}

	public void DoClickAbilitySpace(ClickableSpace space){
		Debug.Log("Clicked ability " + space.x + ", " + space.y);
	}

	public void DoCastAbility(Ability ability){
		ability.DoCast();
	}
}
