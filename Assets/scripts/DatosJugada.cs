using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatosJugada : DatoPos {

    Animator animator;
    private int turno;

    public int Turno
    {
        get
        {
            return turno;
        }

        set
        {
            turno = value;
        }
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnMouseEnter()
    {
        if (turno % 2 == 0) {
            animator.SetBool("MouseOn", true);
        }
        
    }

    private void OnMouseExit()
    {
        if (turno % 2 == 0)
        {
            animator.SetBool("MouseOn", false);
        }
        
    }

    public void eliminar()
    {
        StartCoroutine(destruir());
        
    }

    IEnumerator destruir()
    {
        animator.Play("eliminar");
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
    }
}
