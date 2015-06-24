using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AviaTicketsWpfApplication.Behaviors
{
    public sealed class FrameBehavior : DependencyObject
    {
        public static readonly DependencyProperty TitleProperty = 
            DependencyProperty.RegisterAttached("Title", typeof(string), typeof(FrameBehavior), new PropertyMetadata(default(string), OnTitleChange));

        public static string GetTitle(Frame frame)
        {
            var labelTitle = (Label)frame.Template.FindName("PART_Title", frame);
            return (string)labelTitle.Content;
        }

        public static void SetTitle(Frame frame, string title)
        {
            var labelTitle = (Label)frame.Template.FindName("PART_Title", frame);
            labelTitle.Content = title;
        }

        private static void OnTitleChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var frame = d as Frame;
            if (frame != null)
            {
                frame.ApplyTemplate();

                var labelTitle = (Label)frame.Template.FindName("PART_Title", frame);

                if (labelTitle == null)
                    return;

                labelTitle.Content = e.NewValue;
            }
        }
    }
}
