using DallEApiDotNet.Models;

namespace DallEApiDotNet.Services
{
    public interface IImageGenerator
    {
        Task<Response> GenerateImg(InputData inputData);
    }
}
