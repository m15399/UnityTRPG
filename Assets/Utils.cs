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

}
