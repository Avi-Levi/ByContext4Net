using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Client
{
    public static class AsyncHelper
    {
        private static readonly WindowsFormsSynchronizationContext UIContext = new WindowsFormsSynchronizationContext();

        public static void InvokeOnBackground(Action action)
        {
            action.BeginInvoke(ar => action.EndInvoke(ar), null);
        }

        public static void InvokeOnUI(Action action)
        {
            UIContext.Send(o => action(), null);
        }
    }
}
