using UnityEngine;

namespace Animations
{
    public sealed class ShootAnimatorTrigger : AnimatorTriggerBase
    {
        
        public override void SetTrigger()
        {
            _animator.SetTrigger("Shoot");
        }
    }
}