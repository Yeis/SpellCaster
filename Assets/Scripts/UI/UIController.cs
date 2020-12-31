using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UIController : MonoBehaviour {
    public List<string> commands;
    public List<Text> commandLabels, spellLabels;
    private Text attackLabel, spellLabel;
    public float padding = 50f;
    public Font font;
    private GameObject mainOptionsPanel, spellOptionsPanel;
    private Image selector, attackSelector;
    public string currentCommand, currentSpell;
    public int currentMenuIndex, currentSpellIndex;
    private bool isInAttackMenu = false, isInTypingMode = false;
    private float initialYSelectorPosition, inputDelay;
    private Vector2 navigation;
    private Animator animator;


    private void OnEnable() {
        Keyboard.current.onTextInput += HandleTypingInput;
    }

    private void OnDisable() {
        Keyboard.current.onTextInput -= HandleTypingInput;
    }

    // Start is called before the first frame update
    void Start() {
        //Trigger Animation
        animator = GetComponent<Animator>();
        animator.SetTrigger("EnableUI");

        //Get all UI References
        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        commandLabels = new List<Text>();
        mainOptionsPanel = GameObject.Find("Main_Options_Panel");
        spellOptionsPanel = GameObject.Find("Spell_Options_Panel");
        selector = GameObject.Find("Selector").GetComponent<Image>();
        attackSelector = GameObject.Find("Attack_Selector").GetComponent<Image>();
        attackLabel = GameObject.Find("Attack_Label").GetComponent<Text>();
        spellLabel = GameObject.Find("Spell_Label").GetComponent<Text>();
        ToggleAttackSubMenu(false);
        initialYSelectorPosition = selector.rectTransform.localPosition.y;

        currentSpellIndex = 0;
        currentMenuIndex = 0;
        currentCommand = commands[0];
        spellLabel.text = currentSpell;
        foreach (string command in commands) {
            GameObject textLabel = new GameObject(command + "_Label");
            textLabel.transform.SetParent(mainOptionsPanel.transform);

            Text text = textLabel.AddComponent<Text>();
            text.text = command;
            text.font = font;
            text.rectTransform.sizeDelta = new Vector2(100, 40);
            text.fontSize = 32;
            text.color = Color.black;
            text.alignment = TextAnchor.UpperLeft;
            commandLabels.Add(text);
        }

        foreach (Spell spell in player.spellBook) {
            GameObject textLabel = new GameObject(spell.name + "_Label");
            textLabel.transform.SetParent(spellOptionsPanel.transform);

            Text text = textLabel.AddComponent<Text>();
            text.text = spell.name;
            text.font = font;
            text.rectTransform.sizeDelta = new Vector2(100, 40);
            text.fontSize = 32;
            text.color = Color.black;
            text.alignment = TextAnchor.UpperLeft;
            spellLabels.Add(text);
        }
    }

    // Update is called once per frame
    void Update() {
        HandleMenuInput();
    }

    private void HandleTypingInput(char letter) {
        if (isInTypingMode) {
            print("CurrentSpellIndex: " + currentSpellIndex);
            //Correct Letter
            if (currentSpell.Length > currentSpellIndex && currentSpell[currentSpellIndex] == letter) {
                print("Correct Letter");
                UpdateSpellLabel(++currentSpellIndex);

                if (currentSpellIndex == spellLabel.text.Length) {
                    ResetHUD();
                }
            }
        }
    }

    private void ResetHUD() {
        print("ResetHUD");
        ToggleAttackSubMenu(false);
        isInTypingMode = false;
        currentMenuIndex = 0;
        currentSpellIndex = 0;
        currentSpell = "";
        spellLabel.text = currentSpell;
    }

    private void UpdateSpellLabel(int i) {
        spellLabel.text = "<color=red>" + currentSpell.Substring(0, i) + "</color>" + currentSpell.Substring(i, currentSpell.Length - i);
    }

    private void HandleMenuInput() {
        if (Keyboard.current[Key.Space].wasPressedThisFrame ||
                Keyboard.current[Key.RightArrow].wasPressedThisFrame ||
                Keyboard.current[Key.Enter].wasPressedThisFrame) {
            if (isInAttackMenu) {
                isInTypingMode = true;
                ToggleAttackSubMenu(false);
                currentSpell = spellLabels[currentMenuIndex].text;
                spellLabel.text = currentSpell;
                currentSpellIndex = 0;
            } else if (!isInTypingMode) {
                ToggleAttackSubMenu(true);
                currentMenuIndex = 0;
            }
        } else if (Keyboard.current[Key.LeftArrow].wasPressedThisFrame) {
            if (isInAttackMenu) {
                ToggleAttackSubMenu(false);
                currentMenuIndex = 0;
            }
            //If we want to allow going back once spell is selected
            // else if (isInTypingMode)
            // {
            //     isInTypingMode = false;
            //     ToggleAttackSubMenu(true);
            //     currentMenuIndex = 0;
            //     currentSpellIndex = 0;
            // }

        } else if (navigation.y > 0) {
            if (isInAttackMenu) {
                currentMenuIndex = Mathf.Max((currentMenuIndex - 1) % spellLabels.Count, 0);
            } else {
                currentMenuIndex = Mathf.Max((currentMenuIndex - 1) % commands.Count, 0);
            }

        } else if (navigation.y < 0) {
            if (isInAttackMenu) {
                currentMenuIndex = Mathf.Clamp(currentMenuIndex + 1, 0, spellLabels.Count - 1);
            } else {
                currentMenuIndex = Mathf.Clamp(currentMenuIndex + 1, 0, commands.Count - 1);
            }
        }

        //Set selector position to be left of the command 
        float newYPosition;
        if (isInAttackMenu) {
            newYPosition = spellLabels[currentMenuIndex].rectTransform.position.y;
            attackSelector.rectTransform.position = new Vector3(attackSelector.rectTransform.position.x, newYPosition, attackSelector.rectTransform.position.z);
        } else {
            newYPosition = commandLabels[currentMenuIndex].rectTransform.position.y;
            selector.rectTransform.position = new Vector3(selector.rectTransform.position.x, newYPosition, selector.rectTransform.position.z);
        }
    }

    public void OnNavigate(InputValue value) {
        navigation = value.Get<Vector2>();
    }

    private void ToggleAttackSubMenu(bool toogle) {
        isInAttackMenu = toogle;
        attackSelector.enabled = toogle;
        spellOptionsPanel.SetActive(toogle);
        attackLabel.enabled = toogle;
    }
}
