using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour {

    // Use this for initialization
    public bool isInBarra;
    public float velocidad;
    public Vector2 direccion;
    private Vector2 staticDirection;
    private float delta = (float)0.005;
    private float aumento = (float)0.05;
    private int wallBounce;
    public bool Destroyer;

    IEnumerator playSound(string str)
    {
        AudioSource src = GetComponent<AudioSource>();
        src.clip = Resources.Load<AudioClip>(str);
        src.Play();
        yield return new WaitForSeconds(src.clip.length);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        //gameobject == ball
        //collision.gameobject == brick
        if(collision.gameObject.name == "Brick(Clone)" && Destroyer)
        {
            Debug.Log("Ignorar");
            return;
        }

        if(collision.gameObject.name!="Brick(Clone)" && collision.gameObject.name != "Barra")
        {
            Debug.Log("Ignorar");
            return;
        }
        AudioSource src = GetComponent<AudioSource>();
        if(collision.gameObject.name == "Brick(Clone)")
        {
            StartCoroutine(playSound("Sound/Brick sound"));
        }
        else
        {
            StartCoroutine(playSound("Sound/Barra sound"));
        }
       

        float xbrick = collision.gameObject.transform.position.x;
        float ybrick = collision.gameObject.transform.position.y;
        float largo = (float)(1.28 * collision.gameObject.transform.localScale.y);
        float ancho = (float)(3.84 * collision.gameObject.transform.localScale.x);

        float tamBall = (float)(1.28*gameObject.transform.localScale.x);
        float xball = gameObject.transform.position.x;
        float yball = gameObject.transform.position.y;


        Vector2 BottomLeft = new Vector2((float) (xbrick - ancho / 2), (float) (ybrick - largo / 2) );
        Vector2 TopRight = new Vector2((float) (xbrick + ancho / 2), (float)(ybrick + largo / 2) );
        /*
        Debug.Log("largo: "+largo.ToString());
        Debug.Log("Ancho: " + ancho.ToString());
        Debug.Log("diametro: " + tamBall);
        Debug.Log("BotLeft: " + BottomLeft.x + " " + BottomLeft.y);
        Debug.Log("TopRig: " + TopRight.x + " " + TopRight.y);

        Debug.Log("Brick: " + xbrick.ToString() + " | " + ybrick);
        Debug.Log("Ball: " + (xball).ToString() + " | " + (yball).ToString() );

        Debug.Log("1:" + Mathf.Abs(yball + tamBall / 2 - BottomLeft.y));
        Debug.Log("2:" + Mathf.Abs(yball + tamBall / 2 - TopRight.y));
        Debug.Log("3:" + Mathf.Abs(xball + tamBall / 2 - BottomLeft.x));
        Debug.Log("4:" + Mathf.Abs(xball + tamBall / 2 - TopRight.x));*/
        
        //desde abajo
            if (Mathf.Abs(yball + tamBall / 2 - BottomLeft.y) <= delta)
            {
                Debug.Log(1);
                direccion.y = -staticDirection.y;
            }
            //desde arriba
            else if (Mathf.Abs(yball - tamBall / 2 - TopRight.y) <= delta)
            {
                Debug.Log(2);
                direccion.y = staticDirection.y;
            }
            //desde la izquierda
            else if (Mathf.Abs(xball + tamBall / 2 - BottomLeft.x) <= delta)
            {
                Debug.Log(3);
                direccion.x = -staticDirection.x;
            }
            //desde la derecha
            else if (Mathf.Abs(xball - tamBall / 2 - TopRight.x) <= delta)
            {
                Debug.Log(4);
                direccion.x = staticDirection.x;
            }
            else
            {
                Debug.Log("wrong");
                direccion.x *= -1;
                direccion.y *= -1;
            }
        
        

        
    }

    void Start () {
        velocidad = 20;
        isInBarra = true;
        staticDirection = new Vector2((float)0.5, (float)0.5);
        direccion = new Vector2((float)0.5 ,(float) 0.5);
        wallBounce = 0;
	}
	
	// Update is called once per frame
	void Update () {

        float axis = Input.GetAxis("Barra");
        var Move = axis * Time.deltaTime;
        float tamx;
        if(transform.position.y<= -4.3)
        {

            Destroy(gameObject);
            if(MainController.SecondaryBalls.Count==0)
            {
                GameObject[] powers = GameObject.FindGameObjectsWithTag("PowerUp");
                foreach (GameObject g in powers)
                {
                    Destroy(g);
                }
            }
           
        }
        if (isInBarra)
        {
            

            tamx = (float)(4.85 * transform.localScale.x);
            if (transform.position.x >= MainController.TopRight.x - tamx / 2 && Move > 0)
            {
                Move = 0;
            }
            if (transform.position.x <= MainController.BottomLeft.x + tamx / 2 && Move < 0)
            {
                Move = 0;
            }

            transform.Translate(Move * new Vector2(1, 0) * 20);
        }
        else
        {
            tamx = (float)(1.28 * transform.localScale.x);
            bool f = false;
            if (transform.position.x >= MainController.TopRight.x - tamx / 2)
            {
                direccion.x = -staticDirection.x;
                f = true;
            }
            if (transform.position.x <= MainController.BottomLeft.x + tamx / 2)
            {
                direccion.x = staticDirection.x;
                f = true;
            }
            if (transform.position.y >= MainController.TopRight.y - tamx / 2)
            {
                direccion.y = -staticDirection.y;
                f = true;
            }
            if (transform.position.y <= MainController.BottomLeft.y + tamx / 2)
            {
                direccion.y = staticDirection.y;
                f = true;
            }
            if(f)
            {
                wallBounce++;
                StartCoroutine(playSound("Sound/Wall sound"));
            }
            // con esto se deberia cambiar el angulo cada 5 choques en la pared
            // se desconoce el funcionamiento cuando x llegue a 0 e y siga aumentando
            if(wallBounce==5)
            {
                staticDirection.y += aumento;
                // se despeja la direccion de x de la formula de la hipotenusa,
                // originalmente se tenia que x e y eran 0.5 por lo tanto se tiene que 0.25+0.25 = h^2
                // asi que 0.5 = x^2+y^2
                staticDirection.x = Mathf.Sqrt((float) 0.5- staticDirection.y* staticDirection.y);
                
                wallBounce = 0;
            }
            transform.Translate( direccion * velocidad);
        }
	}
}
