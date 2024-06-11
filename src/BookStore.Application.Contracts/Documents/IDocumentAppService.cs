using DocumentFormat.OpenXml.Packaging;
using System.IO;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace BookStore.Documents
{
    public interface IDocumentAppService : IApplicationService
    {
        MemoryStream GetDocumentAsync();
        MemoryStream GenerateExcelDocumentAsync();
    }
}
