using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiBasicAuth.Tests.Fixtures;

// The CollectionFixture is can help use a Fixure across multiple
// test classes. In this case, for demostration purposes, it is
// used in a single file within this project.
//
// In this case, the Collection Fixtue makes the BasicAuthenticationHandlerFixture
// for all test classes that implement teh attribute 
// [Collection("BasicAuthenticationHandlerCollection")]


[CollectionDefinition("BasicAuthenticationHandlerCollection")]
public class BasicAuthenticationHandlerCollectionFixture
    : ICollectionFixture<BasicAuthenticationHandlerFixture>
{
}
