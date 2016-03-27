﻿using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using UniActionsCore.ScenarioCreation;
using UniActionsUI.ScenarioCreation;

namespace UniActionsUI
{
    /// <summary>
    /// Interaction logic for WCreateAction.xaml
    /// </summary>
    public partial class EditScenarioView : UserControl, ControlsHelper.IRefreshable
    {
        public EditScenarioView()
        {
            InitializeComponent();

            ControlsHelper.AppendOnlyInteger(tbIndex, int.MinValue, int.MaxValue);

            SetScenario(null);

            this.tbName.TextChanged += (o, e) => ProcessOkEnable();

            this.cbSelectCategory.SelectionChanged += (o, e) =>
            {
                if (cbSelectCategory.SelectedItem != null)
                    tbCategory.Text = cbSelectCategory.SelectedItem.ToString();
            };

            this.cbSelectCategory.DropDownOpened += (o, e) =>
            {
                cbSelectCategory.SelectedItem = null;
            }; // crutch

            this.tbCategory.TextChanged += (o, e) =>
                ProcessOkEnable();

            this.bUseInPool.BoolChanged += (o) =>
                ProcessOkEnable();

            this.bUseInServer.BoolChanged += (o) =>
                ProcessOkEnable();

            this.tbIndex.TextChanged += (o, e) =>
                ProcessOkEnable();

            this.bUseOnOffState.BoolChanged += (o) =>
                ProcessOkEnable();

            this.btCancel.Click += (o, e) =>
                SetScenario(_tempItem);

            this.btCreate.Click += (o, e) =>
            {
                var res = App.Uni.ScenariosPool.CheckScenario(Scenario);

                if (res.Value)
                {
                    var wasBusy = _tempItem.IsBusyNow;
                    _tempItem.KillDispatcher();
                    _tempItem.ActionBag = Scenario.ActionBag;
                    _tempItem.Category = Scenario.Category;
                    _tempItem.IsActive = Scenario.IsActive;
                    _tempItem.UseOnOffState = Scenario.UseOnOffState;
                    _tempItem.Name = Scenario.Name;
                    _tempItem.ServerCommand = Scenario.ServerCommand;
                    _tempItem.UseServerThreading = Scenario.UseServerThreading;
                    _tempItem.Index = Scenario.Index;
                    if (wasBusy)
                        _tempItem.ExecuteAsync(null);
                    DisableButtons();
                    App.Uni.CommitChanges();
                    if (Applying != null)
                        Applying(this, new EventArgs());
                    SetScenario(_tempItem);
                }
            };

            this.Loaded += (o, e) => DisableButtons();
        }

        public void Refresh()
        {
            SetScenario(_tempItem);
        }

        private Scenario _tempItem;
        public void SetScenario(Scenario scenario)
        {
            if (scenario == null)
            {
                this.DataContext = new EditScenarioViewContext(null);
            }
            else
            {
                _tempItem = scenario;
                Scenario = scenario.Clone();

                this.DataContext = new EditScenarioViewContext(Scenario);

                if (scenario.ActionBag.Action is DoubleComplexAction)
                {
                    var scenarioView = new DoubleScenarioActionView();
                    scenarioView.Changed += (o, e) => EnableButtons();
                    scenarioView.ActionBag = Scenario.ActionBag;
                    this.borderScenarioHolder.Child = scenarioView;
                }
                else
                {
                    var scenarioView = new SingleActionScenarioView();
                    scenarioView.Changed += (o, e) => EnableButtons();
                    scenarioView.ActionBag = Scenario.ActionBag;
                    this.borderScenarioHolder.Child = scenarioView;
                }
            }
            DisableButtons();
        }

        public Scenario Scenario { get; private set; }

        private void ProcessOkEnable()
        {
            if (Scenario == null)
                DisableButtons();
            else
            {
                var result = App.Uni.ScenariosPool.CheckScenario(Scenario);
                if (result.Value)
                {
                    tbStatus.Text = "";
                    EnableButtons();
                }
                else
                {
                    var str = "";
                    foreach (var warning in result.Warnings)
                        str += warning.Message + "\r\n";

                    tbStatus.Text = str;

                    DisableOkButton();
                }
            }
        }

        private void EnableButtons()
        {
            this.btCreate.Visibility = btCancel.Visibility = Visibility.Visible;
        }

        public void DisableButtons()
        {
            this.btCreate.Visibility = btCancel.Visibility = Visibility.Collapsed;
        }

        private void DisableOkButton()
        {
            btCreate.Visibility = Visibility.Collapsed;
        }

        public event Action<object, EventArgs> Applying;
    }

    [ValueConversion(typeof(string), typeof(int))]
    public class StringToIntConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((string)value == "-" || string.IsNullOrEmpty((string)value))
                value = "-1";
            return int.Parse((string)value);
        }
    }
}