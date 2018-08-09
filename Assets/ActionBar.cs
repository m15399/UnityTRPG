using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionBar : MonoBehaviour {

	public int actionNumber;

	Unit unit;
	SpriteRenderer sr;

	void Start () {
		unit = transform.GetComponentInParent<Unit>();
		sr = GetComponent<SpriteRenderer>();
	}

	void Update () {
		int numActions = unit.currentStats.actions;

		if (numActions > actionNumber){
			sr.enabled = true;
		} else {
			sr.enabled = false;
		}
	}
}
