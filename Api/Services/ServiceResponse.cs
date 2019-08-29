

using System;

namespace AntilopaApi.Services
{
    public class ServiceResponse<T> : Tuple<ServiceOpCodes, T>
    {
        public ServiceResponse(ServiceOpCodes code, T instance) : base(code, instance) { }

        public static ServiceResponse<T> BadInput() {
            return new ServiceResponse<T>(ServiceOpCodes.BAD_INPUT, default(T));
        }

        public static ServiceResponse<T> Success(T instance) {
            return new ServiceResponse<T>(ServiceOpCodes.SUCCESS, instance);
        }
        public bool isSuccess { get { return this.Item1 == ServiceOpCodes.SUCCESS; } }

    }
}