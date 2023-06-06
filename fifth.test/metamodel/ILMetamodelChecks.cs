using NUnit.Framework;

namespace Fifth.Test.metamodel;

using System.Linq;
using fifth.metamodel.metadata;

[TestFixture]
public class ILMetamodelChecks
{
    [Test]
    public void CanSeeClassesDerivedFromAbstractBases()
    {
        var bts = ILMetamodelProvider.BuildableTypes.ToArray();
        bts.Any(x => x.Name == "PropertyDefinition").Should().BeTrue();
    }
}
