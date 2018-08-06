using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillTreeController : MonoBehaviour {

    private GameObject previousPage;
    private GameObject buttons;

    private int index = 0;
    private int maxIndex;

    public Text skillPointText;

    [HideInInspector]
    public int skillPoints;

	void Start ()
    {
        buttons = GameObject.Find("Buttons");
        ActivatePage(0);

        maxIndex = buttons.transform.childCount - 1;

        skillPoints = 2;
        UpdateUI();
	}
	
	
	void Update ()
    {
		
	}

    public void UpdateUI()
    {
        skillPointText.text = string.Format("Skill points: {0}", skillPoints);

    }

    public void NextPage(int i)
    {
        index += i;
        if(index < 0)
        {
            index = maxIndex;
        }
        else if (index > maxIndex)
        {
            index = 0;
        }
        ActivatePage(index);


    }

    private void SetActiveButton(int index)
    {
        DeactivateAllButtons();
        buttons.transform.GetChild(index).gameObject.GetComponent<Image>().color = Color.gray;
       
    }

    private void DeactivateAllButtons()
    {
        for (int i = 0; i < buttons.transform.childCount; i++)
        {
            buttons.transform.GetChild(i).gameObject.GetComponent<Image>().color = Color.black;
        }
    }

    public void ActivatePage(int index)
    {
        if (previousPage != null)
        {
            previousPage.SetActive(false);
        }
        previousPage = gameObject.transform.GetChild(index).gameObject;
        gameObject.transform.GetChild(index).gameObject.SetActive(true);

        SetActiveButton(index);
    }

}
