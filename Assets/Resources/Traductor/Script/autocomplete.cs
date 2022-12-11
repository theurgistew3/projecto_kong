using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Mono.Data.Sqlite;

public class autocomplete : chibi.Chibi_behaviour
{

    public Text text;
    public Dropdown dropdown;

    string dburl;
    IDbConnection connection;
    IDbCommand command;
    IDataReader reader;
    string databasename = "Image_Data.db";
    string comando = "SELECT * FROM palabra WHERE id < 29";
    public List<struct_animation> guardar = new List<struct_animation>();
    public List<struct_animation> palabras = new List<struct_animation>();
    public List<string> dato = new List<string>();
    protected override void _init_cache()
    {
    
        base._init_cache();
        guardar.Clear();
        dato.Clear();
        string filepath = Application.dataPath + "/Resources/Traductor/Data_Base/" + databasename;

        dburl = "URI=file:" + filepath;
        dropdown.options.Clear();
        Debug.Log("Conexion establecida" + dburl);
        connection = new SqliteConnection(dburl);
        connection.Open();

        reader_funcion(dropdown);

        dropdown.onValueChanged.AddListener(delegate { DropdownItemSelected(dropdown); });
    }


    private void reader_funcion(Dropdown dropdown)
    {
        

        using (connection = new SqliteConnection(dburl))
        {
            connection.Open();
            command = connection.CreateCommand();
            command.CommandText = comando;
            reader = command.ExecuteReader();
            int n = 0;
            int i = 51;
            
            struct_animation[] obj = new struct_animation[i];
            while (reader.Read())
            {
                string palabra = reader.GetString(0);
                int id = reader.GetInt32(1);
                obj[n] = new struct_animation(palabra, id);
                palabras.Add(obj[n]);
                n++;
            }
            Debug.Log(palabras.Count);
            foreach(var palabra in palabras )
            {
                Debug.Log("palabra= " + palabra.name);
                dropdown.options.Add(new Dropdown.OptionData() { text = palabra.name });
            }

            
            
        }
        connection.Close();
    }


    
    
    
    
    void DropdownItemSelected(Dropdown dropdown)
    {
        int index = dropdown.value;
        Debug.Log(palabras[index]);

        guardar.Add(palabras[index]);
        dato.Add(dropdown.options[index].text);
        Debug.Log("Se agrego= " + dropdown.options[index].text);
        text.text = string.Join(" ", dato);
    }

    
    

    public void delete()
    {
        Debug.Log("Se elimino registro");
        guardar.Clear();
    }
    
}