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
        reproducir.enabled = false;
    }


    public void lista(List<string> palabras)
    {

    }

    public void traducir()
    {
        reproducir.lista = autocomplete.guardar;
        canvas1.SetActive(false);
        canvas2.SetActive(true);
        reproducir.enabled = true;
        autocomplete.enabled = false;
    }

    public void volver()
    {
        canvas1.SetActive(true);
        canvas2.SetActive(false);
        autocomplete.enabled = true;
        reproducir.enabled = false;
    }


    // Update is called once per frame
    void Update()
    {
        
    }



}
