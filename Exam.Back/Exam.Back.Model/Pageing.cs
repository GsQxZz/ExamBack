using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Back.Model
{
   public class Pageing<T>
    {
        public T Data { get; set; }

        public int Count { get; set; }
    }
}
