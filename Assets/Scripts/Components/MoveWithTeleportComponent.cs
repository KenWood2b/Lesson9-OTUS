using System;
using UnityEngine;
using UnityEngine.UIElements;
using Utils;
using Random = UnityEngine.Random;

namespace Components
{
    public sealed class MoveWithTeleportComponent : MoveComponentBase
    {
        public override bool IsWalking => !_isTeleporting && MoveDirection != Vector3.zero;

        [SerializeField] private float _maxLenghtTeleport = 10f;
        [SerializeField] private float _minLenghtTeleport = 4f;
        [SerializeField][Range(0, 10)] private float _minDelayTeleport = 2f;
        [Tooltip("_minDelayJump * (1 + _addToMinDelayTeleport")]
        [SerializeField][Range(0, 2)] private float _addToMinDelayTeleport = 1f;
        [SerializeField] private TeleportComponent _teleportComponent;
        private TimerRandomDelay _teleportTimer;
        private bool _isTeleporting;

        private void Awake()
        {
            _isTeleporting = false;
            float maxDelay = _minDelayTeleport * (1 + _addToMinDelayTeleport);
            _teleportTimer = new TimerRandomDelay(_minDelayTeleport, maxDelay, Time.fixedDeltaTime);
        }

        public override void OnFixedUpdate()
        {
            if (_isTeleporting) return;

            float distance = MoveDirection.magnitude;
            if (distance <= _minLenghtTeleport)
            {
                return;
            }

            if (distance > _maxLenghtTeleport && _teleportTimer.IsTimeFinish())
            {
                _isTeleporting = true;
                float length = Random.Range(_minLenghtTeleport, _maxLenghtTeleport);
                _teleportComponent.MoveByTeleport(AfterTeleporting, MoveDirection, length);
            }
            
        }

        private void AfterTeleporting()
        {
            _isTeleporting = false;
            _teleportTimer.ResetTimer();
        }
    }
}
