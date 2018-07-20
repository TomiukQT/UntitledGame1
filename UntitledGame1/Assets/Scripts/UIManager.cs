using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {


    public Button SwitchButton;
    public Button CableButton;
    public Button TrashButton;

    [Header("Clocks")]
    public Text ClockText;
    public Text DateText;
    



    private BuildingScript bScript;



    void Start()
    {
        bScript = GameObject.Find("GameManager").GetComponent<BuildingScript>();
        

        Button switchButton = SwitchButton.GetComponent<Button>();
        switchButton.onClick.AddListener(SwitchButtonClick);

        Button cableButton = CableButton.GetComponent<Button>();
        cableButton.onClick.AddListener(CableButtonClick);

        Button trashButton = TrashButton.GetComponent<Button>();
        trashButton.onClick.AddListener(TrashButtonClick);

        

    }


    void Update()
    {

    }

    void SwitchButtonClick()
    {
        bScript.ChooseObjectToBuild(0);
    }

    void CableButtonClick()
    {
        bScript.ChooseObjectToBuild(1);
    }

    void TrashButtonClick()
    {
        bScript.ChangeMode(2);
    }

    


}
