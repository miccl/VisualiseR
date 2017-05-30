using System.Collections;
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

        private float _timeLeft;
        public float _timeFrom { get; private set; }
        public bool stop = true;

        private float _minutes;
        private float _seconds;

        private Text _timerText;

        protected override void Awake()
        {
            base.Awake();
            _timerText = transform.GetComponentInChildren<Text>();
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
            _timeLeft = _timeFrom;
            DisplayTime();
        }

        void Update()
        {
            if (stop) return;
            _timeLeft -= Time.deltaTime;

            if (_timeLeft < 0)
            {
                StopTimer();
                _timeLeft = 0;
                TimerRunDownSignal.Dispatch();
            }
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
    }
}