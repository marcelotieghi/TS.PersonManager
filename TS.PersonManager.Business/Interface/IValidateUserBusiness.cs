namespace TS.PersonManager.Business.Interface;

public interface IValidateUserBusiness
{
    Task<bool> Validate(string login, string Password);
}