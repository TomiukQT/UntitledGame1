using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour {

    private GameObject desc;
    private Text descText;
    private SkillTreeController stc;

    public Skill skill;

	void Start ()
    {
        desc = gameObject.transform.GetChild(0).gameObject;
        descText = desc.transform.GetChild(0).GetComponent<Text>();
        descText.text = skill.name + "\n" + skill.desc;
        gameObject.GetComponent<Image>().sprite = skill.icon;


        stc = GameObject.Find("SkillTree").GetComponent<SkillTreeController>();
	}
	
	
	void Update ()
    {
		
	}

    public void ShowDescription()
    {
        desc.gameObject.SetActive(true);
    }

    public void HideDescription()
    {
        desc.gameObject.SetActive(false);
    }

    public void ActivateButton()
    {
        if(stc.skillPoints > 0)
        {
            gameObject.GetComponent<Image>().color = Color.white;
            gameObject.GetComponent<Button>().enabled = false;

            stc.skillPoints--;
            stc.UpdateUI();
        }
    }
        
    
}
