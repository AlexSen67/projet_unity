using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float bulletSpeed = 30.0f;
    public float bulletLife = 3.0f;
    private Animator animationGun; // Référence à l'Animator


    private AudioSource audioShoot;

    [SerializeField]
    private InputActionReference fireAction;    
    void Start()
    {
        audioShoot = GetComponent<AudioSource>();
        fireAction.asset.Enable();
        animationGun = GetComponent<Animator>();
    }
    void Update()
    {
        // Check if the player has pressed the left mouse button
        if (fireAction.action.triggered)
        {
            Debug.Log("Fire!");
            Fire();
        }
        else
        {
            animationGun.SetBool("isFiring", false);
            audioShoot.Stop();
        }
        

    }

    private void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);

        // shoot the bullet
        bullet.GetComponent<Rigidbody>().AddForce(bulletSpawn.forward.normalized * bulletSpeed, ForceMode.Impulse);

        // Destroy the bullet after a certain amount of time
        StartCoroutine(DestroyBulletAfterTime(bullet, bulletLife));
        animationGun.SetBool("isFiring", true);
        audioShoot.Play();

    }

    private IEnumerator DestroyBulletAfterTime(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(bullet);
    }


}
