using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AviaTicketsWpfApplication.Fundamentals
{
    public static class GlobalCommands
    {
        public static RoutedUICommand HyperlinkCommand { get; private set; }

        static GlobalCommands()
        {
            HyperlinkCommand = new RoutedUICommand("HyperlinkCommand", "HyperlinkCommand", typeof(GlobalCommands));

            // Register CommandBinding for all windows.
            CommandManager.RegisterClassCommandBinding(typeof(FrameworkElement), new CommandBinding(HyperlinkCommand, HyperlinkCommandExecuted, HyperlinkCommandCanExecute));
        }

        private static void HyperlinkCommandCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (e.Parameter == null)
                return;

            var uri = new Uri(e.Parameter.ToString());

            e.CanExecute = uri.IsAbsoluteUri;
        }

        private static void HyperlinkCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            e.Handled = true;

            Process.Start(e.Parameter.ToString());
        }
    }
}
