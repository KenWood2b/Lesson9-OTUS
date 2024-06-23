#pragma warning disable 414
using System;
using Components;
using Game.Content;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Engine
{
    public enum ZombieState
    {
        IDLE,
        TryCatchEnemy,
        AttackEnemy,
    }
    
    [Serializable]
    public sealed class ZombieController : MonoBehaviour
    {
        [SerializeField] private Transform _enemyTransform;
        [SerializeField] private Transform _zombieTransform;
        [SerializeField] private float _attackDistance = 1.1f;
        [Tooltip("Only for information")]
        [ShowInInspector, ReadOnly] private ZombieState _state;
        private MoveComponentBase _myMoveComponent;
        private AimComponent _myRotateComponent;
        private HealthComponent _enemyHealthComponent;
        private AttackComponent _attackComponent;
        private bool _notAttak;
        private float _distance;

        private void Awake()
        {
            _myMoveComponent = this.GetComponent<MoveComponentBase>();
            _myRotateComponent = this.GetComponent<AimComponent>();
            _enemyHealthComponent = _enemyTransform.GetComponentInParent<HealthComponent>();
            _attackComponent = GetComponent<AttackComponent>();

        }

        public void OnFixedUpdate()
        {
            if (!_enemyTransform) return;

            if (!_enemyHealthComponent.IsAlive)
            {
                IdleState();
                return;
            }

            var directionToTarget = GetDirectionToTarget();
            _distance = directionToTarget.magnitude;
            if (directionToTarget.magnitude > this._attackDistance)
            {
                TryCatchEnemy(directionToTarget);
            }
            else
            {
                AttackEnemy(directionToTarget);
            }
        }

        private Vector3 GetDirectionToTarget()
        {
            Vector3 myPosition = _zombieTransform.position;
            Vector3 targetPosition = _enemyTransform.position;
            Vector3 directionToTarget = targetPosition - myPosition;
            directionToTarget.y = 0;

            // For Debug Only
            Debug.DrawRay(myPosition, directionToTarget, Color.red);
            return directionToTarget;
        }

        private void IdleState()
        {
            _state = ZombieState.IDLE;
            InfoPanel.Instance.SetZombiStatus(_state.ToString());
            _myMoveComponent.MoveDirection = Vector3.zero;
            _myRotateComponent.SetDirection(_zombieTransform.forward);
            _attackComponent.SetRequiredAttack(false);
        }

        private void TryCatchEnemy(Vector3 moveDirection)
        {
           
            _state = ZombieState.TryCatchEnemy;
            InfoPanel.Instance.SetZombiStatus(_state.ToString());
            _myMoveComponent.MoveDirection = moveDirection;
            _myRotateComponent.SetDirection(moveDirection);
            _attackComponent.SetRequiredAttack(false);
            _myMoveComponent.OnFixedUpdate();
            


        }

        private void AttackEnemy(Vector3 moveDirection)
        {

            if (_state != ZombieState.AttackEnemy)
            {
                _state = ZombieState.AttackEnemy;
                InfoPanel.Instance.SetZombiStatus(_state.ToString());
                _myMoveComponent.MoveDirection = Vector3.zero;
                _myRotateComponent.SetDirection(_zombieTransform.forward);


                if (_enemyHealthComponent.IsAlive)
                {
                    
                    _attackComponent.SetRequiredAttack(true);
                }
                else
                {
                    
                    IdleState();
                }
            }
        
        }
    }
}