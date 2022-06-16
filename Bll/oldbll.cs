using Dal;
using Dto;
using System.Collections.Generic;
using System.Linq;
namespace Bll
{
    public class oldbll
    {
        public RequestResult GetAllOld()
        {
            using (dbEntities db = new dbEntities())
            {
                List<OldDto> lst = new List<OldDto>();
                foreach (var item in db.old.ToList())
                    lst.Add(OldDto.DalToDto(item));
                return new RequestResult()
                {
                    Data = lst,
                    Message = "succesfull",
                    status = true
                };
            }
        }
        public void AddOldDto(OldDto o)
        {
            using (dbEntities db = new dbEntities())
            {
                db.old.Add(o.DtoToDal());
                db.SaveChanges();
            }
        }
        public OldDto Login(ConstraintsoldDto iduser)
        {
            using (dbEntities db = new dbEntities())
            {
                OldDto co = OldDto.DalToDto(db.old.FirstOrDefault(c => c.conoID == iduser.conoID));


                return co;

            }
        }
        public void Update(int idold, OldDto o)
        {
            using (dbEntities db = new dbEntities())
            {

                foreach (old old in db.old)

                {
                    if (old.idold == idold)
                    {
                        old.conoID = o.conoID;
                        old.lnameold = o.lnameold;
                        old.fnameold = o.fnameold;
                        old.ischeck= o.ischeck;
                        
                    }

                }
                db.SaveChanges();

            }
        }
        public void Deletes(int idold)
        {
            using (dbEntities db = new dbEntities())
            {

                foreach (old old in db.old)

                {
                    if (old.idold == idold)
                    {
                        old.conoID = null;
                        old.lnameold = null;
                        old.fnameold = null;
                        old.ischeck = null;

                    }

                }
                db.SaveChanges();

            }
        }
    }
}
   

