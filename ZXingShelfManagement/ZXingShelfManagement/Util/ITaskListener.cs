using System;
using System.Collections.Generic;
using System.Text;
using static ZXingShelfManagement.TaskHttpGet;

namespace ZXingShelfManagement
{
    public interface ITaskListener
    {
        void OnSuccess();

        void OnFailure();
    }
}
