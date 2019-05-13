using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarraScript : MonoBehaviour {

    private float velocidad;
	// Use this for initialization
	void Start () {
        velocidad = 20;
    }
	
	// Update is called once per frame
	void Update () {

        float axis = Input.GetAxis("Barra");
        var Move = axis * Time.deltaTime;

        float tamx = (float)( 4.85 * transform.localScale.x );
        if (transform.position.x >= MainController.TopRight.x - tamx/2 && Move > 0)
        {
            Move = 0;
        }
        if (transform.position.x <= MainController.BottomLeft.x + tamx/2 && Move < 0)
        {
            Move = 0;
        }

        transform.Translate(Move * new Vector2(1, 0) * velocidad);
    }
}
