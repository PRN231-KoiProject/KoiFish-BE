using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using KoiFish_Core.Domain.Content;
using KoiFish_Core.Models.Requests;
using KoiFish_Core.Models.Responses;
using KoiFish_Core.Repositories;
using KoiFish_Core.Services;

namespace KoiFish_Data.Services
{
    public class KoiFishService : IKoiFishService
    {
        private readonly IKoiFishRepository _KoiFishRepository;
        private readonly IKoiFishColorRepository _koiFishColorRepository;
        private readonly IImageRepository _imageRepository;
        private readonly IMapper _mapper;
        public KoiFishService(IKoiFishRepository koiFishRepository
        , IKoiFishColorRepository koiFishColorRepository,
        IImageRepository imageRepository,
        IMapper mapper)
        {
            _KoiFishRepository = koiFishRepository;
            _koiFishColorRepository = koiFishColorRepository;
            _imageRepository = imageRepository;
            _mapper = mapper;
        }

        public async Task<bool> AddKoiFishAsync(CreateKoiFishRequest request)
        {
            var koiFish = new KoiFish
            {
                FishName = request.FishName,
                FishElement = request.FishElement,
                Size = request.Size,
                PriceRange = request.PriceRange,
                Lifespan = request.Lifespan,
                CategoryId = request.CategoryId,
                UserId = request.UserId,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            };
            _KoiFishRepository.Add(koiFish);
            foreach (var item in request.FishColors)
            {
                var fishColor = new FishColor
                {
                    ColorId = item.ColorId,
                    KoiFishId = koiFish.KoiFishId
                };
                _koiFishColorRepository.Add(fishColor);
            }
            foreach (var item in request.Images)
            {
                var image = new Image
                {
                    KoiFishId = koiFish.KoiFishId,
                    ImageUrl = item.ImageUrl
                };
                _imageRepository.Add(image);
            }
            await _KoiFishRepository.SaveChangeASync();
            return true;
        }

        public async Task<bool> DeleteKoiFish(Guid id)
        {
            var koiFishId = await _KoiFishRepository.GetKoiFishById(id);
            if (koiFishId == null)
            {
                return false;
            }
            _KoiFishRepository.Remove(koiFishId);
            await _KoiFishRepository.SaveChangeASync();
            return true;
        }

        public async Task<PageResult<KoiFishResponse>> GetAllKoiFishAsync(int page, int limit)
        {
            var paginateKoiFishes = await _KoiFishRepository.GetAllKoiFistAsync(page, limit);
            var KoiFishResponse = new List<KoiFishResponse>();
            foreach (var koiFish in paginateKoiFishes.Items)
            {
                KoiFishResponse.Add(new KoiFishResponse
                {
                    Category = koiFish.Category.Breeds,
                    KoiFishId = koiFish.KoiFishId,
                    User = koiFish.User.FullName,
                    FishName = koiFish.FishName,
                    FishElement = koiFish.FishElement,
                    Lifespan = koiFish.Lifespan,
                    PriceRange = koiFish.PriceRange,
                    Size = koiFish.Size,
                    Colors = koiFish.FishColors.Select(fc => new ColorResponses
                    {
                        ColorName = fc.Color.ColorName
                    }).ToList(),
                    Images = koiFish.Images.Select(i => new ImageResponses
                    {
                        ImageUrl = i.ImageUrl
                    }).ToList()
                });

            }
            return new PageResult<KoiFishResponse>
            {
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling(paginateKoiFishes.TotalCount / (double)limit),
                TotalItems = paginateKoiFishes.TotalCount,
                Items = KoiFishResponse
            };
        }

        public async Task<KoiFishResponse> GetKoiFishByIdAsync(Guid id)
        {
            var query = await _KoiFishRepository.GetKoiFishById(id);

            if (query == null)
            {
                return null;
            }

            return new KoiFishResponse
            {
                KoiFishId = query.KoiFishId,
                Category = query.Category?.Breeds,
                FishElement = query.FishElement,
                FishName = query.FishName,
                Lifespan = query.Lifespan,
                PriceRange = query.PriceRange,
                Size = query.Size,
                User = query.User?.FullName,
                Colors = query.FishColors?.Select(fc => new ColorResponses
                {
                    ColorName = fc.Color?.ColorName
                }).ToList() ?? new List<ColorResponses>(),
                Images = query.Images?.Select(i => new ImageResponses
                {
                    ImageUrl = i.ImageUrl
                }).ToList() ?? new List<ImageResponses>()
            };
        }


        public async Task<bool> UpdateKoiFishAsync(UpdateKoiFishRequest request, Guid id)
        {
            var koiFishId = await _KoiFishRepository.GetByIdAsync(id);
            if (koiFishId == null)
            {
                return false;
            }
            koiFishId.FishName = request.FishName;
            koiFishId.FishElement = request.FishElement;
            koiFishId.Size = request.Size;
            koiFishId.PriceRange = request.PriceRange;
            koiFishId.Lifespan = request.Lifespan;
            if (request.FishColors != null)
            {
                koiFishId.FishColors.Clear();
                foreach (var color in request.FishColors)
                {
                    koiFishId.FishColors.Add(new FishColor
                    {
                        KoiFishId = id,
                        ColorId = color.ColorId
                    });
                }
            }
            if (request.Images != null)
            {
                koiFishId.Images.Clear();
                foreach (var imageRequest in request.Images)
                {
                    koiFishId.Images.Add(new Image
                    {
                        KoiFishId = id,
                        ImageUrl = imageRequest.ImageUrl
                    });
                }
            }
            _KoiFishRepository.Update(koiFishId);
            await _KoiFishRepository.SaveChangeASync();
            return true;
        }
    }
}