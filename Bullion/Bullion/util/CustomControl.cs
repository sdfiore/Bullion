using System.Windows.Controls;
using System.Windows.Media;

namespace Bullion.util
{
    class CustomControl
    {
        // Creates a green label representing an increase in an account resource
        public Label DepositLabel(double value)
        {
            Label depositLabel = new Label
            {
                Height = 34,
                FontSize = 18,
                FontFamily = new FontFamily("Segoe UI Light"),
                Foreground = new SolidColorBrush(Color.FromRgb(41, 158, 52)),
                Content = "+ $" + value.ToString("N")
            };
            return depositLabel;
        }

        // Creates a red label representing a decrease in an account resource.
        public Label WithdrawLabel(double value)
        {
            Label withdrawLabel = new Label
            {
                Height = 34,
                FontSize = 18,
                FontFamily = new FontFamily("Segoe UI Light"),
                Foreground = new SolidColorBrush(Color.FromRgb(178, 28, 28)),
                Content = "- $" + value.ToString("N")
            };
            return withdrawLabel;
        }

        // Creates a line to separate each label entry.
        public Separator LineSeparator()
        {
            Separator separator = new Separator
            {
                Opacity = 0.4,
                Background = new SolidColorBrush(Color.FromRgb(12, 41, 68))
            };
            return separator;
        }
    }
}
