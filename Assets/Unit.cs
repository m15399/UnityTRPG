using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {

	[System.Serializable]
	public struct UnitDescription {

		public string name;
		public string resourceName; // e.g. Mana, Energy, ...

	}

	[System.Serializable]
	public struct UnitStats {
		public int health;
		public int resource;
		public int speed;
		public int actions;

		public UnitStats Clone(){
			UnitStats stats;
			stats.health = health;
			stats.resource = resource;
			stats.speed = speed;
			stats.actions = actions;
			return stats;
		}
	}

	public Coord pos;
	public UnitDescription unitDescription;
	public UnitStats defaultStats;
	public UnitStats currentStats;

	public Ability[] GetAbilities(){
		return GetComponents<Ability>();
	}

	void Start () {
		currentStats = defaultStats.Clone();
	}
	
	void Update () {
		// TODO hmmm
		Utils.SnapToPosition(transform, pos);
	}

	void DoClick(){
		Game.Instance().gameState.DoClickOnUnit(this);
	}

	public void DoMoveTo(Coord coord){
		pos = coord.Clone();
	}

	public void TakeDamage(int amount){
		currentStats.health -= amount;
	}
}
