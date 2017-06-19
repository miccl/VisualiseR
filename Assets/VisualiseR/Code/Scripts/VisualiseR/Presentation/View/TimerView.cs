using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;
using UnityEngine;
using UnityEngine.UI;
using VisualiseR.Util;

namespace VisualiseR.Presentation
{
    public class TimerView : View
    {
        public Signal TimerRunDownSignal = new Signal();
        public Signal<bool> ShowTimerSignal = new Signal<bool>();

        public ClockType _type = ClockType.Countdown;
        private float _timeLeft;
        public float _timeFrom { get; private set; }
        public bool stop = true;

        private float _minutes;
        private float _seconds;

        private Text _timerText;

        private GvrAudioSource audioSource;
        private Animation _animation;

        protected override void Awake()
        {
            base.Awake();
            _timerText = transform.GetComponentInChildren<Text>();
            audioSource = GetComponent<GvrAudioSource>();
            _animation = GetComponent<Animation>();
        }

        protected override void Start()
        {
            base.Start();
            SetTimer(_timeFrom);
        }

        public void SetTimer(float timeInSeconds)
        {
            _timeFrom = timeInSeconds;
            _timeLeft = timeInSeconds;
            _timerText.text = TimeUtil.FormatTime(timeInSeconds);
        }

        public void StartTimer()
        {
            if (_type.Equals(ClockType.Countdown) && (_timeFrom <= 0 || _timeLeft <= 0))
            {
                return;
            }

            stop = false;
            Update();
            StartCoroutine(runTimer());
        }

        public void StopTimer()
        {
            stop = true;
            StopCoroutine(runTimer());
        }

        public void ResetTimer()
        {
            StopTimer();
            switch (_type)
            {
                case ClockType.Stopwatch:
                    _timeLeft = 0;
                    break;
                default:
                    _timeLeft = _timeFrom;
                    break;
            }
            DisplayTime();
        }

        void Update()
        {
            if (stop) return;
            switch (_type)
            {
                case ClockType.Stopwatch:
                    _timeLeft += Time.deltaTime;
                    break;
                default:
                    _timeLeft -= Time.deltaTime;
                    break;
            }


            if (_timeLeft < 0)
            {
                TimerRunDown();
            }
        }

        private void TimerRunDown()
        {
            StopTimer();
            _timeLeft = 0;
            audioSource.Play();
            _animation.Play();
            TimerRunDownSignal.Dispatch();
            StartCoroutine(StopAfterSeconds());
        }

        private IEnumerator StopAfterSeconds()
        {
            yield return new WaitForSeconds(5f);
            audioSource.Stop();
            _animation.Stop();
        }


        private IEnumerator runTimer()
        {
            while (!stop)
            {
                DisplayTime();
                yield return new WaitForSeconds(1f);
            }
        }

        private void DisplayTime()
        {
            _timerText.text = TimeUtil.FormatTime(_timeLeft);
        }


        public void Show(bool isShown)
        {
            ShowTimerSignal.Dispatch(isShown);
        }

        public void ChangeClockType(ClockType type)
        {
            if (_type.Equals(type))
            {
                return;
            }

            _type = type;
            ResetTimer();
        }
    }
}