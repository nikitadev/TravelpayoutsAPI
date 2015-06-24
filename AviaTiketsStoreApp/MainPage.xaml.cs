﻿using System;
using AviaTicketsStoreApp.ViewModel;

namespace AviaTicketsStoreApp
{
    public sealed partial class MainPage
    {
        /// <summary>
        /// Gets the view's ViewModel.
        /// </summary>
        public MainViewModel Vm
        {
            get
            {
                return (MainViewModel)DataContext;
            }
        }

        public MainPage()
        {
            InitializeComponent();
        }

        protected override void LoadState(object state)
        {
            var casted = state as MainPageState;

            if (casted != null)
            {
                Vm.Load(casted.LastVisit);
            }
        }

        protected override object SaveState()
        {
            return new MainPageState
            {
                LastVisit = DateTime.Now
            };
        }
    }

    public class MainPageState
    {
        public DateTime LastVisit
        {
            get;
            set;
        }
    }
}