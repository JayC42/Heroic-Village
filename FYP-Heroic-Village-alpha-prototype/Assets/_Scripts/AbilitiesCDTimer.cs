using UnityEngine;
using UnityEngine.UI;


public class AbilitiesCDTimer : MonoBehaviour
{
    [Header("Ability 1")]
    [SerializeField] Image skillImage1;
    [SerializeField] float cdTime1 = 5;
    private bool isCooldown1 = false;

    [Header("Ability 2")]
    [SerializeField] Image skillImage2;
    [SerializeField] float cdTime2 = 5;
    private bool isCooldown2 = false;

    [Header("Ability 3")]
    [SerializeField] Image skillImage3;
    [SerializeField] float cdTime3 = 5;
    private bool isCooldown3 = false;

    [Header("Ability 4")]
    [SerializeField] Image skillImage4;
    [SerializeField] float cdTime4 = 5;
    private bool isCooldown4 = false;

    [SerializeField] MagicData[] _md;

    private void Start()
    {
        skillImage1.fillAmount = 0;
        skillImage2.fillAmount = 0;
        skillImage3.fillAmount = 0;
        skillImage4.fillAmount = 0;

        // for scriptable objects magic
        cdTime1 = _md[0].CastTime; 
        cdTime2 = _md[1].CastTime;
        cdTime3 = _md[2].CastTime;

        // special case magic
        cdTime4 = 30f; 
    }

    private void Update()
    {
        // TOFIX: CD TImer runs regardless of whether the target is in range or the spell is fired
        Ability1CDTimer();
        Ability2CDTimer();
        Ability3CDTimer();
        Ability4CDTimer();
    }
    public void Ability1CDTimer()
    {
        if (Input.GetKey(UIManager.Instance.ActionSkill_01) && !isCooldown1)
        {
            isCooldown1 = true;
            skillImage1.fillAmount = 1; 
        }
        if (isCooldown1)
        {
            skillImage1.fillAmount -= 1 / cdTime1 * Time.deltaTime;
            if (skillImage1.fillAmount <= 0)
            {
                skillImage1.fillAmount = 0;
                isCooldown1 = false; 
            }
        }
    }
    public void Ability2CDTimer()
    {
        if (Input.GetKey(UIManager.Instance.ActionSkill_02) && !isCooldown2)
        {
            isCooldown2 = true;
            skillImage2.fillAmount = 1;
        }
        if (isCooldown2)
        {
            skillImage2.fillAmount -= 1 / cdTime2 * Time.deltaTime;
            if (skillImage2.fillAmount <= 0)
            {
                skillImage2.fillAmount = 0;
                isCooldown2 = false;
            }
        }
    }
    public void Ability3CDTimer()
    {
        if (Input.GetKey(UIManager.Instance.ActionSkill_03) && !isCooldown3)
        {
            isCooldown3 = true;
            skillImage3.fillAmount = 1;
        }
        if (isCooldown3)
        {
            skillImage3.fillAmount -= 1 / cdTime3 * Time.deltaTime;
            if (skillImage3.fillAmount <= 0)
            {
                skillImage3.fillAmount = 0;
                isCooldown3 = false;
            }
        }
    }
    public void Ability4CDTimer()
    {
        if (Input.GetKey(UIManager.Instance.ActionSkill_04) && !isCooldown4)
        {
            isCooldown4 = true;
            skillImage4.fillAmount = 1;
        }
        if (isCooldown4)
        {
            skillImage4.fillAmount -= 1 / cdTime4 * Time.deltaTime;
            if (skillImage4.fillAmount <= 0)
            {
                skillImage4.fillAmount = 0;
                isCooldown4 = false;
            }
        }
    }
}
