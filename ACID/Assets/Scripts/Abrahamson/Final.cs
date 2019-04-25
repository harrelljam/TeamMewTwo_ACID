using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Final : MonoBehaviour
{

    public GameObject WinScreen;
    private bool _riddle1;
    private bool _riddle2;
    private bool _riddle3;
    private bool _riddle4;

    public void CheckComplete()
    {
        if (_riddle1 && _riddle2 & _riddle3 & _riddle4)
        {
            WinScreen.SetActive(true);
            StartCoroutine(WaitClose());
        }
    }
    
    public void Riddle1()
    {
        _riddle1 = true;
        CheckComplete();
    }
    public void Riddle2()
    {
        _riddle2 = true;
        CheckComplete();
    }
    public void Riddle3()
    {
        _riddle3 = true;
        CheckComplete();
    }
    public void Riddle4()
    {
        _riddle4 = true;
        CheckComplete();
    }

    private IEnumerator WaitClose()
    {
        yield return new WaitForSeconds(5);
        Application.Quit();
    }
}
