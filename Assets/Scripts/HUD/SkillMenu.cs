using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillMenu : MonoBehaviour {
	[SerializeField]
	private SkillButton firstSkill;
	public SkillButton currentSkill;

	public Text lateralTitle;
	public Text lateralDescription;
	public Text lateralType;
	public Text lateralUnlockText;

	void Start()
	{
		currentSkill = firstSkill;
		currentSkill.SelectSkill();
		currentSkill.MakeAvailable();
		FillLateralPanel();

	}

	public void SetCurrentSkill(SkillButton newSkill)
	{
		currentSkill.DeselectSkill();
		currentSkill = newSkill;
		currentSkill.SelectSkill();
		FillLateralPanel();
	}
	

	private void FillLateralPanel()
	{
		lateralTitle.text = currentSkill.title;
		lateralType.text = currentSkill.type;
		lateralDescription.text = currentSkill.descriptionText;
		lateralUnlockText.text = "Debloquer pour " + currentSkill.cost + " ChromDNA";
	}
}
