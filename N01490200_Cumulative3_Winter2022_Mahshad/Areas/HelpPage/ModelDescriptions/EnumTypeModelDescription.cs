using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace N01490200_Cumulative3_Winter2022_Mahshad.Areas.HelpPage.ModelDescriptions
{
    public class EnumTypeModelDescription : ModelDescription
    {
        public EnumTypeModelDescription()
        {
            Values = new Collection<EnumValueDescription>();
        }

        public Collection<EnumValueDescription> Values { get; private set; }
    }
}