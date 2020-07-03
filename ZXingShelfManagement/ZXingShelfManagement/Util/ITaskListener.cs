using System;
using System.Collections.Generic;
using System.Text;

namespace ZXingShelfManagement
{
    public interface ITaskListener
    {
        void OnSuccess();

        void OnFailure();
    }
}
