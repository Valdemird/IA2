using System.Collections;
using System.Collections.Generic;
public class Estado  {


    public int[,] representacion;
    public int puntajeIA;
    public int puntajeJugador;
    public int posX;
    public int posY;
    public int posXJugador;
    public int posYJugador;
    public Estado(int[,] representacion, int puntajeIA, int puntajeJugador, int posX, int posY, int posXJugador, int posYJugador)
    {
        this.representacion = representacion;
        this.puntajeIA = puntajeIA;
        this.puntajeJugador = puntajeJugador;
        this.posX = posX;
        this.posY = posY;
        this.posXJugador = posXJugador;
        this.posYJugador = posYJugador;
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

    public int calcularHeuristica(int cantidadItems) {
        int tmp = 0;
        tmp = puntajeIA - puntajeJugador;
        return tmp;
    }
}
