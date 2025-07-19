using AMSWebApi.Models;
using AMSWebApi.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace AMSWebApi.Repository
{
    public class AssetRepository : IAssetRepository
    {
        private readonly AssetsDbContext _context;

        public AssetRepository(AssetsDbContext context)
        {
            _context = context;
        }

        #region 1 - Get All Assets
        public async Task<ActionResult<IEnumerable<Asset>>> GetAllAssetsAsync()
        {
            var assets = await _context.Assets
                .Include(a => a.AssetDefinition)
                .Include(a => a.PurchaseOrder)
                .ToListAsync();

            return assets;
        }
        #endregion

        #region 2 - Get Asset By Id
        public async Task<ActionResult<Asset>> GetAssetByIdAsync(int id)
        {
            var asset = await _context.Assets
                .Include(a => a.AssetDefinition)
                .Include(a => a.PurchaseOrder)
                .FirstOrDefaultAsync(a => a.AssetId == id);

            if (asset == null)
                return new NotFoundResult();

            return asset;
        }
        #endregion

        #region 3 - Search Assets By Name
        public async Task<ActionResult<IEnumerable<Asset>>> SearchAssetsByNameAsync(string name)
        {
            var result = await _context.Assets
                .Include(a => a.AssetDefinition)
                .Include(a => a.PurchaseOrder)
                .Where(a => a.AssetDefinition.AssetName.Contains(name))
                .ToListAsync();

            return result;
        }
        #endregion

        #region 4 - Create New Asset
        public async Task<ActionResult<Asset>> CreateAssetAsync(Asset asset)
        {
            await _context.Assets.AddAsync(asset);
            var result = await _context.SaveChangesAsync();

            if (result > 0)
                return asset;

            return new BadRequestResult();
        }
        #endregion

        #region 5 - Update Asset
        public async Task<ActionResult<Asset>> UpdateAssetAsync(int id, Asset asset)
        {
            var existing = await _context.Assets.FindAsync(id);
            if (existing == null)
                return new NotFoundResult();

            existing.AssetDefinitionId = asset.AssetDefinitionId;
            existing.PurchaseOrderId = asset.PurchaseOrderId;
            existing.Status = asset.Status;
            existing.CreatedDate = asset.CreatedDate;

            _context.Assets.Update(existing);
            var result = await _context.SaveChangesAsync();

            if (result > 0)
                return existing;

            return new BadRequestResult();
        }
        #endregion

        #region 6 - Delete Asset
        public async Task<ActionResult<string>> DeleteAssetAsync(int id)
        {
            var asset = await _context.Assets.FindAsync(id);
            if (asset == null)
                return new NotFoundResult();

            _context.Assets.Remove(asset);
            var result = await _context.SaveChangesAsync();

            if (result > 0)
                return "Asset deleted successfully.";

            return new BadRequestResult();
        }
        #endregion

        #region 7 - Get PurchaseOrder By Id
        public async Task<ActionResult<PurchaseOrder>> GetPurchaseOrderByIdAsync(int poId)
        {
            var po = await _context.PurchaseOrders.FindAsync(poId);
            if (po == null)
                return new NotFoundResult();

            return po;
        }
        #endregion



        public async Task<ActionResult<IEnumerable<AssetViewModel>>> GetAssetsByNameFromSP(string name)
        {
            if (_context != null)
            {
                var param = new SqlParameter("@AssetName", name);

                var result = await _context.Set<AssetViewModel>()
                    .FromSqlRaw("EXEC sp_GetAssetsByName @AssetName", param)
                    .ToListAsync();

                return result;
            }
            return new NotFoundResult();
        }

        #region 8 - Get AssetDefinition By Id
        public async Task<ActionResult<AssetDefinition>> GetAssetDefinitionByIdAsync(int definitionId)
        {
            var def = await _context.AssetDefinitions.FindAsync(definitionId);
            if (def == null)
                return new NotFoundResult();

            return def;
        }
        #endregion


    }
}
