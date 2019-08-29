

using System;
using System.Linq;
using System.Threading.Tasks;
using AntilopaApi.Data;
using AntilopaApi.Models;
using AutoMapper;

namespace AntilopaApi.Services {

    public class CarService {
        private readonly Data.ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CarService(Data.ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            this._mapper = mapper;
        }

       public async Task<ServiceResponse<Car>> UpdateAsync(int id, CarInputModel inputModel) {            
            var carForUpdate = await this._context.Cars.FindAsync(id);
            if (carForUpdate == null) {
                return ServiceResponse<Car>.BadInput();
            }

            carForUpdate.Nickname = inputModel.Nickname;
            carForUpdate.RegistrationNr = inputModel.RegistrationNr;
            carForUpdate.Model = inputModel.Model;
            carForUpdate.PicUrl = inputModel.PicUrl;
            carForUpdate.UpdatedAt = DateTime.UtcNow;
            var res = this._context.Cars.Update(carForUpdate);
            await this._context.SaveChangesAsync();

            return ServiceResponse<Car>.Success(carForUpdate);
        }

        public async Task<ServiceResponse<Car>> InsertAsync(CarInputModel inputModel, int ownerId = 1) // TODO: repalce this when implement multiuser func.
        {
            var carForInsert = this._mapper.Map<CarInputModel, Car>(inputModel);
            carForInsert.OwnerId = ownerId;// TODO: put in _mapper
            var allCars = this._context.Cars.ToArray();
            var res = this._context.Cars.AddAsync(carForInsert);
            await this._context.SaveChangesAsync();

            return ServiceResponse<Car>.Success(carForInsert);
        }
    }
}