using KingReserveBack.PersonAdministration.Domain.Model.Commands;
using KingReserveBack.PersonAdministration.Domain.Model.Entities;
using KingReserveBack.PersonAdministration.Domain.Repositories;
using KingReserveBack.PersonAdministration.Domain.Services;
using KingReserveBack.Shared.Domain.Repositories;

namespace KingReserveBack.PersonAdministration.Application.Internal.CommandServices;

public class PersonCommandService:IPersonCommandService
{
    private readonly IPersonRepository _personRepository;
    private readonly IUnitOfWork _unitOfWork;

    public PersonCommandService(IPersonRepository personRepository,
        IUnitOfWork unitOfWork)
    {
        _personRepository = personRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Person?> Handle(CreatePersonCommand command)
    {
        var person = new Person(command);
        try
        {
            await _personRepository.AddAsync(person);
            await _unitOfWork.CompleteAsync();
            return person;
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred while creating a person: {e.Message}");
            return null;
        }
    }

    public async Task<Person?> Handle(UpdatePersonCommand command)
    {
        var person = await _personRepository.FindByIdAsync(command.PersonId);
        person.UpdateInformationPerson(command);
        try
        {
            await _unitOfWork.CompleteAsync();
            return person;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }

    public async Task<IEnumerable<Person>> Handle(DeletePersonCommand command)
    {
        var person = await _personRepository.FindByIdAsync(command.PersonId);
        _personRepository.Remove(person);
        var persons = await _personRepository.ListAsync();
        try
        {
            await _unitOfWork.CompleteAsync();
            Console.WriteLine("Person deleted successfully");
            return persons;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }
}