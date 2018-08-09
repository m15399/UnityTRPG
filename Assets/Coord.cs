using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Coord {

	public int x, y;

	public Coord(){
		x = y = 0;
	}

	public Coord(int x, int y){
		this.x = x;
		this.y = y;
	}

	public Coord Clone(){
		return new Coord(x, y);
	}

	public override string ToString ()
	{
		return string.Format ("({0}, {1})", x, y);
	}

	public bool IsSame (Coord c)
	{
		return c.x == x && c.y == y;
	}

	public int Distance(Coord o){
		int dx = Mathf.Abs(o.x - x);
		int dy = Mathf.Abs(o.y - y);
		return dx + dy;
	}
}
