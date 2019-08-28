using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace WpfApplication1
{
    /// <summary>
    /// https://joshsmithonwpf.wordpress.com/2007/09/14/modifying-the-auto-tooltip-of-a-slider/
    /// </summary>
    public class SeekBar : Slider
    {
        private ToolTip _autoToolTip;
        public ToolTip AutoToolTip
        {
            get
            {
                if (_autoToolTip == null)
                {
                    var field = typeof(Slider).GetField(nameof(_autoToolTip),
                        BindingFlags.NonPublic | BindingFlags.Instance);
                    _autoToolTip = field?.GetValue(this) as ToolTip;
                }

                return _autoToolTip;
            }
        }

        public static readonly DependencyProperty AutoToolTipFormatProperty = DependencyProperty.Register("AutoToolTipFormat", typeof(string), typeof(SeekBar), new PropertyMetadata(default(string)));
        public string AutoToolTipFormat
        {
            get => (string)GetValue(AutoToolTipFormatProperty);
            set => SetValue(AutoToolTipFormatProperty, value);
        }

        protected override void OnThumbDragStarted(DragStartedEventArgs e)
        {
            base.OnThumbDragStarted(e);
            FormatAutoToolTipContent();
        }

        protected override void OnThumbDragDelta(DragDeltaEventArgs e)
        {
            base.OnThumbDragDelta(e);
            FormatAutoToolTipContent();
        }

        protected void FormatAutoToolTipContent()
        {
            if (!string.IsNullOrEmpty(AutoToolTipFormat))
            {
                AutoToolTip.Content = string.Format(AutoToolTipFormat, AutoToolTip.Content);
            }
        }
    }
}
