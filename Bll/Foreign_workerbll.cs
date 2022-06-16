using Dal;
using Dto;
using System.Collections.Generic;
using System.Linq;
namespace Bll
{
    public class Foreign_workerbll
    {
        public RequestResult GetAllforeign_worer()
        {
            using (dbEntities db = new dbEntities())
            {
                List<Foreign_workerDto> lst = new List<Foreign_workerDto>();
                foreach (var item in db.Foreign_worker.ToList())
                    lst.Add(Foreign_workerDto.DalToDto(item));
                return new RequestResult()
                {
                    Data = lst,
                    Message = "succesfull",
                    status = true
                };
            }
        }
        public void Addforeign_worer(Foreign_workerDto fw)
        {
            using (dbEntities db = new dbEntities())
            {
                db.Foreign_worker.Add(fw.DtoToDal());
                db.SaveChanges();
            }
        }
        public void Update(int idwor, Foreign_workerDto f)
        {
            using (dbEntities db = new dbEntities())
            {

                foreach (Foreign_worker fo in db.Foreign_worker)

                {
                    if (fo.idwor == idwor)
                    {
                        fo.confid = f.confid;
                        fo.lnwor = f.lnwor;
                        fo.fnwor=f.fnwor;
                      
                        fo.ischeck = f.ischeck;
                        
                    }

                }
                db.SaveChanges();

            }
        }
        public Foreign_workerDto Login(ConstraintsforeigenworkerDto iduser)
        {
            using (dbEntities db = new dbEntities())
            {
                Foreign_workerDto co = Foreign_workerDto.DalToDto(db.Foreign_worker.FirstOrDefault(c => c.confid == iduser.confid));


                return co;

            }
        }
        public void Delete(int idwor)
        {
            using (dbEntities db = new dbEntities())
            {

                foreach (Foreign_worker fo in db.Foreign_worker)

                {
                    if (fo.idwor == idwor)
                    {
                        fo.confid = null;
                        fo.lnwor = null;
                        fo.fnwor = null;

                        fo.ischeck = null;

                    }

                }
                db.SaveChanges();

            }
        }
    }
}
