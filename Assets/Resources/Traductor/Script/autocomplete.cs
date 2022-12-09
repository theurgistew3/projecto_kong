using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Mono.Data.Sqlite;

public class autocomplete : MonoBehaviour
{

    public Text text;

    string dburl;
    IDbConnection connection;
    IDbCommand command;
    IDataReader reader;
    string databasename = "Base_de_Datos.db";
    string comando = "SELECT letra FROM Alfabeto";

    void Start()
    {
        string filepath = Application.dataPath + "/Resources/Traductor/Data_Base/" + databasename;

        dburl = "URI=file:" + filepath;
        var dropdown = transform.GetComponent<Dropdown>();

        dropdown.options.Clear();
        string palabra;
        Debug.Log("Conexion establecida" + dburl);
        connection = new SqliteConnection(dburl);
        connection.Open();

        reader_funcion(dropdown);
    }


    private void reader_funcion(Dropdown dropdown)
    {
        string palabra;
        using (connection = new SqliteConnection(dburl))
        {
            connection.Open();
            command = connection.CreateCommand();
            command.CommandText = comando;
            reader = command.ExecuteReader();
            while(reader.Read())
            {
                palabra = reader.GetString(0);

                dropdown.options.Add(new Dropdown.OptionData() { text = palabra });
            }
        }
        connection.Close();
    }

    void DropdownItemSelected(Dropdown dropdown)
    {
        int index = dropdown.value;

        text.text = dropdown.options[index].text;
    }

    
}