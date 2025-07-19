using System;
using System.Collections.Generic;

namespace AMSWebApi.Models;

public partial class PurchaseOrder
{
    public int PurchaseOrderId { get; set; }

    public int AssetDefinitionId { get; set; }

    public string Status { get; set; } = null!;

    public virtual AssetDefinition AssetDefinition { get; set; } = null!;

    public virtual ICollection<Asset> Assets { get; set; } = new List<Asset>();
}
