using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BrickScript : MonoBehaviour {

    // Use this for initialization
    public BlockEnum type;
    public SpriteRenderer render;
    public int x, y;
    public int aguante;
    private int puntos;

    private int SelectPower()
    {
        ///la idea es tener con probabilidad la creacion de los power up
        /// y que cada power up tenga mas probabilidad que otro, por ejemplo el +50 que tenga un 20% +100 10%...
        /// para eso se utilizará un arreglo con numeros del 0 a n (cantidad de enum) y que la cantidad de aparicion
        /// sea en relacion a su probabilidad, si +50 tiene 20% entonces el 20% de los items son 1 (que representan al +50)
        /// luego solo se obtiene un indice random para obtener la opcion

        ///50% nada 
        double[] probs = { 0.75, 0.05, 0.04, 0.03, 0.01, 0.02, 0.02, 0.02, 0.01, 0.02, 0.02, 0.01 };
        int n = 10000;
        int[] array = new int[n];
        int i = 0;
        for (int k = 0; k < probs.Length;k++)
        {
            for(int j = 0 ; j < probs[k]*n ; j++)
            {
                array[i] = k;
                i++;
                // por si las moscas
                if (i == n)
                {
                    break;
                }

            }
        }

        System.Random rnd = new System.Random();
        int index = rnd.Next(0, n);

        return array[index];

    }
    private void crearPower(int tipo)
    {
        GameObject power = Instantiate(Resources.Load("PowerUp")) as GameObject;
        power.name = "PowerUp";
        power.GetComponent<PowerUpScript>().type = (PowerEnum)tipo;
        power.GetComponent<PowerUpScript>().x = gameObject.transform.position.x;
        power.GetComponent<PowerUpScript>().y = gameObject.transform.position.y;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Ball" || collision.gameObject.name == "Bullet")
        {
            if(collision.gameObject.name == "Bullet")
            {
                Destroy(collision.gameObject);
            }
            
            aguante--;
            if (collision.gameObject.name == "Ball")
            {
               if(collision.gameObject.GetComponent<BallScript>().Destroyer)
               {
                    aguante -= 6;
               }
            }
            if (aguante<=0)
            {
                int index = SelectPower();
                if(index>0)
                {
                    crearPower(index);
                }
                MainController.puntuacion += puntos;
                MainController.nbricks--;
                if(MainController.nbricks==0)
                {
                    if(MainController.world==1)
                    {
                        MainController.mapa2();
                        MainController.world = 2;
                    }
                    else
                    {
                        MainController.mapa3();
                        MainController.world = 3;
                    }
                }
                Destroy(gameObject);
            }
            else
            {
                if (type == BlockEnum.Azul)
                {
                    render.sprite = Resources.Load<Sprite>("Sprites/02-Breakout-Tiles");
                }
                if (type == BlockEnum.Amarillo)
                {
                    render.sprite = Resources.Load<Sprite>("Sprites/14-Breakout-Tiles");
                }
                if (type == BlockEnum.Cafe)
                {
                    render.sprite = Resources.Load<Sprite>("Sprites/20-Breakout-Tiles");
                }
                if (type == BlockEnum.Celeste)
                {
                    render.sprite = Resources.Load<Sprite>("Sprites/12-Breakout-Tiles");
                }
                if (type == BlockEnum.Gris)
                {
                    aguante = int.MaxValue;
                    render.sprite = Resources.Load<Sprite>("Sprites/18-Breakout-Tiles");
                }
                if (type == BlockEnum.Marron)
                {
                    render.sprite = Resources.Load<Sprite>("Sprites/10-Breakout-Tiles");
                }
                if (type == BlockEnum.Morado)
                {
                    render.sprite = Resources.Load<Sprite>("Sprites/06-Breakout-Tiles");
                }
                if (type == BlockEnum.Rojo)
                {
                    render.sprite = Resources.Load<Sprite>("Sprites/08-Breakout-Tiles");
                }
                if (type == BlockEnum.Verde_claro)
                {
                    render.sprite = Resources.Load<Sprite>("Sprites/12-Breakout-Tiles");
                }
                if (type == BlockEnum.Verde_oscuro)
                {
                    render.sprite = Resources.Load<Sprite>("Sprites/16-Breakout-Tiles");
                }
            }
        }
    }

    void Start () {
        
        int nBlocks = 16;
        float escala = (float) (nBlocks / (3.84 * 21));
        float tamx = (float) (escala * 3.84);
        float tamy = (float)(escala * 1.28);
        transform.localScale = new Vector3(escala, escala, escala);
        float inix = -8 - tamx / 2;
        float iniy = 5 - tamy / 2;
        float posx = (float) (inix + tamx * (x + 1) );
        float posy = (float) (iniy - tamy * y);
        transform.position = new Vector2(posx , posy);

        if (type == BlockEnum.Azul)
        {
            puntos = 3;
            aguante = 2;
            render.sprite = Resources.Load<Sprite>("Sprites/01-Breakout-Tiles");
        }
        if (type == BlockEnum.Amarillo)
        {
            puntos = 5;
            aguante = 3;
            render.sprite = Resources.Load<Sprite>("Sprites/13-Breakout-Tiles");
        }
        if (type == BlockEnum.Cafe)
        {
            puntos = 20;
            aguante = 6;
            render.sprite = Resources.Load<Sprite>("Sprites/19-Breakout-Tiles");
        }
        if (type == BlockEnum.Celeste)
        {
            puntos = 1;
            aguante = 1;
            render.sprite = Resources.Load<Sprite>("Sprites/11-Breakout-Tiles");
        }
        if (type == BlockEnum.Gris)
        {
            aguante = int.MaxValue;
            render.sprite = Resources.Load<Sprite>("Sprites/17-Breakout-Tiles");
        }
        if (type == BlockEnum.Marron)
        {
            puntos = 10;
            aguante = 5;
            render.sprite = Resources.Load<Sprite>("Sprites/09-Breakout-Tiles");
        }
        if (type == BlockEnum.Morado)
        {
            puntos = 8;
            aguante = 4;
            render.sprite = Resources.Load<Sprite>("Sprites/05-Breakout-Tiles");
        }
        if (type == BlockEnum.Rojo)
        {
            puntos = 8;
            aguante = 4;
            render.sprite = Resources.Load<Sprite>("Sprites/07-Breakout-Tiles");
        }
        if (type == BlockEnum.Verde_claro)
        {
            puntos = 5;
            aguante = 3;
            render.sprite = Resources.Load<Sprite>("Sprites/11-Breakout-Tiles");
        }
        if (type == BlockEnum.Verde_oscuro)
        {
            puntos = 10;
            aguante = 5;
            render.sprite = Resources.Load<Sprite>("Sprites/15-Breakout-Tiles");
        }

    }
	
	// Update is called once per frame
	void Update () {


        



    }
}
