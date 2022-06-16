using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dto;
namespace Dto
{
   public class AllWor
    {
        public ConstraintsforeigenworkerDto co { get; set; }
        public Foreign_workerDto f { get; set; }
        public Days_of_wor2Dto d { get; set; }
        public AllWor()
        {
            co = new ConstraintsforeigenworkerDto();
            f = new Foreign_workerDto();
            d = new Days_of_wor2Dto();
        }
        public ConstraintsforeigenworkerDto getCO()
        {
            return co;
        }
        public Foreign_workerDto getO()
        {
            return f;
        }
        public Days_of_wor2Dto getDO()
        {
            return d;
        }
        public void setCO(ConstraintsforeigenworkerDto val)
        {
            co = val;
        }
        public void setF(Foreign_workerDto val)
        {
            f = val;
        }
        public void setCDO(Days_of_wor2Dto val)
        {
            d = val;
        }
    }
}
