using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal;
using Dto;

  

namespace Bll
    {
        public class Placementbll
    {
            public RequestResult GetAllplacement()
            {
                using (dbEntities db = new dbEntities())

                {
                    List<PlacementDto> lst = new List<PlacementDto>();
                    foreach (var item in db.Placement.ToList())
                    {
                        lst.Add(PlacementDto.DalToDto(item));
                    }
                    return new RequestResult()
                    {
                        Data = lst,
                        Message = "succesfull",
                        status = true
                    };
                }
            }

            public void Addplacement(PlacementDto p)
            {
                using (dbEntities db = new dbEntities())
                {
                    db.Placement.Add(p.DtoToDal());
                    db.SaveChanges();
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
    }
    }


