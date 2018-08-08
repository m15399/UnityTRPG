using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitDetails : MonoBehaviour {

	public static UnitDetails Instance(){
		// TODO
		return GameObject.Find("MainCamera/UnitDetails").GetComponent<UnitDetails>();
	}

	const int maxAbilities = 5;

	Transform container;

	Text titleText;
	Text statusText;
	Text statusDetailsText;
	Text statusDetails2Text;
	Text abilitiesText;
	Text abilitiesDetailsText;
	AbilityButton[] abilityButtons;

	Unit currentUnit = null;

	public void Hide(){
		container.gameObject.SetActive(false);
	}

	public void Show(){
		container.gameObject.SetActive(true);
	}

	public void ShowUnitDetails(Unit unit){
		Reset();

		currentUnit = unit;
		UpdateCurrentUnitDetails();

		Show();
	}

	void UpdateCurrentUnitDetails(){
		Unit unit = currentUnit;
		Unit.UnitDescription desc = unit.unitDescription;

		titleText.text = desc.name;

		string status = "Health\n\n" + desc.resourceName + "\n\nSpeed\n\nActions";
		statusText.text = status;

		UpdateStatusDetails(statusDetailsText, unit.currentStats);
		UpdateStatusDetails(statusDetails2Text, unit.defaultStats);

		string abilityNames = "";
		string abilityDetails = "";
		Ability[] unitAbilities = unit.GetAbilities();

		foreach(Ability ability in unitAbilities){
			abilityNames += ability.GetName() + "\n\n";

			int numNewLines = ability.GetDescription().Split('\n').Length;
			abilityDetails += ability.GetDescription();
			for(int i = numNewLines; i <= 3; i++){
				abilityDetails += "\n";
			}
		}

		abilitiesText.text = abilityNames;
		abilitiesDetailsText.text = abilityDetails;

		for(int i = 0; i < unitAbilities.Length; i++){
			abilityButtons[i].gameObject.SetActive(true);
			abilityButtons[i].ability = unitAbilities[i];
		}
	}

	void UpdateStatusDetails(Text text, Unit.UnitStats stats){
		string statusDetails = "";
		statusDetails += stats.health.ToString();
		statusDetails += "\n\n";
		statusDetails += stats.resource.ToString();
		statusDetails += "\n\n";
		statusDetails += stats.speed.ToString();
		statusDetails += "\n\n";
		statusDetails += stats.actions.ToString();
		text.text = statusDetails;
	}

	void Reset(){
		titleText.text = "";
		statusText.text = "";
		statusDetailsText.text = "";
		statusDetails2Text.text = "";
		abilitiesText.text = "";
		abilitiesDetailsText.text = "";

		for(int i = 0; i < maxAbilities; i++){
			AbilityButton b = abilityButtons[i];
			b.gameObject.SetActive(false);
		}

		currentUnit = null;
	}

	Text FindText(string name){
		return container.Find("Text/" + name).gameObject.GetComponent<Text>();
	}

	void Start () {
		container = transform.Find("Container");

		abilityButtons = new AbilityButton[maxAbilities];
		for(int i = 0; i < maxAbilities; i++){
			abilityButtons[i] = container.Find("Buttons/Button" + i).gameObject.GetComponent<AbilityButton>();
		}

		titleText = FindText("Title");
		statusText = FindText("Status");
		statusDetailsText = FindText("StatusDetails");
		statusDetails2Text = FindText("StatusDetails2");
		abilitiesText = FindText("Abilities");
		abilitiesDetailsText = FindText("AbilitiesDetails");

		Reset();
		Hide();
	}
	
	void Update () {
		if (currentUnit != null)
			UpdateCurrentUnitDetails();
	}
}
