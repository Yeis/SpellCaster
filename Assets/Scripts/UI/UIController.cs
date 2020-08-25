using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Text spellText, timeleftText, attackButton;
    public Slider castingSlider, healthSlider;
    public Image sliderFill;
    // Start is called before the first frame update
    void Start()
    {

    }


    public void SetupNewSpell(Spell spell, float castingTime)
    {
        castingSlider.maxValue = castingTime;

    }

    public void UpdateSpellText(string spellName, int currentSpellIndex)
    {
        // print("currentIndex: " + currentSpellIndex.ToString());
        spellText.text = "<color=red>" + spellName.Substring(0, currentSpellIndex) + "</color>" + spellName.Substring(currentSpellIndex, spellName.Length - currentSpellIndex);
    }

    public void UpdateTimeLeft(bool isPenalty, float timeLeft)
    {
        if (isPenalty)
        {
            timeleftText.text = "Penalty Left: " + timeLeft.ToString();
        }
        else
        {
            timeleftText.text = "Time Left: " + timeLeft.ToString();
        }
        castingSlider.value = timeLeft;
    }

    public void UpdateHealth(float currentHealthPoints)
    {
        healthSlider.value = currentHealthPoints;
    }

    public void UpdateSlider(bool isPenalty, float maxValue)
    {
        if (isPenalty) sliderFill.color = Color.magenta;
        else sliderFill.color = Color.green;

        castingSlider.value = maxValue;
        castingSlider.maxValue = maxValue;
    }

    public void DisableAttackUI()
    {
        attackButton.enabled = false;
    }

    public void EnableAttackUI()
    {
        attackButton.enabled = true;
    }

    public void SetupPlayerHUD(float healthPoints)
    {
        healthSlider.value = healthPoints;
        healthSlider.maxValue = healthPoints;
    }


}
