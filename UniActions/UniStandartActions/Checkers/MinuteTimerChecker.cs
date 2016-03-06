﻿using HierarchicalData;
using System;
using System.Xml.Serialization;
using UniActionsClientIntefaces;

namespace UniStandartActions.Checkers
{
    [Serializable]
    public class MinuteTimerChecker : ICustomChecker
    {
        public DateTime DateTimeFinish { get; set; }

        [XmlIgnore]
        [HumanFriendlyName("Минут")]
        public double Minutes
        {
            get
            {
                return (DateTimeFinish - DateTime.Now).TotalMinutes;
            }
        }

        private bool _shown;

        [XmlIgnore]
        public bool IsCanDoNow
        {
            get
            {
                if (_shown) return false;
                return _shown = (DateTime.Now.Minute == DateTimeFinish.Minute &&
                    DateTime.Now.Hour == DateTimeFinish.Hour &&
                    DateTime.Now.Day == DateTimeFinish.Day &&
                    DateTime.Now.Month == DateTimeFinish.Month &&
                    DateTime.Now.Year == DateTimeFinish.Year);
            }
        }

        [XmlIgnore]
        public string Name
        {
            get { return "Таймер"; }
        }

        public bool BeginUserSettings()
        {
            var form = new MinuteTimerCheckerView();
            form.Minutes = DateTimeFinish > DateTime.Now ? (decimal)(DateTimeFinish - DateTime.Now).TotalMinutes : 5;
            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                DateTimeFinish = DateTime.Now.AddMinutes((double)form.Minutes);
                return true;
            }
            return false;
        }

        public void Refresh() { }

        [XmlIgnore]
        public bool AllowUserSettings { get { return true; } }
    }
}
