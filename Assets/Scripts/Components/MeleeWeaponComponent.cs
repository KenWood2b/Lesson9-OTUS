using System;
using Components;
using UnityEngine;

namespace Game.Engine
{
    public sealed class MeleeWeaponComponent : WeaponBase
    {
        [SerializeField] private int damage = 1;
        [SerializeField] private Transform meleePoint;
        [SerializeField] private float meleeRadius = 0.5f;
        [SerializeField] private LayerMask layerMask;
        
        public override event Action OnAttack;
        public override void Attack()
        {
            /*
             * проверка возможно атаковать данным оружием,
             *  то попытаться осуществить атаку и нанести урон.
             *  оповещение всех подписавшихся после проведение атаки
             */

            if (IsCanAttack)
            {
                TryDealDamage();
                OnAttack?.Invoke();
            }
        }

        private void TryDealDamage()
        {
            /*
             * попытка нанести урон
             * посредством использования Physics.OverlapSphereNonAlloc и имеющихся данных
             * найти объекты которые могут быть повреждены ударом зомби
             * урон должен получить только один
             */
            Collider[] hitColliders = Physics.OverlapSphere(meleePoint.position, meleeRadius, layerMask);

            foreach (var hitCollider in hitColliders)
            {
                HealthComponent health = hitCollider.GetComponentInParent<HealthComponent>();
                if (health != null)
                {
                    
                    health.TakeDamage(damage);
                    break;
                }
                
            }
    }
        public override bool IsCanAttack => true;
        
        //удобно использовать для Debuging 
        private void OnDrawGizmos()
        {
            if (this.meleePoint != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(this.meleePoint.position, this.meleeRadius);
            }
        }
    }
}