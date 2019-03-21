using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadiusDamage : MonoBehaviour
{
	public GameObject player;
	public float damagePerSecond;
	private bool inside;

    // Start is called before the first frame update
    void Start()
    {
        inside = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(inside){
        	//deal damage per second to the player
        	
        }
    }
    
    private void OnTriggerEnter(Collider obj){
    	if(obj.gameObject.CompareTag("Player")){
    		inside = true;
    	}
    }
    
    private void OnTriggerExit(Collider obj){
    	if(obj.gameObject.CompareTag("Player")){
    		inside = false;
    	}
    }
}
