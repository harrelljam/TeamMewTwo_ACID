using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadiusDamage : MonoBehaviour
{
	public GameObject player;
	private bool inside;

    // Start is called before the first frame update
    void Start()
    {
        this.inside = false;
    }

    // Update is called once per frame
    void Update()
    {
        /*if(this.inside){
        	Debug.Log("ahhh I'm burning");
        }*/
    }
    
    private void onTriggerEnter(Collider obj){
    	if(obj.gameObject.CompareTag("Player")){
    		this.inside = true;
    		Debug.Log("I'm burning");
    	}
    }
    
    private void onTriggerExit(Collider obj){
    	if(obj.gameObject.CompareTag("Player")){
    		this.inside = false;
    		Debug.Log("Left");
    	}
    }
}
