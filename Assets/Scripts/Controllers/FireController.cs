using System;
using Components;
using UnityEngine;

namespace Controllers
{
    public sealed class FireController : MonoBehaviour
    {
        [SerializeField]
        private GameObject characterGameObject;
        
       
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                var weaponComponent = characterGameObject.GetComponentInChildren<WeaponBase>();
                if (weaponComponent != null && weaponComponent.IsCanAttack)
                {
                    weaponComponent.Attack();
                }
            }
        }
    }
}