using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenGui : MonoBehaviour
{
    public GameObject screen;
    public PauseMenu pausemenu;

    public void Show()
    {
        screen.SetActive(true);
        pausemenu.Freeze();
    }

    public void Hide()
    {
        screen.SetActive(false);
        pausemenu.Unfreeze();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
