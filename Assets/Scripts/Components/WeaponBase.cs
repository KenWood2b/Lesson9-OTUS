using System;
using UnityEngine;

namespace Components
{
    public abstract class WeaponBase : MonoBehaviour
    {
        public abstract event Action OnAttack;

        public abstract void Attack();

        public virtual bool IsCanAttack => true;
    }
}