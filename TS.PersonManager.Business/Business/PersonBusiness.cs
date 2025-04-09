using Microsoft.EntityFrameworkCore;
using TS.PersonManager.Business.Interface;
using TS.PersonManager.Business.ViewModels;
using TS.PersonManager.Core.Entities;
using TS.PersonManager.Data.Context;

namespace TS.PersonManager.Business.Business;

public class PersonBusiness(PersonContext context) : IPersonBusiness
{
    private readonly PersonContext _context = context;

    public async Task<List<PersonViewModel>> GetAll()
    {
        return await _context.Persons
                            .Select(x => new PersonViewModel
                            {
                                Id = x.Id,
                                FirstName = x.FirstName,
                                LastName = x.LastName,
                                Email = x.Email
                            }).ToListAsync();
    }

    public async Task<PersonViewModel> GetById(int Id)
    {
        var model = await _context.Persons
                            .Where(x => x.Id == Id)
                            .Select(x => new PersonViewModel
                            {
                                FirstName = x.FirstName,
                                LastName = x.LastName,
                                Email = x.Email,
                                PhoneNumber = x.PhoneNumber,
                                DateOfBirth = x.DateOfBirth,
                                Gender = x.Gender.GetValueOrDefault(),
                                ImageName = x.ImageName
                            })
                            .FirstAsync();

        return model;
    }

    public async Task<int> CreateModify(PersonViewModel model)
    {
        Person data = new();

        if (model.Id > 0)
        {
            data = await _context.Persons.Where(x => x.Id == model.Id).FirstAsync();
        }

        data.FirstName = model.FirstName;
        data.LastName = model.LastName;
        data.Email = model.Email;
        data.PhoneNumber = model.PhoneNumber;
        data.DateOfBirth = model.DateOfBirth;
        data.Gender = model.Gender;

        if (model.ImageUpload != null && model.ImageUpload.Length > 0)
        {
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
            Directory.CreateDirectory(uploadsFolder);

            var uniqueFileName = $"{Guid.NewGuid()}_{Path.GetFileName(model.ImageUpload.FileName)}";
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await model.ImageUpload.CopyToAsync(stream);
            }

            data.ImageName = uniqueFileName;
        }

        if (model.Id > 0)
        {
            _context.Persons.Update(data);
        }
        else
        {
            _context.Persons.Add(data);
        }

        await _context.SaveChangesAsync();

        return data.Id;
    }

    public async Task Remove(int Id)
    {
        var data = await _context.Persons.Where(x => x.Id == Id).FirstAsync();
        _context.Persons.Remove(data);
        await _context.SaveChangesAsync();
    }
}