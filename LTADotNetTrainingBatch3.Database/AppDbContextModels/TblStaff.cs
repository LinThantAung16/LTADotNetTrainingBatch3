using System;
using System.Collections.Generic;

namespace LTADotNetTrainingBatch3.Database.AppDbContextModels;

public partial class TblStaff
{
    public int StaffId { get; set; }

    public string StaffName { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;
}
