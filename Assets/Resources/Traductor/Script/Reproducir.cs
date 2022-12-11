using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations;
using UnityEditor.Animations;
using System;
using System.Data;
using Mono.Data.Sqlite;



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
    public bool play;
    public bool termino;
    public chibi.pomodoro.Pomodoro_obj timer;
    protected struct_animation _current;
    public traductor.animator.Animator_npc_sordos animator;
    string dburl;
    IDbConnection connection;
    IDbCommand command;
    IDataReader reader;
    string databasename = "Image_Data.db";
    string comando = "SELECT letra, idAnim FROM Alfabeto WHERE letra = 'A' or letra = 'B' or letra  = 'C' or letra = 'M' or letra = 'N' or letra = 'X' or letra = 'Y'";
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

    List<struct_animation> lista = new List<struct_animation>();

    protected override void _init_cache()
    {
        
        base._init_cache();

        string filepath = Application.dataPath + "/Resources/Traductor/Data_Base/" + databasename;
        dburl = "URI=file:" + filepath;

        Debug.Log("Conexion establecida" + dburl);
        connection = new SqliteConnection(dburl);
        connection.Open();

        

        reader_funcion();

        
        timer = new chibi.pomodoro.Pomodoro_obj(5.0f);
        timer.is_enable = false;
        //Cambiartext(lista);
    }

    private void reader_funcion()
    {
        int i = 7;
        int n = 0;
        struct_animation[] obj = new struct_animation[i];
        using (connection = new SqliteConnection(dburl))
        {
            connection.Open();
            command = connection.CreateCommand();
            command.CommandText = comando;
            reader = command.ExecuteReader();
            if(n==i)
            {
                Debug.Log("Lista llena");
            }
            else
            {
                while (reader.Read())
                {
                    string letra = reader.GetString(0);
                    int ID = reader.GetInt32(1);

                    Debug.Log("letra= " + letra + " ID= " + ID);
                    
                    obj[n] = new struct_animation(letra, ID);
                    lista.Add(obj[n]);
                    n++;
                }
            }

            connection.Close();
        }
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
