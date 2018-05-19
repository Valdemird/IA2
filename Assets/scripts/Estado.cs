using System.Collections;
using System.Collections.Generic;
public class Estado  {


    public int[,] representacion;
    public int puntajeIA;
    public int puntajeJugador;
    public int posX;
    public int posY;
    public Estado(int[,] representacion, int puntajeIA, int puntajeJugador,int posX,int posY)
    {
        this.representacion = representacion;
        this.puntajeIA = puntajeIA;
        this.puntajeJugador = puntajeJugador;
        this.posX = posX;
        this.posY = posY;
    }

    public void agregarManazana(bool esJugador) {
        if (esJugador)
        {
            puntajeJugador++;
        }
        else {
            puntajeIA++;
        }
    }

    public int calcularHeuristica() {
         return puntajeIA - puntajeJugador;
    }
}
