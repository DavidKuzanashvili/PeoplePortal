using People.Domain.Entities;

namespace People.API.Seeding.Dtos;

internal static class TransformCity
{
    internal static City Transform(CitySeedDto dto)
    {
        return City.Create(dto.Name);
    }
}
