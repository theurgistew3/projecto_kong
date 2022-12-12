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
      palabras.Clear();
        
      palabras = new List<struct_animation>()
      {
        new struct_animation("A", 1),
        new struct_animation("b", 2),
        new struct_animation("C", 3),
        new struct_animation("D", 4),
        new struct_animation("E", 5),
        new struct_animation("F", 6),
        new struct_animation("G", 7),
        new struct_animation("H", 8),
        new struct_animation("i", 9),
        new struct_animation("j", 10),
        new struct_animation("K", 11),
        new struct_animation("L", 12),
        new struct_animation("Ll", 13),
        new struct_animation("M", 14),
        new struct_animation("N", 15),
        new struct_animation("o", 16),
        new struct_animation("P", 17),
        new struct_animation("Q", 18),
        new struct_animation("r", 19),
        new struct_animation("rr", 20),
        new struct_animation("S", 21),
        new struct_animation("T", 22),
        new struct_animation("U", 23),
        new struct_animation("V", 24),
        new struct_animation("W", 25),
        new struct_animation("X", 26),
        new struct_animation("y", 27),
        new struct_animation("Ñ", 28),
        new struct_animation("Catorce", 29),
        new struct_animation("Cinco", 30),
        new struct_animation("Cuatro", 31),
        new struct_animation("Diecinueve", 32),
        new struct_animation("Dieciocho", 33),
        new struct_animation("Dieciseis", 34),
        new struct_animation("Diecisiete", 35),
        new struct_animation("Diez", 36),
        new struct_animation("Doce", 37),
        new struct_animation("Dos", 38),
        new struct_animation("Nueve", 39),
        new struct_animation("Ocho", 40),
        new struct_animation("Once", 41),
        new struct_animation("Quince", 42),
        new struct_animation("Seis", 43),
        new struct_animation("Siete", 44),
        new struct_animation("Trece", 45),
        new struct_animation("Tres", 46),
        new struct_animation("Uno", 47),
        new struct_animation("Veinte", 48),
        new struct_animation("Adios", 49),
        new struct_animation("Bien", 50),
        new struct_animation("Buenas noches", 51),
        new struct_animation("Buenas tardes", 52),
        new struct_animation("Buenos dias", 53),
        new struct_animation("Cuidate mucho", 54),
        new struct_animation("Gracias", 55),
        new struct_animation("Hola", 56),
        new struct_animation("Mal", 57),
        new struct_animation("Mucho gusto en conocerte", 58),
        new struct_animation("El", 59),
        new struct_animation("Ellos", 60),
        new struct_animation("Mio", 61),
        new struct_animation("Nosotros", 62),
        new struct_animation("Suyo", 63),
        new struct_animation("Tu", 64),
        new struct_animation("Tuyo", 65),
        new struct_animation("Ustedes", 66),
        new struct_animation("Yo", 67),
      };

        dropdown.options.Clear();

        //reader_funcion2(dropdown);
        reader_funcion3(dropdown);

        //dropdown.onValueChanged.AddListener(delegate { DropdownItemSelected(dropdown); });
    }

    private void reader_funcion3(Dropdown dropdown)
    {
		foreach (var palabra in palabras)
		{
			 //Debug.Log("palabra= " + palabra.name);
			 dropdown.options.Add(new Dropdown.OptionData() { text = palabra.name });
		}
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

        string insert2 = "INSERT INTO Alfabeto(letra, idanim) VALUES('A', 1), ('B', 2), ('C', 3), ('D', 4), ('E', 5), ('F', 6) , ('G', 7), ('H', 8)";
        command = con.CreateCommand();
        command.CommandText = insert2;
        command.ExecuteNonQuery();


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