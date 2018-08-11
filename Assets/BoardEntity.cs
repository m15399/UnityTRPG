using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Object that has a position on the board
//
public class BoardEntity : MonoBehaviour {

	Coord _pos = new Coord(-1, -1);

	public Coord pos {
		get {
			return _pos;
		}
		set {
			Game.Instance().board.SetPosition(this, value);
			_pos = value;
		}
	}

}
