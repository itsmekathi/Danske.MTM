using System.ComponentModel;

namespace Danske.MTM.Common.Enums
{
    public enum eTaxScheduleTypes
    {
        [Description("Daily")]
        Daily = 1,
        [Description("Weekly")]
        Weekly,
        [Description("Monthly")]
        Monthly,
        [Description("Yearly")]
        Yearly
    }
}
