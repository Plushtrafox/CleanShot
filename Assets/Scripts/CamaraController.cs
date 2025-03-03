using UnityEngine;

public class CamaraController : MonoBehaviour
{
    float movimientoX;
    float movimientoZ;
    Rigidbody rb;
    void Awake()
    {
        movimientoX = Input.GetAxis("Horizontal");
        movimientoZ = Input.GetAxis("Vertical");
        rb = GetComponent<Rigidbody>();

    }


    void Update()
    {
    //rb.linearVelocity =
    }
}
