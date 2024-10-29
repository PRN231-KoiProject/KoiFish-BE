using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KoiFish_Core.Domain.Content;
using KoiFish_Core.Models.Requests;
using KoiFish_Core.Models.Responses;
using KoiFish_Core.Repositories;
using KoiFish_Core.Services;

namespace KoiFish_Data.Services
{
    public class PondFeatureService : IPondFeatureService
    {
        private readonly IPondFeatureRepository _pondFeatureRepository;

        public PondFeatureService(IPondFeatureRepository pondFeatureRepository)
        {
            _pondFeatureRepository = pondFeatureRepository;
        }

        public async Task<bool> CreatePondFeature(CreatePondFeatureRequest request)
        {
            var pondFeature = new PondFeature
            {
                CompatibleFish = request.CompatibleFish,
                Direction = request.Direction,
                Element = request.Element,
                FilterType = request.FilterType,
                MaintenanceLevel = request.MaintenanceLevel,
                PondMaterial = request.PondMaterial,
                Position = request.Position,
                Shape = request.Shape,
                Size = request.Size,
                WaterSource = request.WaterSource,
            };
            _pondFeatureRepository.Add(pondFeature);
            await _pondFeatureRepository.SaveChangeAsync();
            return true;
        }

        public async Task<PageResult<PondFeatureResponse>> GetAllPondFeatureAsync(int page, int limit)
        {
            var paginatePonds = await _pondFeatureRepository.GetAllPondFeatureAsync(page, limit);
            var PondsResponse = new List<PondFeatureResponse>();
            foreach (var ponds in paginatePonds.Items)
            {
                PondsResponse.Add(new PondFeatureResponse
                {
                    CompatibleFish = ponds.CompatibleFish,
                    Direction = ponds.Direction,
                    Element = ponds.Element,
                    FilterType = ponds.FilterType,
                    MaintenanceLevel = ponds.MaintenanceLevel,
                    PondId = ponds.PondId,
                    PondMaterial = ponds.PondMaterial,
                    Position = ponds.Position,
                    Shape = ponds.Shape,
                    Size = ponds.Size,
                    WaterSource = ponds.WaterSource


                });

            }
            return new PageResult<PondFeatureResponse>
            {
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling(paginatePonds.TotalCount / (double)limit),
                TotalItems = paginatePonds.TotalCount,
                Items = PondsResponse
            };

        }

        public async Task<PondFeatureResponse> GetById(Guid id)
        {
            var ponds = await _pondFeatureRepository.GetByIdAsync(id);
            return new PondFeatureResponse
            {
                CompatibleFish = ponds.CompatibleFish,
                Direction = ponds.Direction,
                Element = ponds.Element,
                FilterType = ponds.FilterType,
                MaintenanceLevel = ponds.MaintenanceLevel,
                PondId = ponds.PondId,
                PondMaterial = ponds.PondMaterial,
                Position = ponds.Position,
                Shape = ponds.Shape,
                Size = ponds.Size,
                WaterSource = ponds.WaterSource
            };
        }

        public async Task<bool> UpdateAsync(UpdatePondFeatureRequest request, Guid id)
        {
            var pond = await _pondFeatureRepository.GetByIdAsync(id);
            pond.CompatibleFish = request.CompatibleFish;
            pond.Direction = request.Direction;
            pond.Element = request.Element;
            pond.FilterType = request.FilterType;
            pond.MaintenanceLevel = request.MaintenanceLevel;
            pond.Position = request.Position;
            pond.Shape = request.Shape;
            pond.Size = request.Size;
            pond.WaterSource = request.WaterSource;
            _pondFeatureRepository.Update(pond);
            await _pondFeatureRepository.SaveChangeAsync();
            return true;
        }
    }
}