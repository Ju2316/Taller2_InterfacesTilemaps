using System;
using UnityEngine;

public class PuntosVerdes : MonoBehaviour
{
    public static Action PuntosRecolectado;



    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Recolectar();
        }
    }

    private void Recolectar()
    {
        PuntosRecolectado?.Invoke();
        Destroy(gameObject);
    }
}