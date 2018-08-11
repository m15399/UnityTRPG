using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board {

	const int boardSize = 1024;

	class Tile {
		public Coord pos;
		public BoardEntity entity;

		public Tile(Coord pos){
			this.pos = pos;
			this.entity = null;
		}
	}

	Tile[,] tiles;
	List<BoardEntity> allEntities = new List<BoardEntity>();

	public Board(){
		tiles = new Tile[boardSize, boardSize];
		for(int i = 0; i < boardSize; i++){
			for (int j = 0; j < boardSize; j++){
				tiles[j,i] = new Tile(new Coord(i, j));
			}
		}
	}

	Tile GetTile(Coord c){
		if (Utils.InArray(tiles, c.y, c.x)){
			return tiles[c.y, c.x];
		} else {
			return null;
		}
	}

	bool IsEmpty(Tile t){
		bool ret = false;

		if (t != null){

			BoardEntity e = t.entity;

			if (e == null){
				ret = true;
			}
		}

		return ret;
	}

	public bool IsEmpty(Coord c){
		return IsEmpty(GetTile(c));
	}

	public void SetPosition(BoardEntity e, Coord c){
		Tile oldTile = GetTile(e.pos);
		if (oldTile != null){
			oldTile.entity = null;
		} else {
			// Adding entity to board
			//
			allEntities.Add(e);
		}

		Tile newTile = GetTile(c);
		Utils.Assert(newTile != null, "Cannot move entity off the board");
		Utils.Assert(IsEmpty(newTile), "Cannot move entity onto non-empty tile");
		newTile.entity = e;
	}

	public void RemoveEntity(BoardEntity e){
		bool removed = allEntities.Remove(e);
		Utils.Assert(removed, "Tried to remove board entity that wasn't on the board!");
	}

	public void CheckEntityPositions(){

		// TODO if debug

		// Check all BoardEntities are on board
		//
		BoardEntity[] foundEntities = GameObject.FindObjectsOfType<BoardEntity>();
		Utils.Assert(foundEntities.Length == allEntities.Count);

		// Check each entity is on the right space
		//
		foreach(BoardEntity e in allEntities){
			Coord p = e.pos;
			Utils.Assert(e == GetTile(p).entity, "Mitmatch entity position at " + p.ToString());
		}

		Utils.DebugText("Board has: " + allEntities.Count + " entities");
	}


	public BoardEntity FindEntity(Coord coord){
		Tile t = GetTile(coord);
		return t.entity;
	}

	public T FindEntity<T>(Coord coord) where T : class {
		return FindEntity(coord) as T;
	}

	public Unit FindUnit(Coord coord){
		return FindEntity<Unit>(coord);
	}

	public List<BoardEntity> FindEntitiesInArea(Coord center, int radius){

		List<BoardEntity> ret = new List<BoardEntity>();

		for(int i = center.y - radius; i <= center.y + radius; i++){
			for(int j = center.x - radius; j <= center.x + radius; j++){
				Coord c = new Coord(j, i);

				if (center.Distance(c) <= radius){
					Tile t = GetTile(c);
					if (t != null){
						BoardEntity e = t.entity;
						if (e != null){
							ret.Add(e);
						}
					}
				}
			}
		}

		return ret;
	}

	public List<T> FindEntitiesInArea<T>(Coord center, int radius) where T : class {
		List<BoardEntity> entities = FindEntitiesInArea(center, radius);
		List<T> ret = new List<T>();

		foreach(BoardEntity e in entities){
			T t = e as T;
			if (t != null){
				ret.Add(t);
			}
		}

		return ret;
	}

	public List<Unit> FindUnitsInArea(Coord center, int radius){
		return FindEntitiesInArea<Unit>(center, radius);
	}

}
