using TS.PersonManager.Business.ViewModels;

namespace TS.PersonManager.Business.Interface;

public interface IPersonBusiness
{
    Task<int> CreateModify(PersonViewModel model);
    Task<List<PersonViewModel>> GetAll();
    Task<PersonViewModel> GetById(int Id);
    Task Remove(int Id);
}