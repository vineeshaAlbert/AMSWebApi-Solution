using System;
using System.Collections.Generic;

namespace AMSWebApi.Models;

public partial class Asset
{
    public int AssetId { get; set; }

    public int AssetDefinitionId { get; set; }

    public int PurchaseOrderId { get; set; }

    public DateTime CreatedDate { get; set; }

    public string? Status { get; set; }

    public virtual AssetDefinition AssetDefinition { get; set; } = null!;

    public virtual PurchaseOrder PurchaseOrder { get; set; } = null!;
}
