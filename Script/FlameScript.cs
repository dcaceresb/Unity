using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
        
        Vector3 vec = MainController.MainBall.transform.position;
        vec.z = 0;
        transform.position = vec;
	}
}
