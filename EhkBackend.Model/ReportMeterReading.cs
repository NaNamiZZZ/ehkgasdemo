//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace EhkBackend.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class ReportMeterReading
    {
        public int id { get; set; }
        public string account { get; set; }
        public decimal meter { get; set; }
        public System.DateTime ReadDate { get; set; }
        public string email { get; set; }
        public Nullable<bool> delflag { get; set; }
    }
}