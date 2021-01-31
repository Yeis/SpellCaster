﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.UI;
using System.ComponentModel;
using System;

public class UIController : MonoBehaviour, INotifyPropertyChanged {
    public List<string> commands;
    public List<Text> commandLabels, spellLabels;
    private List<Spell> spellList;
    private Text attackLabel, spellLabel;
    public float padding = 50f;
    public Font font;
    private GameObject mainOptionsPanel, spellOptionsPanel;
    private Image selector, attackSelector;
    public string currentCommand;
    private Spell currentSpell;
    public int currentMenuIndex, currentSpellIndex;
    private bool isInAttackMenu = false, isInTypingMode = false;
    private float initialYSelectorPosition, inputDelay;
    private Animator animator;
    private bool isEnabled;
    private Player player;
    public Slider HPSlider;
    private PlayerState state = PlayerState.Unknown;
    public PlayerState StateEnum { get => state; set => state = value; }
    public bool IsInAttackMenu { get => isInAttackMenu; }
    public bool IsInTypingMode { get => isInTypingMode; }

    public Spell CurrentSpell {
        get => currentSpell;
        set {
            currentSpell = value;
            OnPropertyChanged("CurrentSpell");
        }
    }

    private void OnEnable() {
        Keyboard.current.onTextInput += HandleTypingInput;
    }

    private void OnDisable() {
        Keyboard.current.onTextInput -= HandleTypingInput;
    }

    void Awake()
    {
        //Trigger Animation
        animator = GetComponent<Animator>();
        //Get all UI References
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        commandLabels = new List<Text>();
        mainOptionsPanel = GameObject.Find("Main_Options_Panel");
        spellOptionsPanel = GameObject.Find("Spell_Options_Panel");
        selector = GameObject.Find("Selector").GetComponent<Image>();
        attackSelector = GameObject.Find("Attack_Selector").GetComponent<Image>();
        attackLabel = GameObject.Find("Attack_Label").GetComponent<Text>();
        spellLabel = GameObject.Find("Spell_Label").GetComponent<Text>();
        HPSlider = GameObject.Find("HP_Slider").GetComponent<Slider>();
    }

    // Start is called before the first frame update
    void Start() {


        //Get all UI References
        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        commandLabels = new List<Text>();
 
        ToggleAttackSubMenu(false);
        initialYSelectorPosition = selector.rectTransform.localPosition.y;

        currentSpellIndex = 0;
        currentMenuIndex = 0;
        currentCommand = commands[0];
        spellLabel.text = "";
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

        spellList = player.spellBook;
        foreach (Spell spell in player.spellBook) {
            GameObject textLabel = new GameObject(spell.spellName + "_Label");
            textLabel.transform.SetParent(spellOptionsPanel.transform);

            Text text = textLabel.AddComponent<Text>();
            text.text = spell.spellName;
            text.font = font;
            text.rectTransform.sizeDelta = new Vector2(100, 40);
            text.fontSize = 32;
            text.color = Color.black;
            text.alignment = TextAnchor.UpperLeft;
            spellLabels.Add(text);
        }
    }

    void Update() {
        //Set selector position to be left of the command 
        float newYPosition;
        if (isInAttackMenu) {
            newYPosition = spellLabels[currentMenuIndex].rectTransform.position.y;
            attackSelector.rectTransform.position = new Vector3(attackSelector.rectTransform.position.x, newYPosition, attackSelector.rectTransform.position.z);
        } else {
            newYPosition = commandLabels[currentMenuIndex].rectTransform.position.y;
            selector.rectTransform.position = new Vector3(selector.rectTransform.position.x, newYPosition, selector.rectTransform.position.z);
        }
        switch (player.StateEnum) {
            case PlayerState.Cooldown:
            case PlayerState.Action:
                isEnabled = false;
                break;
            case PlayerState.Standby:
                isEnabled = true;
                break;
            default: break;
        }
    }

    private void HandleTypingInput(char letter) {
        if (isInTypingMode) {
            print("CurrentSpellIndex: " + currentSpellIndex);
            //Correct Letter
            if (currentSpell.spellName.Length > currentSpellIndex && currentSpell.spellName[currentSpellIndex] == letter) {
                print("Correct Letter");
                UpdateSpellLabel(++currentSpellIndex);

                //Terminamos de escribir la palabra
                if (currentSpellIndex == currentSpell.spellName.Length) {
                    ResetHUD();
                    player.stockpile = currentSpell;
                    player.Animator.SetTrigger("Cast");
                }
            } else {
                // send to cooldown
                ResetHUD();
                Cooldown.ResetPosition(player, 0.49f, 0.1789f);
                player.SetState(new CooldownState(player));
            }
        }
    }

    private IEnumerator DampenHealthBar(int value) {
        for (int i = 0; i < value; i++)
        {
            this.HPSlider.value += 1;
            yield return null;
            this.HPSlider.value -= 1;
            yield return null;
        }
        yield return null;
    }

    public void UpdateHealth(int value, bool dampening) {
        this.HPSlider.value = value;
        if(dampening) {
            StartCoroutine(DampenHealthBar(value));
        }
    }


    private void ResetHUD() {
        print("ResetHUD");
        ToggleAttackSubMenu(false);
        isInTypingMode = false;
        currentMenuIndex = 0;
        currentSpellIndex = 0;
        CurrentSpell = new Spell();
        spellLabel.text = "";
    }

    private void UpdateSpellLabel(int i) {
        spellLabel.text = "<color=red>" + currentSpell.spellName.Substring(0, i) + "</color>" + currentSpell.spellName.Substring(i, currentSpell.spellName.Length - i);
    }

    public void OnSubmit(InputValue value) {
        MenuForward();
    }

    public void OnCancel(InputValue value) {
        MenuBackward();
    }

    public void OnNavigate(InputValue value) {
        if (isEnabled) {
            var nav = value.Get<Vector2>();
            if (nav.x > 0) { // forward
                MenuForward();
            } else if (nav.x < 0) { // back
                MenuBackward();
            } else if (nav.y > 0) { // up
                if (isInAttackMenu) {
                    currentMenuIndex = Mathf.Max((currentMenuIndex - 1) % spellLabels.Count, 0);
                    CurrentSpell = spellList[currentMenuIndex];
                } else {
                    currentMenuIndex = Mathf.Max((currentMenuIndex - 1) % commands.Count, 0);
                }
            } else if (nav.y < 0) { // down
                if (isInAttackMenu) {
                    currentMenuIndex = Mathf.Clamp(currentMenuIndex + 1, 0, spellLabels.Count - 1);
                    CurrentSpell = spellList[currentMenuIndex];
                } else {
                    currentMenuIndex = Mathf.Clamp(currentMenuIndex + 1, 0, commands.Count - 1);
                }
            }

        }
    }

    public void EnableUI() {
        animator.SetTrigger("EnableUI");
        isEnabled = true;
    }


    public void DisableUI() {
        animator.SetTrigger("DisableUI");
        isEnabled = false;
    }

    private void MenuForward() {
        if (isInAttackMenu) {
            isInTypingMode = true;
            ToggleAttackSubMenu(false);
            spellLabel.text = currentSpell.spellName;
            currentSpellIndex = 0;

            player.SetState(new ActionState(player));
        } else if (!isInTypingMode) {
            ToggleAttackSubMenu(true);
            currentMenuIndex = 0;
            CurrentSpell = spellList[currentMenuIndex];

            player.SetState(new AimState(player));
        }
    }

    private void MenuBackward() {
        if (isInAttackMenu) {
            ToggleAttackSubMenu(false);
            currentMenuIndex = 0;

            player.SetState(new StandbyState(player));
        }
    }

    private void ToggleAttackSubMenu(bool toogle) {
        isInAttackMenu = toogle;
        attackSelector.enabled = toogle;
        spellOptionsPanel.SetActive(toogle);
        attackLabel.enabled = toogle;
    }

    // INotifyPropertyChanged
    public event PropertyChangedEventHandler PropertyChanged;
    private void OnPropertyChanged(string propertyName) {
        if (PropertyChanged != null) {
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
