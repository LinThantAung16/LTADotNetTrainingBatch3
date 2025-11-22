using System;
using System.Collections.Generic;

namespace LTADotNetTrainingBatch3.Database.AppDbContextModels;

public partial class TblProductCategory
{
    public int ProductCategoryId { get; set; }

    public string? ProductCategoryCode { get; set; }

    public string? ProductCategoryName { get; set; }
}
