using Dal;
using Dto;
using System.Collections.Generic;
using System.Linq;
namespace Bll
{
    public class citybll
    {
        public RequestResult GetAllCity()
        {
            using (dbEntities db = new dbEntities())

            {
                List<CityDto> lst = new List<CityDto>();
                foreach (var item in db.city.ToList())
                {
                    lst.Add(CityDto.DalToDto(item));
                }
                return new RequestResult()
                {
                    Data = lst,
                    Message = "succesfull",
                    status = true
                };
            }
        }

        public void Addcity(CityDto c)
        {
            using (dbEntities db = new dbEntities())
            {
                db.city.Add(c.DtoToDal());
                db.SaveChanges();
            }
        }
    }
}
