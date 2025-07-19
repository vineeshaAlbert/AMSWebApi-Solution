using AMSWebApi.Models;
using AMSWebApi.Repository;
using AMSWebApi.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace AMSWebApi.Service
{
    public class AssetService : IAssetService
    {
        private readonly IAssetRepository _assetRepository;

        public AssetService(IAssetRepository assetRepository)
        {
            _assetRepository = assetRepository;
        }

        #region 1 - Get All Assets
        public async Task<ActionResult<IEnumerable<Asset>>> GetAllAssetsAsync()
        {
            return await _assetRepository.GetAllAssetsAsync();
        }
        #endregion

        #region 2 - Get Asset By Id
        public async Task<ActionResult<Asset>> GetAssetByIdAsync(int id)
        {
            return await _assetRepository.GetAssetByIdAsync(id);
        }
        #endregion

        #region 3 - Search Assets By Name
        public async Task<ActionResult<IEnumerable<Asset>>> SearchAssetsByNameAsync(string name)
        {
            return await _assetRepository.SearchAssetsByNameAsync(name);
        }
        #endregion

        #region 4 - Create New Asset
        public async Task<ActionResult<Asset>> CreateAssetAsync(Asset asset)
        {
            // Get PO and validate
            var poResult = await _assetRepository.GetPurchaseOrderByIdAsync(asset.PurchaseOrderId);
            if (poResult.Result is NotFoundResult)
                return new BadRequestObjectResult("Invalid Purchase Order ID");

            var po = poResult.Value;
            if (po.Status != "Asset Details registered internally")
                return new BadRequestObjectResult("Purchase Order is not in a valid status to create an asset.");

            // Validate AssetDefinition
            var defResult = await _assetRepository.GetAssetDefinitionByIdAsync(asset.AssetDefinitionId);
            if (defResult.Result is NotFoundResult)
                return new BadRequestObjectResult("Invalid Asset Definition ID");

            // Proceed with creation
            return await _assetRepository.CreateAssetAsync(asset);
        }
        #endregion

        #region 5 - Update Asset
        public async Task<ActionResult<Asset>> UpdateAssetAsync(int id, Asset asset)
        {
            return await _assetRepository.UpdateAssetAsync(id, asset);
        }
        #endregion

        #region 6 - Delete Asset
        public async Task<ActionResult<string>> DeleteAssetAsync(int id)
        {
            return await _assetRepository.DeleteAssetAsync(id);
        }
        #endregion

        #region 7 - Search Asset By Name Using SP
        public async Task<ActionResult<IEnumerable<AssetViewModel>>> GetAssetsByNameFromSP(string name)
        {
            return await _assetRepository.GetAssetsByNameFromSP(name);
        }
        #endregion
    }
}
