﻿using System;

namespace IrregularMachine.Core.Tweens {
    public class TweenFloatEaseOut : ITween{
        public readonly float StartingValue;
        public readonly float FinalValue;
        public readonly int Duration;

        private int _framesSpent;
        private readonly Action<float> _updateCallback;
        private readonly Action<float> _finishedCallback;
        
        public TweenFloatEaseOut(float startingValue, float finalValue, int duration, Action<float> updateCallback = null, Action<float> finishedCallback = null) {
            StartingValue = startingValue;
            FinalValue = finalValue;
            Duration = duration;
            _framesSpent = 0;
            _updateCallback = updateCallback;
            _finishedCallback = finishedCallback;
        }

        public void Update() {
            if (!IsFinished) {
                _framesSpent++;

                _updateCallback?.Invoke(Value);
                
                if (IsFinished) {
                    _finishedCallback?.Invoke(Value);
                }
            }
        }

        public void GoToEnd() {
            _framesSpent = Duration;
            _updateCallback?.Invoke(Value);
            _finishedCallback?.Invoke(Value);
        }

        public bool IsFinished => _framesSpent >= Duration;
        private float TimeInternal => (float) _framesSpent / Duration;
        private float EasedTimeInternal => TimeInternal * (2 - TimeInternal);
        private float Value => StartingValue + (FinalValue - StartingValue) * EasedTimeInternal;
    }
}