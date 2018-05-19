using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nodo
{
    public Estado estado;
    public Nodo parent;
    public List<Nodo> hijos;
    public int action;
    public int depth;
    public int valorMinimax;
    public Nodo(Estado data)
    {

        depth = 0;
        this.parent = null;
        hijos = new List<Nodo>();
        this.estado = data;
    }

    public Nodo(Estado data, Nodo parent)
    {
        depth = parent.depth + 1;
        this.parent = parent;
        hijos = new List<Nodo>();
        this.estado = data;
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

    public void agregarHijos(Nodo hijo) {
        hijos.Add(hijo);
    }

    public void setValorHeuristica() {
        valorMinimax = estado.calcularHeuristica();
    }

}
