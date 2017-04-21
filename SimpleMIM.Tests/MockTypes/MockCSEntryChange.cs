using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.MetadirectoryServices;

namespace MIMSimplifier.Tests.MockTypes
{
    public class MockCSEntryChange: CSEntryChange
    {
        public override ObjectModificationType ObjectModificationType { get; set; }
        public override IList<string> ChangedAttributeNames { get; }
        public override Guid Identifier { get; }
        public override string DN { get; set; }
        public override string RDN { get; }
        public override string ObjectType { get; set; }
        public override MAImportError ErrorCodeImport { get; set; }
        public override KeyedCollection<string, AttributeChange> AttributeChanges { get; }
        public override KeyedCollection<string, AnchorAttribute> AnchorAttributes { get; }
        public override string ErrorName { get; set; }
        public override string ErrorDetail { get; set; }
    }
}
