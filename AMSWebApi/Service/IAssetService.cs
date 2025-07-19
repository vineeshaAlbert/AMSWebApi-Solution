using AMSWebApi.Models;
using AMSWebApi.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace AMSWebApi.Service
{
    public interface IAssetService
    {
        #region 1 - Get All Assets
        Task<ActionResult<IEnumerable<Asset>>> GetAllAssetsAsync();
        #endregion

        #region 2 - Get Asset By Id
        Task<ActionResult<Asset>> GetAssetByIdAsync(int id);
        #endregion

        #region 3 - Search Assets By Name
        Task<ActionResult<IEnumerable<Asset>>> SearchAssetsByNameAsync(string name);
        #endregion

        #region 4 - Create New Asset
        Task<ActionResult<Asset>> CreateAssetAsync(Asset asset);
        #endregion

        #region 5 - Update Asset
        Task<ActionResult<Asset>> UpdateAssetAsync(int id, Asset asset);
        #endregion

        #region 6 - Delete Asset
        Task<ActionResult<string>> DeleteAssetAsync(int id);
        #endregion

        #region 7 - Search Asset By Name Using SP
        Task<ActionResult<IEnumerable<AssetViewModel>>> GetAssetsByNameFromSP(string name);
        #endregion
    }
}
