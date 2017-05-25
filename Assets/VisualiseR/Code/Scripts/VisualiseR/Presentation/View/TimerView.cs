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
            Show(true);
        }

        public void StartTimer()
        {
            stop = false;
            Update();
            StartCoroutine(runTimer());
            Show(true);
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
            ShowTime();
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
                ShowTime();
                yield return new WaitForSeconds(1f);
            }
        }

        private void ShowTime()
        {
            _timerText.text = TimeUtil.FormatTime(_timeLeft);
        }


        public void Show(bool isShown)
        {
            _timerText.gameObject.SetActive(isShown);
        }
    }
}