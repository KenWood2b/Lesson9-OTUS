using System;
using JetBrains.Annotations;
using UnityEngine;

namespace Components
{
    /*
     * необходимо сделать рефакторинг, чтобы наследовался от WeaponBase
     * функционал измениться не должен, сохранения наименований членов WeaponBase в приорете
     */
    public sealed class WeaponComponent : MonoBehaviour
    {
        public event Action OnFire;
        
        [SerializeField] private Transform firePoint;
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private bool canFire = true;
        [SerializeField] private float bulletForce = 10;
        [SerializeField] private ForceMode forceMode;
        
        public bool CanFire
        {
            get => canFire;
            set => canFire = value;
        }

        [UsedImplicitly]
        public void Fire()
        {
            if (!CanFire) return;
            
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

            Vector3 force = firePoint.forward * bulletForce;
            bullet.GetComponent<Rigidbody>().AddForce(force, forceMode);
                
            OnFire?.Invoke();
        }
    }
}