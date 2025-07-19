using System;
using System.Collections.Generic;

namespace AMSWebApi.Models;

public partial class AssetType
{
    public int AssetTypeId { get; set; }

    public string TypeName { get; set; } = null!;

    public virtual ICollection<AssetDefinition> AssetDefinitions { get; set; } = new List<AssetDefinition>();
}
