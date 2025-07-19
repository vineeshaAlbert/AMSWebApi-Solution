using System;
using System.Collections.Generic;

namespace AMSWebApi.Models;

public partial class AssetDefinition
{
    public int AssetDefinitionId { get; set; }

    public int AssetTypeId { get; set; }

    public string AssetName { get; set; } = null!;

    public virtual AssetType AssetType { get; set; } = null!;

    public virtual ICollection<Asset> Assets { get; set; } = new List<Asset>();

    public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; } = new List<PurchaseOrder>();
}
