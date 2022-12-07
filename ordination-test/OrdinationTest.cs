namespace ordination_test;

using Microsoft.EntityFrameworkCore;

using Service;
using Data;
using shared.Model;

[TestClass]
public class OrdinationTest
{
    private DataService service;
    //laver testdatabase og seeder data til den, så alt data er det samme hver test
    [TestInitialize]
    public void SetupBeforeEachTest()
    {
        var optionsBuilder = new DbContextOptionsBuilder<OrdinationContext>();
        optionsBuilder.UseInMemoryDatabase(databaseName: "test-database");
        var context = new OrdinationContext(optionsBuilder.Options);
        service = new DataService(context);
        service.SeedData();
    }


    [TestMethod]
    public void TestAnbefaledeEnhederPrDoegn()
    {

        Assert.AreEqual(9.51, Math.Round(service.GetAnbefaletDosisPerDøgn(1, 1), 2));
    }

}


