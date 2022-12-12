using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;
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
    string filepath = Application.streamingAssetsPath + "/Resources/Traductor/Data_Base/";

   public SqliteConnection connection_other;

    public List<struct_animation> guardar = new List<struct_animation>();
    public List<struct_animation> palabras = new List<struct_animation>();
    public List<string> dato = new List<string>();
    protected override void _init_cache()
    {
    
        base._init_cache();
        guardar.Clear();
        dato.Clear();

        dropdown.options.Clear();

        reader_funcion2(dropdown);

        //dropdown.onValueChanged.AddListener(delegate { DropdownItemSelected(dropdown); });
    }

    private void reader_funcion2(Dropdown dropdown)
    {
      connection_other = new SqliteConnection( "Data Source=:memory:" );
		connection_other.Open();
		creartabla(connection_other);
		insertar(connection_other);

		command = connection_other.CreateCommand();
		command.CommandText = comando;
		reader = command.ExecuteReader();
		int n = 0;
		int i = 51;

      debug.log( "despues de creacion" );

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
		foreach (var palabra in palabras)
		{
			 Debug.Log("palabra= " + palabra.name);
			 dropdown.options.Add(new Dropdown.OptionData() { text = palabra.name });
		}
    }

	protected override void OnDisable()
	{
      base.OnDisable();
      connection_other.Close();
	}


	private void insertar(IDbConnection con)
    {
        string insert1 = "INSERT INTO Animacion(animid) VALUES(1), (2), (3), (4), (5), (6) , (7), (8), (9), (10)";
        command = con.CreateCommand();
        command.CommandText = insert1;
        reader = command.ExecuteReader();
      debug.log( "asdf" );

        string insert2 = "INSERT INTO Alfabeto(letra, idanim) VALUES('A', 1), ('B', 2), ('C', 3), ('D', 4), ('E', 5), ('F', 6) , ('G', 7), ('H', 8)";
        command = con.CreateCommand();
        command.CommandText = insert2;
        command.ExecuteNonQuery();
      debug.log( "qwert" );


    }

    private void creartabla(IDbConnection con)
    {
        string tabla1 = "CREATE TABLE Animacion(animid INTEGER PRIMARY KEY NOT NULL)";
        command = con.CreateCommand();
        command.CommandText = tabla1;
        command.ExecuteNonQuery();

        string tabla2 = "CREATE TABLE Alfabeto(idalf INTEGER PRIMARY KEY AUTOINCREMENT, letra varchar(2), idanim INTEGER, FOREIGN KEY(idanim) REFERENCES Animacion(animid))";
        command = con.CreateCommand();
        command.CommandText = tabla2;
        command.ExecuteNonQuery();

        string vista = "CREATE VIEW palabra as SELECT letra, idanim as id from Alfabeto";
        command = con.CreateCommand();
        command.CommandText = vista;
        command.ExecuteNonQuery();
    }



   public void on_dropbox_change()
   {
        int index = dropdown.value;
        Debug.Log(palabras[index]);

        guardar.Add(palabras[index]);
        dato.Add(dropdown.options[index].text);
        Debug.Log("Se agrego= " + dropdown.options[index].text);
        text.text = string.Join(" ", dato);
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