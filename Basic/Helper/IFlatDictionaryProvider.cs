using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuildExeBasic.Helper
{
    public interface IFlatDictionaryProvider
    {
        Dictionary<string, string> Execute(object @object, string prefix = "");
    }
}
