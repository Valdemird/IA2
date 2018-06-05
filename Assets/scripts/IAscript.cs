using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAscript : MonoBehaviour {
    public ManagerScript manager;
    int vecesExpandido = 0;
    Stack<Nodo> nodosRecorrer = new Stack<Nodo>();
    private void Start()
    {
        manager = gameObject.GetComponent<ManagerScript>();
    }


    public Vector2 AlgoritmoMinimax(int[,] representacion, int puntajeIA, int puntajeJugador, int posX, int posY,int posXJugador,int posYjugador, int profundidad) {
        Estado estadoRoot = new Estado(representacion, puntajeIA, puntajeJugador, posX, posY, posXJugador,posYjugador);
        Stack<Nodo> nodosFrontera = new Stack<Nodo>();
        nodosFrontera.Push(new Nodo(estadoRoot));
        Nodo nodoActual = null;
        while (nodosFrontera.Count != 0) {
            nodoActual = nodosFrontera.Pop();
            nodosRecorrer.Push(nodoActual);
            List<Vector2> listaMovimientos = calcularMovimientosPosibles(nodoActual);
            if (nodoActual.depth < profundidad) {
                foreach (Vector2 movimiento in listaMovimientos)
                {
                    Nodo nodoTmp = Expandir(movimiento, nodoActual, nodoActual.isMax());
                    if (nodoActual.depth == profundidad - 1)
                    {
                        nodoTmp.setValorHeuristica(manager.cantidadItems);
                        nodoTmp.ultimajugada = nodoTmp;
                    }
                    nodoActual.agregarHijos(nodoTmp);
                    
                    //Debug.Log("nodo en profundidad : " + nodoTmp.depth + " utilidad" + nodoTmp.utilidad + "(" + nodoTmp.estado.puntajeJugador + "," + nodoTmp.estado.puntajeIA + ")");
                    nodosFrontera.Push(nodoTmp);
                }
            }

        }
       return EncontrarMejorJugada(nodoActual);
    }

    public Vector2 EncontrarMejorJugada(Nodo nodoActual) {
        Nodo root = minimax(nodoActual);
        Nodo result = null;
        foreach (Nodo hijo in root.hijos) {
            Debug.Log("movimiento posible " + hijo.utilidad + "  (puntajeJugador,puntajeIA) (" + hijo.estado.posX + "," + hijo.estado.posY + ")" + "movimiento ultimaJugada " + hijo.ultimajugada.utilidad + "  (puntajeJugador,puntajeIA) (" + hijo.ultimajugada.estado.puntajeJugador + "," + hijo.ultimajugada.estado.puntajeIA + ")");
            if (result == null)
            {
                result = hijo;
            }
            else {
                if (result.utilidad < hijo.utilidad) {
                    result = hijo;
                }
            }
        }
        Debug.Log("movimiento hecho " + result.utilidad + "  (puntajeJugador,puntajeIA) (" + result.estado.posX + "," + result.estado.posY + ")" + "movimiento ultimaJugada " + result.ultimajugada.utilidad + "  (puntajeJugador,puntajeIA) (" + result.ultimajugada.estado.puntajeJugador + "," + result.ultimajugada.estado.puntajeIA + ")");
        Vector2 vector2 = new Vector2(result.estado.posX, result.estado.posY);
        return vector2;
        
    }

    public Nodo minimax(Nodo nodo)
    {
        Nodo temporal = null;
        while (nodosRecorrer.Count != 0)
        {
            temporal = nodosRecorrer.Pop();
            if (temporal.parent != null)
            {
                //Debug.Log("movimiento iterativo , profundidad = "+ temporal.depth + " utilidad = "+ temporal.utilidad + "(puntajeJugador,puntajeIA) (" + temporal.estado.puntajeJugador + "," + temporal.estado.puntajeIA + ")");
                temporal.parent.calculateMinMax(temporal);
            }
        }

        return temporal;
    }


    List<Vector2> calcularMovimientosPosibles(Nodo nodoActual)
    {

        int posX;
        int posY;

        if (nodoActual.isMax())
        {
            posX = nodoActual.estado.posX;
            posY = nodoActual.estado.posY;
        }
        else {
            posX = nodoActual.estado.posXJugador;
            posY = nodoActual.estado.posYJugador;
        }
        List<Vector2> jugadasPosibles = new List<Vector2>();
        for (int i = ManagerScript.MOVIMIENTO_ABAJO_DERECHA; i <= ManagerScript.MOVIMIENTO_IZQUIERDA_ARRIBA; i++)
        {
            
            Vector2 posicion = manager.obtenerNuevaPosicion(i, posX, posY);
            if (manager.validarMovimiento((int)posicion.x, (int)posicion.y))
            {
                jugadasPosibles.Add(posicion);
            }
        }
        return jugadasPosibles;
    }

    public Nodo Expandir(Vector2 nuevaPosicion, Nodo nodoPadre, bool isMax) {
        int posX = nodoPadre.estado.posX;
        int posY = nodoPadre.estado.posY;
        int posXJugador = nodoPadre.estado.posXJugador;
        int posYJugador  = nodoPadre.estado.posYJugador;
        int puntajeJugador = nodoPadre.estado.puntajeJugador;
        int puntajeIA = nodoPadre.estado.puntajeIA;
        int[,] representacionTmp = (int[,])nodoPadre.estado.representacion.Clone();


        representacionTmp[posX, posY] = ManagerScript.VACIO;

        int valorAnterio = representacionTmp[(int)nuevaPosicion.x, (int)nuevaPosicion.y];
        if (valorAnterio == ManagerScript.MANZANA) {
            if (isMax)
            {
                puntajeIA++; 
            }
            else {
                puntajeJugador++;  
            }
        }
        if (isMax) {
            posX = (int)nuevaPosicion.x;
            posY = (int)nuevaPosicion.y;
            representacionTmp[posX,posY] = ManagerScript.CABALLOIA;
        } else {
            posXJugador = (int)nuevaPosicion.x;
            posYJugador = (int)nuevaPosicion.y;
            representacionTmp[posX, posY] = ManagerScript.CABALLOJUGADOR;
        }

        Estado nuevoEstado = new Estado(representacionTmp, puntajeIA, puntajeJugador, posX, posY,posXJugador,posYJugador);
        return new Nodo(nuevoEstado, nodoPadre);
       
    }




}
