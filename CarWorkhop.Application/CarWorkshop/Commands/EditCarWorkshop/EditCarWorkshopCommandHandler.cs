using AutoMapper;
using CarWorkshop.Domain.Interfaces;
using MediatR;

namespace CarWorkshop.Application.CarWorkshop.Commands.EditCarWorkshop;

public class EditCarWorkshopCommandHandler : IRequestHandler<EditCarWorkshopCommand>
{
    private readonly ICarWorkshopRepository _repository;

    public EditCarWorkshopCommandHandler(ICarWorkshopRepository carWorkshopRepository)
    {
        _repository = carWorkshopRepository;
    }
    public async Task<Unit> Handle(EditCarWorkshopCommand request, CancellationToken cancellationToken)
    {
        var carWorkshop = await _repository.GetByEncodedName(request.EncodedName!);

        carWorkshop.Description = request.Description;
        carWorkshop.About = request.About;

        carWorkshop.ContactDetails.City = request.City;
        carWorkshop.ContactDetails.PhoneNumber = request.PhoneNumber;
        carWorkshop.ContactDetails.PostalCode = request.PostalCode;
        carWorkshop.ContactDetails.Street = request.Street;

        await _repository.Commit();

        return Unit.Value;
    }
}
