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
    string comando = "SELECT * FROM palabra";
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
        new struct_animation("Espacio", 1),
        new struct_animation("A", 2),
new struct_animation("B", 3),
new struct_animation("C", 4),
new struct_animation("D", 5),
new struct_animation("E", 6),
new struct_animation("F", 7),
new struct_animation("G", 8),
new struct_animation("H", 9),
new struct_animation("i", 10),
new struct_animation("j", 11),
new struct_animation("K", 12),
new struct_animation("L", 13),
new struct_animation("Ll", 14),
new struct_animation("M", 15),
new struct_animation("N", 16),
new struct_animation("O", 17),
new struct_animation("P", 18),
new struct_animation("Q", 19),
new struct_animation("R", 20),
new struct_animation("RR", 21),
new struct_animation("S", 22),
new struct_animation("T", 23),
new struct_animation("U", 24),
new struct_animation("V", 25),
new struct_animation("W", 26),
new struct_animation("X", 27),
new struct_animation("Y", 28),
new struct_animation("Z", 29),
new struct_animation("Ñ", 30),
new struct_animation("1", 31),
new struct_animation("2", 33),
new struct_animation("3", 34),
new struct_animation("4", 35),
new struct_animation("5", 36),
new struct_animation("6", 37),
new struct_animation("7", 38),
new struct_animation("8", 39),
new struct_animation("9", 40),
new struct_animation("10", 32),
new struct_animation("Adios", 41),
new struct_animation("Buenos", 42),
new struct_animation("Cuidate mucho", 43),
new struct_animation("Dia", 44),
new struct_animation("Gracias", 45),
new struct_animation("Hola", 46),
new struct_animation("Mal", 47),
new struct_animation("Mucho gusto", 48),
new struct_animation("Noche", 49),
new struct_animation("Tarde", 50),
new struct_animation("El", 51),
new struct_animation("Ellos", 52),
new struct_animation("Mio", 53),
new struct_animation("Nosotros", 54),
new struct_animation("Suyo", 55),
new struct_animation("Tu", 56),
new struct_animation("Tuyo", 57),
new struct_animation("Ustedes", 58),
new struct_animation("Yo", 59),
new struct_animation("Adulto", 60),
new struct_animation("Bonito", 61),
new struct_animation("Dificil", 62),
new struct_animation("Divertido", 63),
new struct_animation("Dulce", 64),
new struct_animation("Feo", 65),
new struct_animation("Grande", 66),
new struct_animation("Limpio", 67),
new struct_animation("Nuevo", 68),
new struct_animation("Pequeño", 69),
new struct_animation("Rico Adinerado", 70),
new struct_animation("Rico Sabor", 71),
new struct_animation("Sucio", 72),
new struct_animation("Viejo", 73),
new struct_animation("Caminar", 74),
new struct_animation("Comer", 75),
new struct_animation("Comprar", 76),
new struct_animation("Conocer", 77),
new struct_animation("Correr", 78),
new struct_animation("Dormir", 79),
new struct_animation("Estudiar", 80),
new struct_animation("Gustar", 81),
new struct_animation("Hablar", 82),
new struct_animation("Ir", 83),
new struct_animation("Pasear", 84),
new struct_animation("Azul", 85),
new struct_animation("Blanco", 86),
new struct_animation("Cafe", 87),
new struct_animation("Gris", 88),
new struct_animation("Negro", 89),
        };

        dropdown.options.Clear();

        reader_funcion3(dropdown);
    }

    private void reader_funcion3(Dropdown dropdown)
    {
		foreach (var palabra in palabras)
		{
			 dropdown.options.Add(new Dropdown.OptionData() { text = palabra.name });
		}
    }


    private void reader_funcion2(Dropdown dropdown)
    {

        if (filepath.Contains("base.apk"))
        {
            filepath = filepath.Replace("base.apk", "");
        }

        if (!Directory.Exists(filepath))
        {
            Directory.CreateDirectory(filepath);
            Debug.Log("Directorio creado=" + filepath);
        }

        filepath = Path.Combine(filepath, databasename);

        if (File.Exists(filepath))
        {
            Debug.Log("Existe el archivo");

        }

        dburl = "URI=file:" + filepath;

        if (!File.Exists(filepath))
        {

            var file = File.Create(filepath);

            file.Close();

            using (connection = new SqliteConnection(dburl))
            {
                connection.Open();
                creartabla(connection);
                insertar(connection);
                connection.Close();
            }

        }



        


        

        using (connection = new SqliteConnection(dburl))
        {
            connection.Open();


            command = connection.CreateCommand();
            command.CommandText = comando;
            reader = command.ExecuteReader();
            int n = 0;
            int i = 1000;

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
        connection.Close();
    }

	protected override void OnDisable()
	{
      base.OnDisable();
	}


	private void insertar(IDbConnection con)
    {
        string insert1 = "INSERT INTO Animacion(animid) VALUES(1), (2), (3), (4), (5), (6) , (7), (8), (9), (10)";
        command = con.CreateCommand();
        command.CommandText = insert1;
        reader = command.ExecuteReader();

        string insert2 = "INSERT INTO Alfabeto(letra, idanim) VALUES('A', 1), ('B', 2), ('C', 3), ('D', 4), ('E', 5), ('F', 6) , ('G', 7), ('H', 8), ('I', 9), ('J', 10), ('K', 11), ('L', 12), ('LL', 13), ('F', 6) , ('G', 7), ('H', 8), ('I', 9)";
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

        string vista = "CREATE VIEW palabra as SELECT letra, idanim as id from Alfabeto UNION ALL SELECT numero, idanim from numeros UNION ALL SELECT numero, idanim from adjetivo UNION ALL SELECT objeto, idanim from Objetos UNION ALL SELECT size, idanim FROM Tamaño UNION all SELECT sujeto, idanim FROM Sujeto_t from verbo, idanim from Verbo_t";
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

    

    
    

    public void delete()
    {
        Debug.Log("Se elimino registro");
        dato.Clear();
        guardar.Clear();
    }
    
}