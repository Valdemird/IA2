using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerScript : MonoBehaviour {
    public const int VACIO = 0;
    public const int MANZANA = 1;
    public const int CABALLOIA = 2;
    public const int CABALLOJUGADOR = 3;
    public const int MOVIMIENTO_ABAJO_DERECHA = 4;
    public const int MOVIMIENTO_ABAJO_IZQUIERDA = 5;
    public const int MOVIMIENTO_ARRIBA_DERECHA = 6;
    public const int MOVIMIENTO_ARRIBA_IZQUIERDA = 7;
    public const int MOVIMIENTO_DERECHA_ABAJO = 8;
    public const int MOVIMIENTO_DERECHA_ARRIBA = 9;
    public const int MOVIMIENTO_IZQUIERDA_ABAJO = 10;
    public const int MOVIMIENTO_IZQUIERDA_ARRIBA = 11;



    [SerializeField]
    GameObject casilaOcupada;
    [SerializeField]
    GameObject manazana;
    [SerializeField]
    GameObject CaballoJugador;
    [SerializeField]
    GameObject CaballoIA;
    [SerializeField]
    GameObject CasillaNegra;
    [SerializeField]
    GameObject CasillaBlanca;
    [SerializeField]
    int cantidadItems;
    int[,] representacion;
    int color = 1;

    //Instancias
    GameObject caballoEnjuego;
    GameObject caballoEnJUegoIA;
    ArrayList manzanas;

    // Use this for initialization
    void Start () {
        representacion = new int[6,6];
        manzanas = new ArrayList();
        crearTablero();
        inicializacion();
        calcularMovimientosPosibles(caballoEnjuego);

    }


    void calcularMovimientosPosibles(GameObject caballo) {
        int posX = (int)caballo.transform.position.x;
        int posY =(int) caballo.transform.position.z;

        for (int i = MOVIMIENTO_ABAJO_DERECHA; i <= MOVIMIENTO_IZQUIERDA_ARRIBA; i++) {

            Vector2 posicion = obtenerNuevaPosicion(i, posX, posY);
            Debug.Log("Posicion X:  " + posicion.x);
            Debug.Log("Posicion Y:  " + posicion.y);
            Debug.Log(validarMovimiento((int)posicion.x, (int)posicion.y));
            if (validarMovimiento((int)posicion.x, (int)posicion.y)) {
                
                Vector3 pos = new Vector3(posicion.x,0,posicion.y);
                Instantiate(casilaOcupada, pos, transform.rotation);
            }
        }
    }

    Vector2 obtenerNuevaPosicion(int movimiento,int posX,int posY) {

        switch (movimiento) {
            case MOVIMIENTO_ABAJO_DERECHA:
                posY -= 2;
                posX += 1;
                break;
            case MOVIMIENTO_ABAJO_IZQUIERDA:
                posY -= 2;
                posX -= 1;
                break;
            case MOVIMIENTO_ARRIBA_DERECHA:
                posY += 2;
                posX += 1;
                break;
            case MOVIMIENTO_ARRIBA_IZQUIERDA:
                posY += 2;
                posX -= 1;
                break;
            case MOVIMIENTO_DERECHA_ABAJO:
                posY -= 1;
                posX += 2;
                break;
            case MOVIMIENTO_DERECHA_ARRIBA:
                posY += 1;
                posX += 2;
                break;
            case MOVIMIENTO_IZQUIERDA_ABAJO:
                posY -= 1;
                posX -= 2;
                break;
            case MOVIMIENTO_IZQUIERDA_ARRIBA:
                posY += 1;
                posX -= 2;
                break;
        }
        return new Vector2(posX, posY);
    }

    bool validarMovimiento(int posX, int posY) {
        if (posX < 0 || posX > 5 || posY < 0 || posY > 5)
        {
            return false;
        }
        else if (representacion[posX,posY] == CABALLOIA)
        {
            return false;    
        }
        return true;
    }

    void inicializacion() {
        createObject(CABALLOIA);
        createObject(CABALLOJUGADOR);

        for (int i = 0; i < cantidadItems; i++)
        {
            createObject(MANZANA);
        }
    }
    void crearTablero()
    {
        for (int i = 0; i < 6; i++)
        {
            if (i % 2 == 0){
                color = 1; }
            else {
                color = 0;
            }

            for (int j = 0; j < 6; j++)
            {
                if (color == 1)
                {
                    Instantiate(CasillaBlanca, new Vector3(i,0,j), transform.rotation);
                    color = 0;
                }
                else
                {
                    Instantiate(CasillaNegra, new Vector3(i,0,j), transform.rotation);
                    color = 1;
                }
            }
        }
    }
    void createObject(int tipo) {
        bool escrito = false;
        int posX;
        int posY;
        while (!escrito) {
            posX = Random.Range(0, 6);
            posY = Random.Range(0, 6);
            if (representacion[posX, posY] == VACIO)
            {
                escrito = true;
                GameObject elemento = new GameObject();
                Vector3 pos = new Vector3(posX, 0.1f, posY);
                switch (tipo)
                {
                    case MANZANA:
                        GameObject man = Instantiate(manazana, pos, transform.rotation) as GameObject;
                        manzanas.Add(man);
                        break;
                    case CABALLOIA:
                        caballoEnJUegoIA = Instantiate(CaballoIA, pos, transform.rotation) as GameObject;
                        break;
                    case CABALLOJUGADOR:
                        caballoEnjuego = Instantiate(CaballoJugador, pos, transform.rotation) as GameObject;
                        break;
                }

                representacion[posX, posY] = tipo;
            }
            
        }
          
            
        
    }
	

	// Update is called once per frame
	void Update () {
		
	}
}
