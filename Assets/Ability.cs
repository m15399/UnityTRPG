using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour {

	public string GetName(){
		return ("Default Ability");
	}

	public string GetDescription(){
		return "(Default\nability\ndescription)";
	}

	public void DoCast(){
		Debug.Log("Casting ability " + GetName() + "!");
	}

	void Start () {
		
	}
	
	void Update () {
		
	}
}
