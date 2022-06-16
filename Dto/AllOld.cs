using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dto;
namespace Dto
{
   public class AllOld
    {
        public ConstraintsoldDto co { get; set; }
        public OldDto o { get; set; }
        public Days_of_oldDto d { get; set; }
        public AllOld()
        {
            co = new ConstraintsoldDto();
            o = new OldDto();
            d = new Days_of_oldDto();
        }
        public ConstraintsoldDto getCO()
        {
            return co;
        }
        public OldDto getO()
        {
            return o;
        }
        public Days_of_oldDto getDO()
        {
            return d;
        }
        public void setCO(ConstraintsoldDto val)
        {
            co = val;
        }
        public void setO(OldDto val)
        {
            o = val;
        }
        public void setCDO(Days_of_oldDto val)
        {
            d = val;
        }
    }
}
