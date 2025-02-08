using Microsoft.AspNetCore.Http.HttpResults;
using NezerdoFarmApp.DTOs;
using NezerdoFarmApp.Interfaces;
using NezerdoFarmApp.Models;
using NezerdoFarmApp.Shared;

namespace NezerdoFarmApp.Services;

public class FarmServices:IFarmService
{
    private readonly ApplicationDbContext _context;
    public async Task<Result<Farm>> CreateFarmAsync(CreateFarmDto createFarmDto)
    {
        // _context.Farms.Add(createFarmDto);
        return (Result<Farm>)Result<Farm>.Success();
    }
}