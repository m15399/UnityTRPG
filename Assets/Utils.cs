using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Utils {

	static int lastDebugTextFrame = 0;

	static Text GetDebugText(){
		// TODO
		Text debugText = GameObject.Find("DebugText").GetComponent<Text>();
		return debugText;
	}

	public static void DebugText(string s){
		Text debugText = GetDebugText();
		if (lastDebugTextFrame != Time.frameCount){
			debugText.text = "";
			lastDebugTextFrame = Time.frameCount;
		}
		debugText.text += s + "\n";
	}

	public static void DebugText(string s, GameObject o){
		if (o != null){
			DebugText(s + ": " + o.ToString());
		} else {
			DebugText(s + ": -");
		}
	}

	public static void DebugText(string s, bool b){
		DebugText(s + ": " + b.ToString());
	}


	public static void SnapToPosition(Transform t, int x, int y){
		Vector3 pos = t.position;
		pos.x = x;
		pos.y = y;
		t.position = pos;
	}

	public static bool InArray<T>(T[,] a, int i, int j){
		return i >= 0 && i < a.GetLength(0) && j >= 0 && j < a.GetLength(1);
	}
}
