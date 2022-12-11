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
    string databasename = "Image_Data.db";
    string comando = "SELECT * FROM palabra";
    List<string> guardar = new List<string>();

    void Start()
    {
        guardar.Clear();
        string filepath = Application.dataPath + "/Resources/Traductor/Data_Base/" + databasename;

        dburl = "URI=file:" + filepath;
        var dropdown = transform.GetComponent<Dropdown>();

        dropdown.options.Clear();
        string palabra;
        Debug.Log("Conexion establecida" + dburl);
        connection = new SqliteConnection(dburl);
        connection.Open();

        reader_funcion(dropdown);

        dropdown.onValueChanged.AddListener(delegate { DropdownItemSelected(dropdown); });
    }


    private void reader_funcion(Dropdown dropdown)
    {
        List<string> palabras = new List<string>();
        using (connection = new SqliteConnection(dburl))
        {
            connection.Open();
            command = connection.CreateCommand();
            command.CommandText = comando;
            reader = command.ExecuteReader();
            while(reader.Read())
            {
                palabras.Add(reader.GetString(0));

            }
            foreach(var palabra in palabras)
            {
                dropdown.options.Add(new Dropdown.OptionData() { text = palabra });
            }
        }
        connection.Close();
    }


    
    
    int n = 0;
    
    void DropdownItemSelected(Dropdown dropdown)
    {
        int index = dropdown.value;
        
        guardar.Add(dropdown.options[index].text);
        Debug.Log("Se agrego= " + dropdown.options[index].text);
        text.text = string.Join(", ", guardar);
    }

    public string imprimir(List<string> dato)
    {
        var text = string.Empty;
        foreach (string palabra in dato)
        {
            text += palabra.ToString() + "\r\n";
        }
        return text;

    }


    public void delete()
    {
        Debug.Log("Se elimino registro");
        guardar.Clear();
    }
    
}