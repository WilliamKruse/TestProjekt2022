namespace ordination_test;

using shared.Model;

[TestClass]
public class DagligFastTest
{
    
    [TestMethod]
    public void DoegnDosisTest()
    {
        // Gyldig data - 1 styk.
        // TC1: Test1Styk
        DagligFast tc1 = new DagligFast(new DateTime(2023, 01, 01), new DateTime(2023, 01, 08), new Laegemiddel("Acetylsalicylsyre", 0.1, 0.15, 0.16, "Styk"), 1, 0, 0, 0);

        double doegnDosis_tc1 = tc1.doegnDosis();

        Assert.AreEqual(1, doegnDosis_tc1);

        // Gyldig data - 3 styk.
        // TC2: Test3Styk
        DagligFast tc2 = new DagligFast(new DateTime(2023, 01, 01), new DateTime(2023, 02, 01), new Laegemiddel("Acetylsalicylsyre", 0.1, 0.15, 0.16, "Styk"), 0, 3, 0, 0);

        double doegnDosis_tc2 = tc2.doegnDosis();

        Assert.AreEqual(3, doegnDosis_tc2);

        // Gyldig data - 9 styk.
        // TC3: Test9Styk
        DagligFast tc3 = new DagligFast(new DateTime(2023, 01, 01), new DateTime(2024, 01, 01), new Laegemiddel("Acetylsalicylsyre", 0.1, 0.15, 0.16, "Styk"), 3, 3, 2, 1);
        
        double doegnDosis_tc3 = tc3.doegnDosis();

        Assert.AreEqual(9, doegnDosis_tc3);
        
    }

    // Ugyldig data testes
    [TestMethod]
    public void DoegnDosisTestFejl()
    {
        // Ugyldig - prøver at give minus-dosis.
        // TC4: TestMinusStykMorgen
        DagligFast tc4 = new DagligFast(new DateTime(2023, 01, 01), new DateTime(2023, 01, 08), new Laegemiddel("Acetylsalicylsyre", 0.1, 0.15, 0.16, "Styk"), -1, 1, 1, 1);

        double doegnDosis_tc4 = tc4.doegnDosis();

        Assert.AreEqual(-1, doegnDosis_tc4);

        // Ugyldig - prøver at give minus-dosis.
        // TC5: TestMinusStykMiddag
        DagligFast tc5 = new DagligFast(new DateTime(2023, 01, 01), new DateTime(2023, 01, 08), new Laegemiddel("Acetylsalicylsyre", 0.1, 0.15, 0.16, "Styk"), 1, -1, 0, 1);

        double doegnDosis_tc5 = tc5.doegnDosis();

        Assert.AreEqual(-1, doegnDosis_tc5);

        // Ugyldig - prøver at give minus-dosis.
        // TC6: TestMinusStykAften
        DagligFast tc6 = new DagligFast(new DateTime(2023, 01, 01), new DateTime(2023, 01, 08), new Laegemiddel("Acetylsalicylsyre", 0.1, 0.15, 0.16, "Styk"), 1, 0, -1, 1);

        double doegnDosis_tc6 = tc6.doegnDosis();

        Assert.AreEqual(-1, doegnDosis_tc6);

        // Ugyldig - prøver at give minus-dosis.
        // TC7: TestMinusStykNat
        DagligFast tc7 = new DagligFast(new DateTime(2023, 01, 01), new DateTime(2023, 01, 08), new Laegemiddel("Acetylsalicylsyre", 0.1, 0.15, 0.16, "Styk"), 1, 1, 0, -1);

        double doegnDosis_tc7 = tc7.doegnDosis();

        Assert.AreEqual(-1, doegnDosis_tc7);

    }

}