using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations;
using System;



[Serializable]
public class struct_animation
{
    public string name;
    public int id;

    public struct_animation(string _name, int _id)
    {
        name = _name;
        id = _id;
    }
}

public class Reproducir : chibi.Chibi_behaviour
{

    public GameObject botonplay;
    public Slider slider;
    public Text text;
    public chibi.pomodoro.Pomodoro_obj timer;
    protected struct_animation _current;
    public traductor.animator.Animator_npc_sordos animator;
    
    public List<struct_animation> lista = new List<struct_animation>();
    //abcmnxyz
    //A y M

    public struct_animation current
    {
        get
        {
            return _current;
        }
        set
        {
            _current = value;
            timer.reset();
            text.text = current.name;
            animator.letra = current.id;
        }

    }

    

    protected override void _init_cache()
    {
        
        base._init_cache();
        

        //reader_funcion();

        
        timer = new chibi.pomodoro.Pomodoro_obj(5.0f);
        timer.is_enable = false;
        //Cambiartext(lista);
    }


    int posicion = 0;
    

    // Update is called once per frame
    protected virtual void Update()
    {
        Debug.Log("posicion: " + posicion);
        int tamaño = lista.Count;
        bool finish = false;
        if (tamaño == posicion)
        {
            finish = true;
        }

        
        if(finish)
        {
            Debug.Log("fin de la animacion");
        }
        else
        {
            slider.value = timer.normalize_time;
            if (timer.tick())
            {
                
                debug.log("name: {0} - id:{1}", current.name, current.id);
                posicion++;
                current = lista[posicion];         
                

            }
        }

        

    }

    

    public void on_play()
    {
        Debug.Log("Se pusho play");
        timer.is_enable = true;
        current = lista[posicion];
    }

    public void on_forward()
    {
        Debug.Log("Se pusho adelantar");
        if (posicion > lista.Count)
        {
            Debug.Log("Fin de la lista");
        }
        else
        {
            posicion++;
            current = lista[posicion];
        }
    }

    public void on_back()
    {
        Debug.Log("Se pusho atrasar");
        if(posicion < 0)
        {
            Debug.Log("Inicio de la lista");
        }
        else
        {
            posicion--;
            current = lista[posicion];
        }
        
    }

    public void on_stop()
    {
        timer.is_enable = false;
        Debug.Log("Se pusho pausar");
    }
}
