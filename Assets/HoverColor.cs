using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverColor : MonoBehaviour {

	public Color hoverColor = Color.white;
	public Color defaultColor { get; set; }

	SpriteRenderer sr;

	void Start () {
		sr = GetComponent<SpriteRenderer>();
		defaultColor = sr.color;
		DoStopHover();
	}

	void Update () {

	}

	void DoStartHover(){
		sr.color = hoverColor;
	}

	void DoStopHover(){
		sr.color = defaultColor;
	}
}
