using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nodo
{

    private const int limite = 2000;
    public Estado estado;
    public Nodo parent;
    public Nodo ultimajugada;
    public List<Nodo> hijos;
    public int action;
    public int depth;
    public int utilidad;
    
    public Nodo(Estado data)
    {

        depth = 0;
        this.parent = null;
        hijos = new List<Nodo>();
        this.estado = data;
        initUtilidad();
    }

    public Nodo(Estado data, Nodo parent)
    {
        depth = parent.depth + 1;
        this.parent = parent;
        hijos = new List<Nodo>();
        this.estado = data;
        initUtilidad();
    }

    public bool isMax() {
        if (depth % 2 == 0)
        {
            return true;
        }
        else {
            return false;
        }
    }

    private void initUtilidad(){
        if (isMax())
        {
            utilidad = -2000;
        }
        else {
            utilidad = 2000;
        }
    }

    public void calculateMinMax(Nodo hijo) {
        if (isMax())
        {
            if (hijo.utilidad > utilidad) {
                utilidad = hijo.utilidad;
                ultimajugada = hijo.ultimajugada;
            }
        }
        else {
            if (hijo.utilidad < utilidad)
            {
                utilidad = hijo.utilidad;
                ultimajugada = hijo.ultimajugada;
            }
        }
    }

    public void agregarHijos(Nodo hijo) {
        hijos.Add(hijo);
    }   

    public void setValorHeuristica(int cantidadItems) {
        utilidad = estado.calcularHeuristica(cantidadItems);
    }

}
