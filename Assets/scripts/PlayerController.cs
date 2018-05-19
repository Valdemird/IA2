using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    Camera camera;
    ManagerScript manager;

	// Use this for initialization
	void Start () {
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        manager = gameObject.GetComponent<ManagerScript>();
	}
	
	// Update is called once per frame
	void Update () {


        if (Input.GetKeyDown(KeyCode.Mouse0) && manager.turnoJugador()) {
            RaycastHit hit;
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction * Mathf.Infinity, Color.red, 5);
            if (Physics.Raycast(ray, out hit))
            {
                
                GameObject objectHit = hit.transform.gameObject;
                //objectHit.GetComponent<Animator>().Play("seleccionado");

                DatosJugada datos = objectHit.GetComponent<DatosJugada>();
                int posX = datos.PosX;
                int posY = datos.PosY;
                manager.hacerJugada(posX, posY,manager.turnoJugador());
            }
        }
    }

}
