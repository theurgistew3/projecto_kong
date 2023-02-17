using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controlador_pantalla : MonoBehaviour
{

    public autocomplete autocomplete;
    public Reproducir reproducir;

    public GameObject canvas1;
    public GameObject canvas2;
    public GameObject canvas3;
    public GameObject canvas4;

    

    // Start is called before the first frame update
    void Start()
    {
        canvas2.SetActive(false);
        canvas3.SetActive(false);
        canvas4.SetActive(false);
    }


    public void inicio()
    {
        canvas1.SetActive(false);
        canvas2.SetActive(true);
    }

    public void frase()
    {
        canvas2.SetActive(false);
        canvas3.SetActive(true);
    }

    public void traducir()
    {
        reproducir.lista = autocomplete.guardar;
        Debug.Log(autocomplete.text.text);
        canvas3.SetActive(false);
        canvas4.SetActive(true);
    }

    public void volver_frase()
    {
        reproducir.lista.Clear();
        reproducir.posicion = 0;
        autocomplete.dato.Clear();
        autocomplete.guardar.Clear();
        canvas3.SetActive(true);
        canvas4.SetActive(false);
        
    }

    public void volver_menu()
    {
        autocomplete.dato.Clear();
        autocomplete.guardar.Clear();
        canvas2.SetActive(true);
        canvas3.SetActive(false);
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }



}
