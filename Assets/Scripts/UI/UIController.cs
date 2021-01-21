using System.Collections;
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
    private bool pendingPunishment;

    private PlayerState state = PlayerState.Unknown;
    public PlayerState StateEnum { get => state; set => state = value; }
    public bool IsInAttackMenu { get => isInAttackMenu; }
    public bool IsInTypingMode { get => isInTypingMode; }
    public bool PendingPunishment { get => pendingPunishment; set => pendingPunishment = value; }

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

    // Start is called before the first frame update
    void Start() {
        //Trigger Animation
        animator = GetComponent<Animator>();

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
        switch (state) {
            case PlayerState.Cooldown:
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
                }
            } else {
                // send to cooldown
                ResetHUD();
                pendingPunishment = true;
            }
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
        print("Aqui andamos 1");

        if (isEnabled) {
            print("Aqui andamos 2");
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
        } else if (!isInTypingMode) {
            ToggleAttackSubMenu(true);
            currentMenuIndex = 0;
            CurrentSpell = spellList[currentMenuIndex];
        }
    }

    private void MenuBackward() {
        if (isInAttackMenu) {
            ToggleAttackSubMenu(false);
            currentMenuIndex = 0;
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
