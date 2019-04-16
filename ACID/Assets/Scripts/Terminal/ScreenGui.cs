using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScreenGui : MonoBehaviour
{
    public GameObject screen;
    public PauseMenu pausemenu;

    public TextMeshProUGUI Title;
    public TextMeshProUGUI Output;
    public TextMeshProUGUI Error;
    public TextMeshProUGUI InputField;
    private string _requiredInput;
    private string _errorMessage;
    private string _finalOutput;
    private bool _isOpen;

    public void Show()
    {
        _isOpen = true;
        Interact.I.RaycastReady = false;
        screen.SetActive(true);
        pausemenu.Freeze();
    }

    public void Hide()
    {
        _isOpen = false;
        Interact.I.RaycastReady = true;
        screen.SetActive(false);
        pausemenu.Unfreeze();
    }

    public void OpenTerminal(Terminal terminal)
    {
        Title.text = terminal.TerminalTitle;
        Output.text = terminal.InitialOutput;
        _requiredInput = terminal.RequiredInput;
        _errorMessage = terminal.ErrorMessage;
        _finalOutput = terminal.FinalOutput;
        Show();
    }

    // Update is called once per frame
    void Update()
    {
        if (_isOpen)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                print(InputField.text + "   vs   " + _requiredInput + "  equals  " + InputField.text.CompareTo(_requiredInput));
                if (InputField.text.CompareTo(_requiredInput) == 1)
                {
                    Output.text = _finalOutput;
                    Error.text = "";
                }
                else
                {
                    Error.text = _errorMessage;
                }
            }
        }
    }
}
