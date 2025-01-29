using UnityEngine;

public class Bullet : MonoBehaviour
{
    public static int damage = 40;

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Target"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.TakeDamage(damage);
            Debug.Log("Touché : " + collision.gameObject.name);
            Destroy(gameObject);

        }
    }


}
