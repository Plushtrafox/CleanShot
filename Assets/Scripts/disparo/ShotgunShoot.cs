using UnityEngine;

public class ShotgunShoot : MonoBehaviour
{
    public int pellets = 6; // N�mero de perdigones
    public float spreadAngle = 10f; // �ngulo de dispersi�n
    public float range = 50f; // Alcance del disparo
    public int damage = 10; // Da�o por perdig�n
    public LayerMask hitMask; // Qu� capas puede impactar
    public Transform shootPoint; // Punto de disparo
    public ParticleSystem muzzleFlash; // Efecto de disparo
    public AudioSource shootSound; // Sonido de disparo

    void Update()
    {
        if (Input.GetButtonDown("Fire1")) // Disparo con bot�n izquierdo del rat�n
        {
            Shoot();
        }
    }

    void Shoot()
    {
        if (muzzleFlash) muzzleFlash.Play(); // Efecto de disparo
        if (shootSound) shootSound.Play(); // Sonido de disparo

        for (int i = 0; i < pellets; i++)
        {
            Vector3 direction = shootPoint.forward;
            direction += new Vector3(
                Random.Range(-spreadAngle, spreadAngle),
                Random.Range(-spreadAngle, spreadAngle),
                Random.Range(-spreadAngle, spreadAngle)
            ) * 0.01f; // Peque�o ajuste para dispersi�n

            RaycastHit hit;
            if (Physics.Raycast(shootPoint.position, direction, out hit, range, hitMask))
            {
                Debug.DrawRay(shootPoint.position, direction * hit.distance, Color.red, 0.1f);

                if (hit.collider.CompareTag("Enemy")) // Si golpea a un enemigo
                {
                    EnemigoVida enemy = hit.collider.GetComponent<EnemigoVida>();
                    if (enemy != null)
                    {
                        enemy.recibirDamage(damage);
                    }
                }
            }
        }
    }
}