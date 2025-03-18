using UnityEngine;

public class playerShoot : MonoBehaviour
{
    public GameObject BulletPrefab;
    public Transform firePoint;
  
    void Update()
    {
        Shoot();
    }

    void Shoot()
    {
        if (Input.GetKeyDown (KeyCode.Space))
        {
            Instantiate(BulletPrefab, firePoint.position, Quaternion.identity);
            Instantiate(BulletPrefab, firePoint.position , Quaternion.identity);
        }
    }
}
