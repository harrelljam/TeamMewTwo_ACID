using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Terminal : MonoBehaviour
{
    public string TerminalTitle;
    public string InitialOutput;
    public string RequiredInput;
    public string ErrorMessage;
    public string FinalOutput;
    public UnityEvent Method;
}
