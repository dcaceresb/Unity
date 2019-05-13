using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.position.y>=5)
        {
            Destroy(gameObject);
        }
        else
        {
            transform.Translate(new Vector2(0, (float)0.1));
        }
	}
}
