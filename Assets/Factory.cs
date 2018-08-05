using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory {

	public static GameObject Create(string resourceName){
		GameObject prefab = Resources.Load("Prefabs/" + resourceName) as GameObject;
		GameObject o = GameObject.Instantiate(prefab);
		return o;
	}

	public static T Create<T>(string resourceName){
		return Create(resourceName).GetComponent<T>();
	}

}
