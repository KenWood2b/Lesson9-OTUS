using System;
using JetBrains.Annotations;
using UnityEngine;

namespace Components
{
    /*
     * необходимо сделать рефакторинг, чтобы наследовался от WeaponBase
     * функционал измениться не должен, сохранения наименований членов WeaponBase в приорете
     */
    public sealed class BulletWeaponComponent : WeaponBase
    {
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

        public override event Action OnAttack;

        public override void Attack()
        {
            if (!CanFire) return;

            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

            Vector3 force = firePoint.forward * bulletForce;
            bullet.GetComponent<Rigidbody>().AddForce(force, forceMode);

            OnAttack?.Invoke();
        }

        public override bool IsCanAttack => CanFire;
    }
}