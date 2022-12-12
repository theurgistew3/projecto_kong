using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controlador_pantalla : MonoBehaviour
{

    public autocomplete autocomplete;
    public Reproducir reproducir;

    public GameObject canvas1;
    public GameObject canvas2;

    

    // Start is called before the first frame update
    void Start()
    {
        canvas2.SetActive(false);
    }


    public void lista(List<string> palabras)
    {

    }

    public void traducir()
    {
        reproducir.lista = autocomplete.guardar;
        
        canvas1.SetActive(false);
        canvas2.SetActive(true);
    }

    public void volver()
    {
        canvas1.SetActive(true);
        canvas2.SetActive(false);
        reproducir.lista.Clear();
        reproducir.posicion = 0;
        autocomplete.dato.Clear();
        autocomplete.guardar.Clear();
    }


    // Update is called once per frame
    void Update()
    {
        
    }



}
