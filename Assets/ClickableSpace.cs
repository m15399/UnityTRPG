using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableSpace : MonoBehaviour {

	public static ClickableSpace Create(int x, int y, Type type){
		// TODO cache
		Transform parent = GameObject.Find("ClickableSpaces").transform;

		ClickableSpace s = Factory.Create<ClickableSpace>("ClickableSpace");
		s.transform.parent = parent;
		s.x = x;
		s.y = y;
		s.type = type;
		return s;
	}

	public enum Type {
		None,
		Move,
		Attack,
		Ability
	}

	public int x, y;
	public Type type;
	SpriteRenderer sr;

	void Start () {
		sr = transform.GetComponentInChildren<SpriteRenderer>();

		Utils.SnapToPosition(transform, x, y);

		float a = .3f;

		switch(type){
		case Type.Move:
			sr.color = new Color(0, 1, .2f, a);
			break;
		case Type.Attack:
			sr.color = new Color(1, .2f, 0, a);
			break;
		case Type.Ability:
			sr.color = new Color(.2f, .1f, 1, a);
			break;
		default:
			sr.color = new Color(0, 0, 0, 1);
			break;
		}
	}
	
	void Update () {
//		Utils.SnapToPosition(transform, x, y);
	}

	void DoClick() {
		switch(type){
		case Type.Move:
			Game.Instance().DoClickMoveSpace(this);
			break;
		case Type.Attack:
			Game.Instance().DoClickAttackSpace(this);
			break;
		case Type.Ability:
			Game.Instance().DoClickAbilitySpace(this);
			break;
		default:
			throw new UnityException("ClickableSpace click not handled for type: " + type.ToString());
		}
	}
}
