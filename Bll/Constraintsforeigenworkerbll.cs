using Dal;
using Dto;
using System.Collections.Generic;
using System.Linq;

namespace Bll
{
    public class Constraintsforeigenworkerbll
    {
        public RequestResult GetAllConstraintsforeigenworker()
        {
            using (dbEntities db = new dbEntities())
            {
                List<ConstraintsforeigenworkerDto> lst = new List<ConstraintsforeigenworkerDto>();
                foreach (var item in db.Constraintsforeigenworker.ToList())
                    lst.Add(ConstraintsforeigenworkerDto.DalToDto(item));
                return new RequestResult()
                {
                    Data = lst,
                    Message = "succesfull",
                    status = true
                };
            }
        }
        public int AddConstraintsforeigenworker(ConstraintsforeigenworkerDto cw)
        {
            int id;
            using (dbEntities db = new dbEntities())
            {
                db.Constraintsforeigenworker.Add(cw.DtoToDal());
                db.SaveChanges();
                cw = ConstraintsforeigenworkerDto.DalToDto(db.Constraintsforeigenworker.FirstOrDefault(user => user.mail == cw.mail));

                id = cw.confid;
            }
            return id;
        }
        public void Update(int idwor, ConstraintsforeigenworkerDto co)
        {
            using (dbEntities db = new dbEntities())
            {

                foreach (Constraintsforeigenworker wor in db.Constraintsforeigenworker)

                {
                    if (wor.confid == idwor)
                    {
                        wor.age_of_o = co.age_of_o;
                        wor.addressfg = co.addressfg;
                        wor.gender = co.gender;
                        wor.idcity = co.idcity;
                        wor.languagefw = co.languagefw;
                        wor.Level_of_functioningfg = wor.Level_of_functioningfg;
                        wor.mail = co.mail;
                        wor.passwardwor = co.passwardwor;
                    }

                }
                db.SaveChanges();

            }
        }
        public AllWor Login(long password, string mail)
        {
            using (dbEntities db = new dbEntities())
            {
                Foreign_workerDto f = null;
                Days_of_wor2Dto d = null;
                ConstraintsforeigenworkerDto co = ConstraintsforeigenworkerDto.DalToDto(db.Constraintsforeigenworker.FirstOrDefault(c => c.passwardwor == password && c.mail == mail));
                if (co != null)
                {
                    f = Foreign_workerDto.DalToDto(db.Foreign_worker.FirstOrDefault(c => c.confid == co.confid));
                    d = Days_of_wor2Dto.DalToDto(db.days_of_wor2.FirstOrDefault(c => c.confid == co.confid));
                }
                AllWor a = new AllWor();
                a.co = co;
                a.f = f;
                a.d = d;
                return a;

            }
        }
        public void Delete(int idwor)
        {
            using (dbEntities db = new dbEntities())
            {

                foreach (Constraintsforeigenworker wor in db.Constraintsforeigenworker)

                {
                    if (wor.confid == idwor)
                    {
                        wor.age_of_o = null;
                        wor.addressfg = null;
                        wor.gender = null;
                        wor.idcity = 0;
                        wor.languagefw = null;
                        wor.Level_of_functioningfg = 0;
                        wor.mail = null;
                        wor.passwardwor = null;
                    }

                }
                db.SaveChanges();

            }
        }
    }
}
