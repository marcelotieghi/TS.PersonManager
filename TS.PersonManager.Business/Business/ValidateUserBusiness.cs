using Microsoft.EntityFrameworkCore;
using TS.PersonManager.Business.Interface;
using TS.PersonManager.Data.Context;

namespace TS.PersonManager.Business.Business;

public class ValidateUserBusiness(PersonContext context) : IValidateUserBusiness
{
    private readonly PersonContext _context = context;

    public async Task<bool> Validate(string login, string Password)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == login && x.Password == Password);

        return user != null;
    }
}