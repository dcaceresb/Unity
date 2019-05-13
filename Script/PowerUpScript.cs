using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpScript : MonoBehaviour {

    // Use this for initialization
    public float x, y;
    private float velocidad;
    public PowerEnum type;
    public SpriteRenderer render;

    private void sumarPuntos(int puntos)
    {
        MainController.puntuacion += puntos;
    }

    private IEnumerator Sumar(int sum)
    {
        MainController.puntuacion += sum;
        yield return null;
        Destroy(gameObject);
    }
    private IEnumerator Shoot()
    {
        Animator anim = MainController.Barra.GetComponent<Animator>();
        anim.runtimeAnimatorController = (RuntimeAnimatorController)Resources.Load("Animation/ShootStyle", typeof(RuntimeAnimatorController ));
        MainController.shootEnabled = true;
        yield return new WaitForSeconds(8);
        MainController.shootEnabled = false;
        anim.runtimeAnimatorController = (RuntimeAnimatorController)Resources.Load("Animation/Barra", typeof(RuntimeAnimatorController));
        Destroy(gameObject);
    }
    private IEnumerator Escalar(bool alargar)
    {
        Vector3 escala = MainController.Barra.transform.localScale;
        float n;
        if (alargar)
        {
            n = escala.x * 2;
            for (float i = escala.x; i < n; i += (float)0.01)
            {
                escala.x = i;
                MainController.Barra.transform.localScale = escala;
                yield return new WaitForSeconds((float)0.001);
            }
            yield return new WaitForSeconds(5);
            for (float i = escala.x; i > n / 2; i -= (float)0.01)
            {
                escala.x = i;
                MainController.Barra.transform.localScale = escala;
                yield return new WaitForSeconds((float)0.001);
            }
        }
        else
        {
            n = escala.x / 2;
            for (float i = escala.x; i > n; i -= (float)0.01)
            {
                escala.x = i;
                MainController.Barra.transform.localScale = escala;
                yield return new WaitForSeconds((float)0.001);
            }
            yield return new WaitForSeconds(5);
            for (float i = escala.x; i < n * 2; i += (float)0.01)
            {
                escala.x = i;
                MainController.Barra.transform.localScale = escala;
                yield return new WaitForSeconds((float)0.001);
            }
        }
        MainController.Barra.transform.localScale = escala;
        Destroy(gameObject);
    }

    private IEnumerator Velocidad(bool aumentar)
    {
        if(aumentar)
        {
            MainController.MainBall.GetComponent<BallScript>().velocidad *= 2;
            yield return new WaitForSeconds(5);
            MainController.MainBall.GetComponent<BallScript>().velocidad /= 2;
        }
        else
        {
            MainController.MainBall.GetComponent<BallScript>().velocidad /= 2;
            yield return new WaitForSeconds(5);
            MainController.MainBall.GetComponent<BallScript>().velocidad *= 2;
        }
        
        Destroy(gameObject);
    }
    IEnumerator Destroyer()
    {
        var flame = Instantiate(Resources.Load("Flame")) as GameObject;
        MainController.MainBall.GetComponent<BallScript>().Destroyer = true;
        yield return new WaitForSeconds(5);
        MainController.MainBall.GetComponent<BallScript>().Destroyer = false;
        Destroy(flame);
    }
    IEnumerator Tri()
    {
        MainController.ballAdded = true;
        yield return null;
        Destroy(gameObject);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {


        if (collision.gameObject.name == "Barra")
        {

            if (type == PowerEnum.Plus50)
            {
                StartCoroutine(Sumar(50) );
            }
            if (type == PowerEnum.Plus100)
            {
                StartCoroutine(Sumar(100));
            }
            if (type == PowerEnum.Plus250)
            {
                StartCoroutine(Sumar(250));
            }
            if (type == PowerEnum.Plus500)
            {
                StartCoroutine(Sumar(500));
            }
            if (type == PowerEnum.Slow)
            {
                StartCoroutine(Velocidad(false));
            }
            if (type == PowerEnum.Fast)
            {
                StartCoroutine(Velocidad(true));
            }
            if (type == PowerEnum.Tri)
            {
                StartCoroutine(Tri());
            }
            if (type == PowerEnum.Romper)
            {
                StartCoroutine(Destroyer());
            }
            if (type == PowerEnum.Encojer)
            {
                StartCoroutine(Escalar(false));

            }
            if (type == PowerEnum.Alargar)
            {
                StartCoroutine(Escalar(true));
            }
            if (type == PowerEnum.Disparar)
            {
                StartCoroutine(Shoot());
            }
            if(type == PowerEnum.Star)
            {

                    if (MainController.world == 1)
                    {
                        MainController.mapa2();
                        MainController.world = 2;
                    }
                    else
                    {
                        MainController.mapa3();
                        MainController.world = 3;
                    }
                
                Destroy(gameObject);
            }
            gameObject.transform.position = new Vector3(0, 1000, 0);
            //gameObject.SetActive(false);
            
        }

    }
    void Start () {

        transform.position = new Vector3(x, y, 0);

		if(type == PowerEnum.Plus50)
        {
            render.sprite = Resources.Load<Sprite>("Sprites/31-Breakout-Tiles");
        }
        if (type == PowerEnum.Plus100)
        {
            render.sprite = Resources.Load<Sprite>("Sprites/34-Breakout-Tiles");
        }
        if (type == PowerEnum.Plus250)
        {
            render.sprite = Resources.Load<Sprite>("Sprites/39-Breakout-Tiles");
        }
        if (type == PowerEnum.Plus500)
        {
            render.sprite = Resources.Load<Sprite>("Sprites/40-Breakout-Tiles");
        }
        if (type == PowerEnum.Slow)
        {
            render.sprite = Resources.Load<Sprite>("Sprites/41-Breakout-Tiles");
        }
        if (type == PowerEnum.Fast)
        {
            render.sprite = Resources.Load<Sprite>("Sprites/42-Breakout-Tiles");
        }
        if (type == PowerEnum.Tri)
        {
            render.sprite = Resources.Load<Sprite>("Sprites/43-Breakout-Tiles");
        }
        if (type == PowerEnum.Romper)
        {
            render.sprite = Resources.Load<Sprite>("Sprites/44-Breakout-Tiles");
        }
        if (type == PowerEnum.Encojer)
        {
            render.sprite = Resources.Load<Sprite>("Sprites/46-Breakout-Tiles");
        }
        if (type == PowerEnum.Alargar)
        {
            render.sprite = Resources.Load<Sprite>("Sprites/47-Breakout-Tiles");
        }
        if (type == PowerEnum.Disparar)
        {
            render.sprite = Resources.Load<Sprite>("Sprites/48-Breakout-Tiles");
        }
    }
	
	// Update is called once per frame
	void Update () {
        float Move = 1;
        float tamy = (float) (1.28 * transform.localScale.y);
        if (transform.position.y + tamy  <= -4  )
        {
            Move = 0;
            Destroy(gameObject);
        }

        transform.Translate(Move * new Vector2(0, (float)-0.02));
    }
}
