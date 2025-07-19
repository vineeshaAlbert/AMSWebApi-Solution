using AMSWebApi.Models;
using AMSWebApi.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace AMSWebApi.Repository
{
    public interface IAssetRepository
    {
        Task<ActionResult<IEnumerable<Asset>>> GetAllAssetsAsync();

        Task<ActionResult<Asset>> GetAssetByIdAsync(int id);

        Task<ActionResult<IEnumerable<Asset>>> SearchAssetsByNameAsync(string name);

        Task<ActionResult<Asset>> CreateAssetAsync(Asset asset);

        Task<ActionResult<Asset>> UpdateAssetAsync(int id, Asset asset);

        Task<ActionResult<string>> DeleteAssetAsync(int id);

        Task<ActionResult<PurchaseOrder>> GetPurchaseOrderByIdAsync(int poId);

        Task<ActionResult<AssetDefinition>> GetAssetDefinitionByIdAsync(int definitionId);

        Task<ActionResult<IEnumerable<AssetViewModel>>> GetAssetsByNameFromSP(string name);
    }
}
