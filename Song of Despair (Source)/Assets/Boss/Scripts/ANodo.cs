using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ANodo {

    // PROPIEDADES

    private string nombre;

    public string Nombre {
        get {
            return nombre;
        }
        private set {
            nombre = value;
        }
    }

    public Dictionary<Simbolo, ANodo> Transferencia {
        get;
        private set;
    }

    //importar System o usar System.Type
    public Type Comportamiento
    {
        get;
        private set;
    }

    public ANodo(string nombre, Type comportamiento) {

        Nombre = nombre;
        Transferencia = new Dictionary<Simbolo, ANodo>();
        Comportamiento = comportamiento;

    }

    public void AddTransicion(Simbolo simbolo, ANodo nodo) {
        Transferencia.Add(simbolo, nodo);
    }

    public ANodo AplicarTransicion(Simbolo simbolo) {

        if (Transferencia.ContainsKey(simbolo))
        {
            return Transferencia[simbolo];
        }
        return this;
    }
}
