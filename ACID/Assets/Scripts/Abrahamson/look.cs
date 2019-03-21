using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class look : MonoBehaviour
{
    public GameObject player;
    public GameObject plane;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = this.player.transform.position;
        pos.y += 10;
        plane.transform.LookAt(pos, Vector3.down);
    }
}
