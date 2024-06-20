using Components;
using UnityEngine;

namespace Animations.VFX
{
    public sealed class ShootWeaponVFXAnimationEvent : MonoBehaviour
    {
        [SerializeField] private BulletWeaponComponent _weaponComponent;

        private void OnEnable()
        {
            _weaponComponent.OnAttack += PlayShootVFX;
        }

        private void OnDisable()
        {
            _weaponComponent.OnAttack   -= PlayShootVFX;
        }

        private void PlayShootVFX()
        {
            Debug.Log("[ShootWeaponVFXAnimationEvent]: ShootVFX()");
        }
    }
}