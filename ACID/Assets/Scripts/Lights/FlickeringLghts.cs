using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickeringLghts : MonoBehaviour
{
    public Light[] points;
    public float minWaitTime;
    public float maxWaitTime;
    public float lightFlickerIntensity;
    private bool lightFlickering = true;
    private int _numOfFlicks = 10;
    public bool test;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Flashing());
    }

    private void Update()
    {
        if (test)
        {
            turnOn();
            test = false;
        }

    }

    public void turnOn()
    {
        foreach(Light x in points)
        {
            x.intensity = 1;
        }
    }

    private IEnumerator Flashing()
    {
        yield return new WaitForSeconds(1f);
        int flick = 0;
        while(flick < _numOfFlicks)
        {
            if (lightFlickering)
                yield return new WaitForSeconds(minWaitTime);
            else
                yield return new WaitForSeconds(maxWaitTime);
            foreach(Light x in points)
            {
                if (!lightFlickering)
                    x.intensity = lightFlickerIntensity;
                else
                    x.intensity = 0;
            }
            lightFlickering = !lightFlickering;
            flick++;
        }

        foreach (Light x in points)
        {
                x.intensity = 0.3f;
        }
    }
}
