using Dal;
using Dto;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Bll
{
    public class Constraintsoldbll
    {
        public int AddConstraintsold(ConstraintsoldDto cw)
        {
            int id;
            using (dbEntities db = new dbEntities())
            {
                db.Constraintsold.Add(cw.DtoToDal());
                db.SaveChanges();
                int id2 = db.Constraintsold.FirstOrDefault(user => user.mail == cw.mail).conoID;
                id = id2;
            }
            return id;
        }
        public RequestResult GetAllConstraintsoldDto()
        {
            using (dbEntities db = new dbEntities())
            {
                List<ConstraintsoldDto> lst = new List<ConstraintsoldDto>();
                foreach (var item in db.Constraintsold.ToList())
                    lst.Add(ConstraintsoldDto.DalToDto(item));
                return new RequestResult()
                {
                    Data = lst,
                    Message = "succesfull",
                    status = true
                };
            }
        }
        public void AddConstraintsoldDto(ConstraintsoldDto co)
        {
            using (dbEntities db = new dbEntities())
            {
                db.Constraintsold.Add(co.DtoToDal());
                db.SaveChanges();
            }
        }
        public Object Login(long password, string mail)
        {
            using (dbEntities db = new dbEntities())
            {
                OldDto o = null;
                Days_of_oldDto d = null;
                ConstraintsoldDto co = ConstraintsoldDto.DalToDto(db.Constraintsold.FirstOrDefault(c => c.passwardold == password && c.mail == mail));
                if (co != null)
                {
                    o = OldDto.DalToDto(db.old.FirstOrDefault(c => c.conoID == co.conoID));
                    d = Days_of_oldDto.DalToDto(db.days_of_old2.FirstOrDefault(c => c.conoID == co.conoID));
                }

                AllOld a = new AllOld();
                a.co = co;
                a.o = o;
                a.d = d;

                if (a.co != null || a.o != null || a.d != null)
                    return a;
                ConstraintsforeigenworkerDto cw = ConstraintsforeigenworkerDto.DalToDto(db.Constraintsforeigenworker.FirstOrDefault(c => c.passwardwor == password && c.mail == mail));
                Foreign_workerDto f = Foreign_workerDto.DalToDto(db.Foreign_worker.FirstOrDefault(c => c.confid == cw.confid));
                //Days_of_wor2Dto dw = Days_of_wor2Dto.DalToDto(db.days_of_wor2.FirstOrDefault(c => c.confid == cw.confid));
                AllWor b = new AllWor();
                b.co = cw;
                b.f = f;
                //b.d = dw;
                return b;
            }
        }
        public void Update(int idold, ConstraintsoldDto co)
        {
            using (dbEntities db = new dbEntities())
            {

                foreach (Constraintsold old in db.Constraintsold)

                {
                    if (old.conoID == idold)
                    {
                        old.age = co.age;
                        old.addressold = co.addressold;
                        old.gender = co.gender;
                        old.idcity = co.idcity;
                        old.languageold = co.languageold;
                        old.Level_of_functioningo = co.Level_of_functioningo;
                        old.mail = co.mail;
                        old.passwardold = co.passwardold;
                    }

                }
                db.SaveChanges();

            }
        }
        public void Deletes(int id)
        {
            using (dbEntities db = new dbEntities())
            {

                foreach (Constraintsold old in db.Constraintsold)

                {
                    if (old.conoID == id)
                    {
                        old.age = null;
                        old.addressold = null;
                        old.gender = null;
                        old.idcity = 0;
                        old.languageold = null;
                        old.Level_of_functioningo = 0;
                        old.mail = null;
                        old.passwardold = null;
                    }

                }
                db.SaveChanges();

            }
        }
    }
}
