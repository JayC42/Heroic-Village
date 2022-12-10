using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton <UIManager>
{
    [Header("Towers")]     // Tower placements
    public Button ArrowTowerButton, CannonTowerButton, IceTowerButton;
    public Image PlacementTowerUIMenu;
    private TowerPlacementController tpc;
    public GameObject arrowTowerPrefab, cannonTowerPrefab, iceTowerPrefab; 

    //SpellCast on button key press 
    public Button[] _actionButtons;
    private KeyCode _action1, _action2, _action3, _action4;

    // Public properties
    public KeyCode ActionSkill_01 => _action1;
    public KeyCode ActionSkill_02 => _action2;
    public KeyCode ActionSkill_03 => _action3;
    public KeyCode ActionSkill_04 => _action4;

    [Header("Player UI")]
    [SerializeField] private Image healthbar;
    [SerializeField] private Image manabar;

    [Header("Base Wall UI")]
    [SerializeField] private Image[] wallHP;

    [Header("Player HUD")]
    public Text[] skillTxt;
    public Text[] castTimeTxt;
    //public Image[] castTimeImg;

    void Start()
    {
        SetupButtons();
        // Keybinds to spells
        _action1 = KeyCode.Alpha1;
        _action2 = KeyCode.Alpha2;
        _action3 = KeyCode.Alpha3;
        _action4 = KeyCode.Alpha4; 

        // Default health & mana to maximum in UI 
        healthbar.fillAmount = 1;
        manabar.fillAmount = 1;

        // Set default wall HP
        foreach(Image w in wallHP)
        {
            w.fillAmount = 1;
        }

        // set HUD 
        foreach (Text txt in skillTxt)
        {
            txt.color = Color.black;
        }
        //foreach (Image img in castTimeImg)
        //{
        //    img.fillAmount = 0;
        //}
        //skillTxt[index].text = magicData[index].Name;
        //skillTxt[index].color = Color.black;
        //castTimeImg[index].color = Color.white;
        //castTimeTxt[index].color = Color.black;
    }

    void Update()
    {
        if (Input.GetKeyDown(_action1))
        {
            ActionButtonOnClick(0);
            //Debug.Log("Fire");
        }
        if (Input.GetKeyDown(_action2))
        {
            ActionButtonOnClick(1);
            //Debug.Log("Ice");
        }
        if (Input.GetKeyDown(_action3))
        {
            ActionButtonOnClick(2);
            //Debug.Log("Stun");
        }
        if (Input.GetKeyDown(_action4))
        {
            ActionButtonOnClick(3);
        }
    }
    public void ActionButtonOnClick(int btnIndex)
    {
        _actionButtons[btnIndex].onClick.Invoke();
    }
    
    // Manage healthbars & mana
    public void UpdateHealthBar(float currentHp, float maxHp)
    {
        healthbar.fillAmount = currentHp / maxHp; 
    }
    public void UpdateManaBar(float currentMp, float maxMp)
    {
        manabar.fillAmount = currentMp / maxMp;
    }
    public void UpdateWallHP(int wallIndex, float currentHp, float maxHp)
    {
        wallHP[wallIndex].fillAmount = currentHp / maxHp;
    }
    public void ResetHUDText(int index)
    {
       // castTimeImg[index].color = Color.white;
        skillTxt[index].color = Color.black;
        castTimeTxt[index].color = Color.black;
        
    }

    // tower placement UI
    public void ShowTowerMenu(TowerPlacementController placementPoint)
    {
        //print("Show tower");
        tpc = placementPoint;
        PlacementTowerUIMenu.gameObject.SetActive(true); 
    }
    public void CloseTowerMenu()
    {
        PlacementTowerUIMenu.gameObject.SetActive(false);
    }
    private void SetupButtons()
    {
        ArrowTowerButton.onClick.AddListener(() =>
        {
            GameObject tower = Instantiate(arrowTowerPrefab);
            tower.transform.position = tpc.transform.position;
            tpc.TowerPlaced(tower.GetComponent<BaseTowerController>());
            CloseTowerMenu();
        });
        CannonTowerButton.onClick.AddListener(() =>
        {
            GameObject tower = Instantiate(cannonTowerPrefab);
            tower.transform.position = tpc.transform.position;
            tpc.TowerPlaced(tower.GetComponent<BaseTowerController>());
            CloseTowerMenu();
        });
        IceTowerButton.onClick.AddListener(() =>
        {
            GameObject tower = Instantiate(iceTowerPrefab);
            tower.transform.position = tpc.transform.position;
            tpc.TowerPlaced(tower.GetComponent<BaseTowerController>());
            CloseTowerMenu();
        });
    }
}
