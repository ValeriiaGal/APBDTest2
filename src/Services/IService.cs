using DTOs;
using Models;

namespace Services;

public interface IService
{
    public Task<List<DriverDTO>> Get();
}