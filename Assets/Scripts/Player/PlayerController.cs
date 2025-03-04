using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    float movimientoX;
    float movimientoZ;
    public float velocidadMovimiento = 5f;
    public float velocidadCaminar = 5f;
    public float velocidadCorrer = 10f;
    public Rigidbody rb;

    public float runSpeed = 8f; // Velocidad al correr
    public float fuerzaSalto = 5f; // Fuerza de salto
    public bool tocandoPiso = true;


    void Awake()
    {

    }
    void Update()
    {
        tocandoPiso = Physics.Raycast(transform.position, Vector3.down, 1.5f);


        velocidadMovimiento = Input.GetKey(KeyCode.LeftShift) ? velocidadCorrer : velocidadCaminar;
        movimientoX = Input.GetAxis("Horizontal");
        movimientoZ = Input.GetAxis("Vertical");

        Vector3 movimientoBasico = transform.right * movimientoX + transform.forward * movimientoZ;
        Vector3 movimientoCompleto = movimientoBasico * velocidadMovimiento;

        movimientoCompleto.y = rb.linearVelocity.y;

        rb.linearVelocity = movimientoCompleto;

        if (tocandoPiso && Input.GetButton("Jump"))
        {
            rb.AddForce(Vector3.up* fuerzaSalto,ForceMode.Impulse );
        }







    }
}
