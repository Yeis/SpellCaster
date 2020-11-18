using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewUIController : MonoBehaviour
{
    public List<string> commands;
    public List<Text> commandLabels;
    public float padding = 50f;
    public Font font;
    private GameObject mainOptionsPanel;
    private Image selector;
    public string currentCommand;
    public int currentMenuIndex;
    private float initialYSelectorPosition;
    // Start is called before the first frame update
    void Start()
    {
        commandLabels = new List<Text>();
        mainOptionsPanel = GameObject.Find("Main_Options_Panel");
        selector = GameObject.Find("Selector").GetComponent<Image>();
        initialYSelectorPosition =  selector.rectTransform.localPosition.y;

        currentCommand = commands[0];
        foreach(string command in commands) {
            GameObject textLabel = new GameObject(command + "_Label");
            textLabel.transform.SetParent(mainOptionsPanel.transform);

            Text text = textLabel.AddComponent<Text>();
            text.text = command;
            text.font = font;
            text.rectTransform.sizeDelta = new Vector2 (100, 40);
            text.fontSize = 32;
            text.color = Color.black;
            text.alignment = TextAnchor.UpperLeft;
            commandLabels.Add(text);
        }
    }   

    // Update is called once per frame
    void Update()
    {
        print("CurrentCommnd: " + currentCommand);
        print("CurrentMenuIndex: " + currentMenuIndex);

        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            currentMenuIndex = Mathf.Max((currentMenuIndex - 1) % commands.Count, 0);
            currentCommand =  commands[currentMenuIndex];
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow)) {
            currentMenuIndex = Mathf.Min((currentMenuIndex + 1) % commands.Count, commands.Count);
            currentCommand = commands[currentMenuIndex];
        }

        //Set selector position to be left of the command 
        selector.rectTransform.position = new Vector3() 
        commandLabels[currentMenuIndex].rectTransform.position.y;
        M1ndG


    }
}
