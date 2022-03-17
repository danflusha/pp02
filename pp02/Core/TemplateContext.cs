using System;
using pp02.MVVM.Model;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pp02.Core
{
    internal class TemplateContext
    {
        private static Model_pp02Entities1 context;
        public static Model_pp02Entities1 GetContext()
        {
            if (context == null)
            {
                context = new Model_pp02Entities1();
            }
            return context;
        }
    }
}
