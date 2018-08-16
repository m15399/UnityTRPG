using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Send a message to some global target on some action
//
// e.g. "On click, send a "DoEndTurnPressed" message to the Game"
//
public class SendGlobalMessage : MonoBehaviour {

	public string message;

	public bool onClick = true;

	[System.Serializable]
	public enum Target {
		Game
	}

	public Target target;

	GameObject GetTarget(){
		GameObject ret = null;

		switch (target){
		case Target.Game:
			ret = Game.Instance().gameObject;
			break;
		}

		return ret;
	}

	void DoClick(){
		if (onClick){
			GetTarget().SendMessage(message);
		}
	}

}
