using System;
using System.Collections.Generic;
using System.Text;

namespace Thitipong.Upload.Service
{
    interface IFileConverter<T> where T : class
    {
        IEnumerable<T> Convert();

    }
}
