using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ZXingShelfManagement
{
    public interface ICompleted
    {
        ICompleted SetListener(ITaskListener listener);

        void RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e);
    }
}
