using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainController : MonoBehaviour {

    // Use this for initialization
    public static int nbricks,world;
    public static GameObject [,] sprite;
    public static Vector2 BottomLeft;
    public static Vector2 TopRight;
    public static GameObject Barra, MainBall;
    public static List<GameObject> SecondaryBalls;
    public Text puntos, lifes;
    public static int vidas;
    public static int puntuacion;
    public static bool shootEnabled;
    public static bool ballAdded;
    private bool startShooting;
    private string[] cheatCode;
    private int index;

    private static void createStar()
    {
        System.Random rnd = new System.Random();
        int index = rnd.Next(1, 100);
        if(index<=10 )
        {
            GameObject star = Instantiate(Resources.Load("Star")) as GameObject;
            star.transform.position = new Vector3(0, 5, 0);
        }
    }
    private static void deletera()
    {
        createStar();
        foreach (GameObject g in sprite)
        {
            if(g!=null)
                Destroy(g);
        }
    }
    public static void mapa1()
    {
        deletera();
        nbricks = 0;
        for (int i = 0; i < 15; i+=2)
        {
            for (int j = 0; j < 21; j++)
            {
                nbricks++;
                sprite[i, j] = Instantiate(Resources.Load("Brick")) as GameObject;

                if (i == 0)
                {
                    sprite[i, j].GetComponent<BrickScript>().type = BlockEnum.Cafe;
                }
                else if (i == 2)
                {
                    sprite[i, j].GetComponent<BrickScript>().type = BlockEnum.Verde_oscuro;
                }
                else if(i == 4)
                {
                    sprite[i, j].GetComponent<BrickScript>().type = BlockEnum.Morado;
                }
                else if(i == 6)
                {
                    sprite[i, j].GetComponent<BrickScript>().type = BlockEnum.Rojo;
                }
                else if(i == 8)
                {
                    sprite[i, j].GetComponent<BrickScript>().type = BlockEnum.Azul;
                }
                else
                {
                    sprite[i, j].GetComponent<BrickScript>().type = BlockEnum.Celeste;
                }
                sprite[i, j].GetComponent<BrickScript>().x = j;
                sprite[i, j].GetComponent<BrickScript>().y = i;
                

            }
        }
    }

    public static void mapa2()
    {
        deletera();
        nbricks = 0;
        for (int i = 1; i < 15; i++)
        {
            for (int j = 0; j < 21; j++)
            {
                if (j % 4 != 0)
                {
                    nbricks++;
                    sprite[i, j] = Instantiate(Resources.Load("Brick")) as GameObject;

                    if (i % 2 == 0)
                    {
                        sprite[i, j].GetComponent<BrickScript>().type = BlockEnum.Azul;
                    }
                    else if (i % 3 == 0)
                    {
                        sprite[i, j].GetComponent<BrickScript>().type = BlockEnum.Verde_oscuro;
                    }
                    else
                    {
                        sprite[i, j].GetComponent<BrickScript>().type = BlockEnum.Celeste;
                    }
                    sprite[i, j].GetComponent<BrickScript>().x = j;
                    sprite[i, j].GetComponent<BrickScript>().y = i;
                }

            }
        }
    }

    public static void mapa3()
    {
        deletera();
        nbricks = 0;
        for (int i = 0; i < 15; i ++)
        {
            for (int j = 1; j < 20; j++)
            {
                nbricks++;
                sprite[i, j] = Instantiate(Resources.Load("Brick")) as GameObject;
                if (i == 0)
                {
                    sprite[i, j].GetComponent<BrickScript>().type = BlockEnum.Cafe;
                }
                else if (j == 1 || j==19 )
                {
                    sprite[i, j].GetComponent<BrickScript>().type = BlockEnum.Gris;
              
                    
                }
                else if(i<3)
                {
                    sprite[i, j].GetComponent<BrickScript>().type = BlockEnum.Morado;
                }
                else if(i<7)
                {
                    sprite[i, j].GetComponent<BrickScript>().type = BlockEnum.Rojo;
                }
                else
                {
                    sprite[i, j].GetComponent<BrickScript>().type = BlockEnum.Celeste;
                }

                sprite[i, j].GetComponent<BrickScript>().x = j;
                sprite[i, j].GetComponent<BrickScript>().y = i;


            }
        }
    }

    void Awake() {

        tiempo = (float)0.5;
        cheatCode = new string[] { "h", "e", "s", "o", "y", "a", "m" };
        index = 0;

        ballAdded = false;
        world = 1;
        vidas = 3;
        SecondaryBalls = new List<GameObject>();
        shootEnabled = false;
        startShooting = false;
        BottomLeft = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));
        TopRight = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));


        sprite = new GameObject[15, 21];
        mapa1();

        Barra = Instantiate(Resources.Load("Barra")) as GameObject;
        Barra.name = "Barra";
        MainBall = Instantiate(Resources.Load("Ball")) as GameObject;
        MainBall.name = "Ball";
        

    }

    private IEnumerator AddBall()
    {
        Vector2 direccion = MainBall.GetComponent<BallScript>().direccion;
        //se calcula el angulo de 22.5 grados en radianes, ya que la bola se desplaza en un angulo de 45°
        float angulo = (float)(22.5 / 180 * Mathf.PI);
        // ahora la hipotenusa del triangulo formado por la direccion
        float h = Mathf.Sqrt(direccion.x * direccion.x + direccion.y * direccion.y);
        // se tiene a mano coseno y seno de 22.5°
        float cos = Mathf.Cos(angulo);
        float sen = Mathf.Sin(angulo);
        // se calcula h' que es la hipotenusa del triangulo de 22.5 
        float h2 = cos / h;
        // ahora falta calcular x e y para obtener las direcciones;
        float x = h2 * cos;
        float y = h2 * sen;
        // ahora tan solo falta ver a que cuadrante pertenece la direccion
        Vector2 dir1, dir2;
        // primer cuadrante
        if (direccion.x > 0 && direccion.y > 0)
        {
            dir1 = new Vector2(x, y);
            dir2 = new Vector2(y, x);
        }
        //segundo cuadrante
        else if (direccion.x < 0 && direccion.y > 0)
        {
            dir1 = new Vector2(-x, y);
            dir2 = new Vector2(-y, x);
        }
        // tercer cuadrante
        else if (direccion.x < 0 && direccion.y < 0)
        {
            dir1 = new Vector2(-x, -y);
            dir2 = new Vector2(-y, -x);
        }
        // cuarto cuadrante 
        else
        {
            dir1 = new Vector2(x, -y);
            dir2 = new Vector2(y, -x);
        }
        Debug.Log(x);
        Debug.Log(y);

        GameObject Ball1 = Instantiate(Resources.Load("Ball")) as GameObject;
        GameObject Ball2 = Instantiate(Resources.Load("Ball")) as GameObject;
        SecondaryBalls.Add(Ball1);
        SecondaryBalls.Add(Ball2);

        yield return new WaitForEndOfFrame();

        Ball1.name = "Ball";
        Ball1.GetComponent<BallScript>().direccion = dir1;
        Ball1.GetComponent<BallScript>().isInBarra = false;
        Ball1.transform.position = MainBall.transform.position;
        Ball1.GetComponent<BallScript>().velocidad = (float)0.1;

        Ball2.name = "Ball";
        Ball2.GetComponent<BallScript>().direccion = dir2;
        Ball2.GetComponent<BallScript>().isInBarra = false;
        Ball2.transform.position = MainBall.transform.position;
        Ball2.GetComponent<BallScript>().velocidad = (float)0.1;


    }

    private float tiempo; 
	IEnumerator Shoot()
    {
        startShooting = true;
        bool waitea = false;
        while(shootEnabled && Input.GetKey("c") && !waitea)
        {
            waitea = true;
            float posx = Barra.transform.position.x;
            float posy = Barra.transform.position.y;
            float tamx = (float)(4.85 * Barra.transform.localScale.x / 2);
            float tamy = (float)(1.28 * Barra.transform.localScale.y / 2);
            posy += tamy;

            var bullet1 = Instantiate(Resources.Load("Bullet")) as GameObject;
            bullet1.transform.position = new Vector3(posx + tamx, posy, 0);
            bullet1.name = "Bullet";

            var bullet2 = Instantiate(Resources.Load("Bullet")) as GameObject;
            bullet2.transform.position = new Vector3(posx - tamx, posy, 0);
            bullet2.name = "Bullet";

            yield return new WaitForSeconds(tiempo);
            waitea = false;

        }
        startShooting = false;
    }
    // Update is called once per frame


    void Update () {


        if (Input.anyKeyDown && !Input.GetKey("c") )
        {
            // Check if the next key in the code is pressed
            if (Input.GetKeyDown(cheatCode[index]))
            {
                // Add 1 to index to check the next key in the code
                index++;
            }
            // Wrong key entered, we reset code typing
            else
            {
                index = 0;
            }
        }

        // If index reaches the length of the cheatCode string, 
        // the entire code was correctly entered
        if (index == cheatCode.Length)
        {
            // Cheat code successfully inputted!
            // Unlock crazy cheat code stuff
            Animator anim = Barra.GetComponent<Animator>();
            anim.runtimeAnimatorController = (RuntimeAnimatorController)Resources.Load("Animation/ShootStyle", typeof(RuntimeAnimatorController));
            shootEnabled = true;
            tiempo = (float)0.05;
            StartCoroutine(Shoot());
            index = 0;
        }

        if (Input.GetKey("space"))
        {
            MainBall.GetComponent<BallScript>().isInBarra = false;
            MainBall.GetComponent<BallScript>().velocidad = (float)0.1;
        }
        puntos.text = "Puntuacion: " + puntuacion;
        lifes.text = "Vidas: " + vidas;

        if (shootEnabled && !startShooting)
        {
            StartCoroutine(Shoot());
        }

        if(ballAdded)
        {
            StartCoroutine(AddBall());
            ballAdded = false;
        }

        if(MainBall==null)
        {
            GameObject flame = GameObject.FindGameObjectWithTag("Particulas");
            if (flame != null)
            {
                Destroy(flame);
            }

            if (SecondaryBalls.Count!=0)
            {
                MainBall = SecondaryBalls[0];
                SecondaryBalls.RemoveAt(0);
            }
            else
            {
                vidas--;
                Destroy(Barra);

                Barra = Instantiate(Resources.Load("Barra")) as GameObject;
                Barra.name = "Barra";
                MainBall = Instantiate(Resources.Load("Ball")) as GameObject;
                MainBall.name = "Ball";
                
                if(vidas==0)
                {
                    mapa1();
                    world = 1;
                    vidas = 3;
                    puntuacion = 0;
                }
               
            }
        }

    }
}
