﻿using MyEndProjectCode.Models;

namespace MyEndProjectCode.Services.Interfaces
{
    public interface IBrandService
    {
        Task<List<Brand>> GetAll();

    }
}