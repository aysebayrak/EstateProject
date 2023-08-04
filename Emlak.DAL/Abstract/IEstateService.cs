using Emlak.Entity.Concrete;

namespace Emlak.DAL.Abstract
{
    public interface IEstateService
    {
        List<Estate> GetAll();

        List<Estate> GetByFilter(string? city, string? medicine, int? room, string? title, int? price, string? buildYear);
        Estate GetById(string id);

        Estate Create(Estate estate);
        void Update(string id, Estate estate);
        void Delete(string id);

    }
}
