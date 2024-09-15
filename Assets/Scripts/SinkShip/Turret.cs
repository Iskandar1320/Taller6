using System.Collections.Generic;
using UnityEngine;

namespace SinkShip
{
    public class Turret : MonoBehaviour
    {
        [SerializeField] List<Transform> turretBarrels;
        [SerializeField] GameObject proyectilePrefab;
        [SerializeField] float reloadDelay = 1.0f;
        [SerializeField] float currentDelay = 0;
        [SerializeField] AudioSource shootAudioSource;

        private bool canShoot = true;
        private Collider2D[] shipColliders;

        private void Awake()
        {
            shipColliders = GetComponentsInParent<Collider2D>();
        }

        private void Update()
        {
            if (canShoot == false)
            {
                currentDelay -= Time.deltaTime;
                if (currentDelay <= 0)
                {
                    canShoot = true;
                }
            }
        }

        public void Shoot()
        {
            if (canShoot)
            {
                canShoot = false;
                currentDelay = reloadDelay;

                foreach (var barrel in turretBarrels)
                {
                    GameObject proyectile = Instantiate(proyectilePrefab);
                    proyectile.transform.position = barrel.position;
                    proyectile.transform.localRotation = barrel.rotation;
                    proyectile.GetComponent<BulletScript>().Initialize();
                    foreach (var collider in shipColliders)
                    {
                        Physics2D.IgnoreCollision(proyectile.GetComponent<Collider2D>(), collider);
                    }
                }
                if (shootAudioSource != null) // Alejo Aca iria lo del Audio de Disparo
                {
                    shootAudioSource.Play();
                }
            }
        }
    }
}
