using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bulletPrefab;

    public GameObject weaponPrefab;

    private Transform _firePoint;

    public LineRenderer lineRenderer;

    public void Awake()
    {
        _firePoint = transform.Find("FirePoint");
    }
 
    public void Shoot()
    {
        if (bulletPrefab != null && _firePoint != null && weaponPrefab != null)
        {
            GameObject myBullet = Instantiate(bulletPrefab, _firePoint.position, Quaternion.identity) as GameObject;

            Bullet bulletComponent = myBullet.GetComponent<Bullet>();

            if (weaponPrefab.transform.localScale.x < 0f)
            {
                bulletComponent.direction = Vector2.left;
            }
            else
            {
                bulletComponent.direction = Vector2.right;
            }

        }
    }

    public IEnumerator ShootWithRayCast()
    {
        if (lineRenderer != null)
        {

            RaycastHit2D hitInfo = Physics2D.Raycast(_firePoint.position, _firePoint.right);

            if (hitInfo)
            {
                lineRenderer.SetPosition(0, _firePoint.position);
                lineRenderer.SetPosition(1, hitInfo.point);
            }
            else
            {
                lineRenderer.SetPosition(0, _firePoint.position);
                lineRenderer.SetPosition(1, hitInfo.point + Vector2.right * 100);
            }

            lineRenderer.enabled = true;

            yield return null;

            lineRenderer.enabled = false;
        }
    }
}
