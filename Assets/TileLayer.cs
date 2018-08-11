using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileLayer {

	ClickableSpace[] currentTiles;

	public TileLayer(){
		currentTiles = null;
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

	public void ClearTiles(){
		if (currentTiles != null){
			foreach (ClickableSpace s in currentTiles){
				GameObject.Destroy(s.gameObject);
			}
		}
		currentTiles = null;
	}

	public ClickableSpace[] LayTiles(int x, int y, int r, ClickableSpace.Type type){

		ClearTiles();

		Board board = Game.Instance().board;
		List<ClickableSpace> createdSpaces = new List<ClickableSpace>();

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

					bool shouldAdd = false;

					if (type == ClickableSpace.Type.Move){
						// Can we move to the tile?
						//
						bool canMove = false;

						if (board.IsEmpty(p)){
							canMove = true;
						}

						shouldAdd = canMove;

					} else {
						// Can we attack the tile?
						//
						shouldAdd = true;
					}

					if (shouldAdd){
						results.Add(p);
					}
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
			ClickableSpace space = ClickableSpace.Create(p.x, p.y, type);
			createdSpaces.Add(space);
		}

		ClickableSpace[] createdSpacesArray = createdSpaces.ToArray();
		currentTiles = createdSpacesArray;
		return createdSpacesArray;
	}

	public ClickableSpace FindTile(Coord coord){
		if (currentTiles != null){
			foreach(ClickableSpace tile in currentTiles){
				if (tile.pos.IsSame(coord)){
					return tile;
				}
			}
		}
		return null;
	}

}
