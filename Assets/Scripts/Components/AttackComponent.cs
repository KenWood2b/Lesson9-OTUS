using Animations;
using Game.Engine;
using UnityEngine;

namespace Components
{

    public class AttackComponent : MonoBehaviour
    {
        /*
         * Класс должен быть полностью переработан
         *  - кусок "OldCode" оставлен чтобы компилировался и работал исходный проект, если не нужен может быть удален
         *  - кусок "NewFunctionality" должен быть в варианте после рефаторинга
         * Включая добавление необходимых переменных и удаление не нужных
         */


        #region NewFunctionality

        [SerializeField] private AnimatorTriggerBase _animatorTrigger;
        [SerializeField] private WeaponBase _weaponComponent;

        private bool _attackRequested;
        private float _attackCooldown = 1f;
        private float _lastAttackTime;

        public void SetRequiredAttack(bool status)
        {
            /*
             * Передача или прекращения запроса на проведение аттак
             */
            _attackRequested = status;
        }

        private void FixedUpdate()
        {
            /*
             * должен запускать анимацию аттаки, если есть запрос на проведение атаки, а также прошло время заданное время с момента последней атаки
             * при чем при временном прекращение атаки, запущенный таймер продолжает идти
             */
            ;
            if (_attackRequested && Time.time - _lastAttackTime >= _attackCooldown)
            {
                
                if (_weaponComponent != null && _weaponComponent.IsCanAttack)
                {
                    _weaponComponent.Attack();
                    _animatorTrigger.SetTrigger();
                    _lastAttackTime = Time.time;
                }
            }
        }

        #endregion

    }
}