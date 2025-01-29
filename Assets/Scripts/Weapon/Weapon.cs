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

    private int bulletsPerShot = 2; // Nombre de balles tirées à chaque tir


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
        if (fireAction.action.triggered)
        {
            Fire();
            
        }
        else
        {
            animationGun.SetBool("isFiring", false);
            audioShoot.Stop();
        }

        CheckXPForWeaponUpgrade(); // Vérifie si l'XP a atteint un palier


    }

    //private void Fire()
    //{
    //    GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);

    //    // shoot the bullet
    //    bullet.GetComponent<Rigidbody>().AddForce(bulletSpawn.forward.normalized * bulletSpeed, ForceMode.Impulse);

    //    // Destroy the bullet after a certain amount of time
    //    StartCoroutine(DestroyBulletAfterTime(bullet, bulletLife));
    //    animationGun.SetBool("isFiring", true);
    //    audioShoot.Play();

    //}

    private void Fire()
    {
        for (int i = 0; i < bulletsPerShot; i++) // Tirer le nombre de balles défini
        {
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);

            // Shoot the bullet
            bullet.GetComponent<Rigidbody>().AddForce(bulletSpawn.forward.normalized * bulletSpeed, ForceMode.Impulse);

            // Destroy the bullet after a certain amount of time
            StartCoroutine(DestroyBulletAfterTime(bullet, bulletLife));
        }

        animationGun.SetBool("isFiring", true);
        audioShoot.Play();
    }

    private IEnumerator DestroyBulletAfterTime(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(bullet);
    }

    // Méthode pour vérifier l'XP et augmenter le nombre de balles tirées
    private void CheckXPForWeaponUpgrade()
    {
        // Si l'XP atteint un multiple de 800, augmenter le nombre de balles par tir
        if (GameManager.xp >= 800 && GameManager.xp < 1400)
        {
            bulletsPerShot = 4; // 2 balles par tir
            
        }
        else if (GameManager.xp >= 1400)
        {
            bulletsPerShot = 6; // 3 balles par tir
          
        }
        else if (GameManager.xp >= 2500)
        {
            bulletsPerShot = 10; // 3 balles par tir
          
        }
        else if (GameManager.xp >= 3300)
        {
            bulletsPerShot = 15; // 3 balles par tir
        
        }
    }


}
