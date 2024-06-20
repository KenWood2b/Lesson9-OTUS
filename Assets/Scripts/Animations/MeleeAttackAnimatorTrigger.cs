using Animations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Animations
{
    public class MeleeAttackAnimatorTrigger : AnimatorTriggerBase
    {
        public override void SetTrigger()
        {
            _animator.SetTrigger("Attack");
        }
    }

}


