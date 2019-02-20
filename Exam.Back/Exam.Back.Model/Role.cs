using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Back.Model
{
  public  class Role
    {
        public int RId { get; set; }

        public string RoleName { get; set; }

        public int Status { get; set; }

        public string Describe { get; set; }

        public string PermissionName { get; set; }
    }
}
