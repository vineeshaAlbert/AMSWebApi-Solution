using AMSWebApi.Models;
using AMSWebApi.Service;
using AMSWebApi.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace AMSWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetsController : ControllerBase
    {
        private readonly IAssetService _assetService;

        public AssetsController(IAssetService assetService)
        {
            _assetService = assetService;
        }

        #region 1 - GET: api/assets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Asset>>> GetAllAssets()
        {
            return await _assetService.GetAllAssetsAsync();
        }
        #endregion

        #region 2 - GET: api/assets/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Asset>> GetAssetById(int id)
        {
            var asset = await _assetService.GetAssetByIdAsync(id);
            if (asset?.Result is NotFoundResult)
            {
                return NotFound("Asset not found");
            }
            return Ok(asset.Value);
        }
        #endregion

        #region 3 - GET: api/assets/search
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Asset>>> SearchAssets([FromQuery] string name)
        {
            var result = await _assetService.SearchAssetsByNameAsync(name);
            return Ok(result.Value);
        }
        #endregion

        #region 4 - POST: api/assets
        [HttpPost]
        public async Task<ActionResult<Asset>> CreateAsset([FromBody] Asset asset)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _assetService.CreateAssetAsync(asset);
            if (result?.Result is BadRequestObjectResult badRequest)
            {
                return BadRequest(badRequest.Value);
            }

            return Ok(result.Value);
        }
        #endregion

        #region 5 - PUT: api/assets/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<Asset>> UpdateAsset(int id, [FromBody] Asset asset)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _assetService.UpdateAssetAsync(id, asset);
            if (result?.Result is NotFoundResult)
                return NotFound("Asset not found");

            return Ok(result.Value);
        }
        #endregion

        #region 6 - DELETE: api/assets/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> DeleteAsset(int id)
        {
            var result = await _assetService.DeleteAssetAsync(id);
            if (result?.Result is NotFoundResult)
                return NotFound("Asset not found");

            return Ok(result.Value);
        }
        #endregion

        #region 7 - GET: api/assets/sp-search?name=value
        [HttpGet("sp-search")]
        public async Task<ActionResult<IEnumerable<AssetViewModel>>> SearchAssetsUsingSP([FromQuery] string name)
        {
            var result = await _assetService.GetAssetsByNameFromSP(name);
            if (result?.Result is NotFoundResult || result?.Value == null || !result.Value.Any())
                return NotFound("No matching assets found via stored procedure.");

            return Ok(result.Value);
        }
        #endregion
    }
}
