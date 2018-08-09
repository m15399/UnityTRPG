using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceBar : MonoBehaviour {

	public enum TargetResource {
		Health,
		Resource
	}

	public TargetResource targetResource;

	Unit unit;

	void Start () {
		unit = transform.GetComponentInParent<Unit>();
	}
	
	void Update () {
		int currResource = unit.currentStats.health;
		int maxResource = unit.defaultStats.health;

		if (targetResource == TargetResource.Resource){
			currResource = unit.currentStats.resource;
			maxResource = unit.defaultStats.resource;
		}

		float fraction = (float) currResource / maxResource;

		Vector3 s = transform.localScale;
		s.x = fraction;
		transform.localScale = s;
	}
}
