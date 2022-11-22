using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// This script allows for us to click anywhere on the NavMesh and have our player move there.
/// </summary>
public class Player : MonoBehaviour
{
    // Temp 
    [Header("For Debugging")]
    public bool HaveTarget = false;
    public GameObject SelectedTarget;
    [SerializeField] private float manacost = 10f;

    [Header("Player Stats")]
    [SerializeField] private float currentHealth;
    [SerializeField] private float MaxHealth;
    [SerializeField] private float currentMana;
    [SerializeField] private float maxMana;
    [SerializeField] private float clickDistance;

    public GameObject MySelectedTarget { get; set; }
    public float MyMaxMana => maxMana;
    public float MyCurrentMana
    {
        get { return currentMana; }
        set { currentMana = value; }
    }
    public float ManaCost
    {
        get { return manacost; }
        set { manacost = value; }
    }
    private NavMeshAgent playerAgent;
    //private SpellBook _spellBook;

    protected bool canMove => Input.GetMouseButton(0) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();
    // Player Stats region
    public CharacterStats characterStats;

    private PlayerCombat _playerCombat; 

    //public int power = 10, toughness = 10, atkSpd = 10;
    private Color hitColor = Color.yellow;

    //private EnemyTarget enemytarget; 
    void InitializeStats()
    {
        // Set player health
        MaxHealth = 100f;
        currentHealth = MaxHealth;

        // Set player mana
        maxMana = 50f; 
        currentMana = maxMana;

        // Set max click distance
        clickDistance = 300f;
        // Initialize player stats  
        //characterStats = new CharacterStats(power, toughness, atkSpd);
    }
    void Start()
    {
        InitializeStats();
        
        if (playerAgent == null)
            playerAgent = GetComponent<NavMeshAgent>();

        _playerCombat = GetComponent<PlayerCombat>();


        //EnemyTarget enemytarget = GetComponent<EnemyTarget>();
    }

    void Update()
    {
        _playerCombat.MagicLineOfSight();
        //RotateFollowMouse();
        // Move on mouse click 
        if (canMove)
        {
            MoveToCursor(); 
        }
        // Cancel/Detarget any selected target
        if (Input.GetKeyDown(KeyCode.Escape)) 
        { 
            MySelectedTarget = null; 
        }
        //Debug.DrawRay(transform.position, transform.forward * 5f, Color.red);

        #region FOR DEBUGGING
        SelectedTarget = MySelectedTarget; 

        if (MySelectedTarget == null)
            HaveTarget = false;
        else
            HaveTarget = true;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            RestoreMana(50); 
        }

        #endregion
    }
    private void RotateFollowMouse()
    {
        //Vector3 mousePos = Input.mousePosition;
        //Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
        //mousePos.z = 0;
        //mousePos.x = mousePos.x - objectPos.y;
        //mousePos.y = mousePos.y - objectPos.z;

        //float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.Euler(new Vector3(0, angle + 360, 0));

    }
    private void MoveToCursor()
    {
        if (Camera.main != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo, clickDistance))
            {
                //GameObject interactedObject = hitInfo.collider.gameObject;
                if (hitInfo.collider != null)
                    MySelectedTarget = hitInfo.collider.gameObject;


                if (MySelectedTarget.tag == "Interactable Object")
                {
                    //Debug.Log(interactedObject.name); 
                    MySelectedTarget.GetComponent<Interactable>().MoveToInteraction(playerAgent);
                }
                else if (MySelectedTarget.tag == "Enemy")
                {
                    GetComponent<PlayerCombat>().SelectEnemy(MySelectedTarget.GetComponent<EnemyTarget>());
                    MySelectedTarget.GetComponent<Interactable>().MoveToInteraction(playerAgent);
                }
                else  // Target is non-interactable
                {
                    playerAgent.stoppingDistance = 0;
                    playerAgent.destination = hitInfo.point;
                }
            }
        }
    }

    //private void interactWithCombat()
    //{
    //    RaycastHit[] hits = Physics.RaycastAll(Camera.main.ScreenPointToRay(Input.mousePosition));
    //    foreach (RaycastHit hit in hits)
    //    {
    //        EnemyTarget target = hit.transform.GetComponent<EnemyTarget>(); 
    //        if (target == null) continue;
    //        if (Input.GetMouseButtonDown(0))
    //        {
    //            SelectEnemy(target);
    //        }
    //    }
    //}
    public void SelfHealing(int amount)
    {
        // Add some hp to player
        if (currentHealth < MaxHealth)
        {
            currentHealth += amount;
            UIManager.Instance.UpdateHealthBar(currentHealth, MaxHealth);
            if (currentHealth > MaxHealth)
                currentHealth = MaxHealth;
        }
    }
    public void RestoreMana(int amount)
    {
        // Add some mp to player
        if (currentMana < maxMana)
        {
            currentMana += amount;
            UIManager.Instance.UpdateManaBar(currentMana, maxMana);
            if (currentMana > maxMana)
                currentMana = maxMana;
        }
    }
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        UIManager.Instance.UpdateHealthBar(currentHealth, MaxHealth);
        if (currentHealth <= 0)
            Die(); 
    }
    private void Die()
    {
        Debug.Log("Player dead. Reset Health.");
        this.currentHealth = this.MaxHealth; 
    }
    // How Enemies does damage to YOU!
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            EnemyTarget enemyTarget = other.gameObject.GetComponent<EnemyTarget>();
            int dmgAmt = enemyTarget.power;
            //Debug.Log("Received " + dmgAmt + " damage!");
            TakeDamage(dmgAmt);
        }
    }

}
