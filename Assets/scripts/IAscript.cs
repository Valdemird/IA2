using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAscript : MonoBehaviour {
    public ManagerScript manager;
    int vecesExpandido = 0;
    private void Start()
    {
        //manager = gameObject.GetComponent<ManagerScript>();
    }


    public Vector2 AlgoritmoMinimax(int[,] representacion, int puntajeIA, int puntajeJugador, int posX, int posY, int profundidad) {
        Estado estadoRoot = new Estado(representacion, puntajeIA, puntajeJugador, posX, posY);
        Queue<Nodo> nodosFrontera = new Queue<Nodo>();
        nodosFrontera.Enqueue(new Nodo(estadoRoot));
        Nodo nodoActual = null;
        while (nodosFrontera.Count != 0) {
            nodoActual = nodosFrontera.Dequeue();
            List<Vector2> listaMovimientos = calcularMovimientosPosibles(nodoActual.estado.posX, nodoActual.estado.posY);
            foreach (Vector2 movimiento in listaMovimientos) {
                if (nodoActual.depth < profundidad) {
                    Nodo nodoTmp = Expandir(movimiento, nodoActual, nodoActual.isMax());
                    nodoActual.agregarHijos(nodoTmp);
                    nodosFrontera.Enqueue(nodoTmp);
                }
            }
        }
       return EncontrarMejorJugada(nodoActual);
    }

    public Vector2 EncontrarMejorJugada(Nodo nodoActual) {
        while (nodoActual.parent != null) {
            nodoActual = nodoActual.parent;
            
        }
        Debug.Log("movimiento papa " + nodoActual.depth + " (" + nodoActual.estado.posX + "," + nodoActual.estado.posY + ")");
        Nodo hijoMax = minimax(nodoActual);
        Debug.Log("movimiento " + hijoMax.depth + " (" + hijoMax.estado.posX + "," + hijoMax.estado.posY + ")");
        Vector2 vector2 = new Vector2(hijoMax.estado.posX, hijoMax.estado.posY);
        return vector2;
        
    }

    public Nodo minimax(Nodo nodo) {
        Nodo nodoMiniMax;
        Nodo resultado = null;
        if (nodo.isMax())
        {
            int max = -2000;
            

            foreach (Nodo hijo in nodo.hijos)
            {
                
                if (hijo.hijos.Count == 0)
                {
                    hijo.setValorHeuristica();
                    nodoMiniMax = hijo;
                    
                }
                else
                {
                    nodoMiniMax = minimax(hijo);
                }
                if (nodoMiniMax.valorMinimax > max) {
                    max = nodoMiniMax.valorMinimax;
                    resultado = nodoMiniMax;
                }
            }
            return resultado;
        }
        else {
            int min = 2000;
            foreach (Nodo hijo in nodo.hijos)
            {
                int valor;
                if (hijo.hijos.Count == 0)
                {
                    hijo.setValorHeuristica();
                    nodoMiniMax = hijo;
                }
                else
                {
                    nodoMiniMax = minimax(hijo);
                }
                if (nodoMiniMax.valorMinimax < min)
                {
                    min = nodoMiniMax.valorMinimax;
                    resultado = nodoMiniMax;
                }
            }
            Debug.Log("minimax resultado" + resultado.depth + " (" + resultado.estado.posX + "," + resultado.estado.posY + ")");
            return resultado;
        }

    }


    List<Vector2> calcularMovimientosPosibles(int posX, int posY)
    {
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
        int puntajeJugador = nodoPadre.estado.puntajeJugador;
        int puntajeIA = nodoPadre.estado.puntajeIA;
        int[,] representacionTmp = nodoPadre.estado.representacion;


        representacionTmp[posX, posY] = ManagerScript.VACIO;
        posX = (int)nuevaPosicion.x;
        posY = (int)nuevaPosicion.y;
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
            representacionTmp[posX,posY] = ManagerScript.CABALLOIA;
        } else {
            representacionTmp[posX, posY] = ManagerScript.CABALLOJUGADOR;
        }

        Estado nuevoEstado = new Estado(representacionTmp, puntajeIA, puntajeJugador, posX, posY);
        return new Nodo(nuevoEstado, nodoPadre);
       
    }




}
