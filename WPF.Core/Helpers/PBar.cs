using System;
using System.Windows.Controls;

namespace WPF.Core.Helpers
{
    public class PBar
    {
        public ProgressBar P;
        private delegate void UpdateProgressBarDelegate(System.Windows.DependencyProperty dp, Object value);
        private readonly UpdateProgressBarDelegate _updatePbDelegate;
        public double Value;
        public PBar(ProgressBar pb)
        {
            P = pb;
            _updatePbDelegate = P.SetValue;
        }

        public void IncPB()
        {
            Value++;
            System.Windows.Application.Current.Dispatcher.Invoke(_updatePbDelegate, System.Windows.Threading.DispatcherPriority.Background, new object[] { System.Windows.Controls.ProgressBar.ValueProperty, Value });
        }
    }
}
