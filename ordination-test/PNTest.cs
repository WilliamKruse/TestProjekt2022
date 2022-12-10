namespace ordination_test;

using shared.Model;

[TestClass]
public class PNTest
{

    [TestMethod]
    public void GivDosisTest()
    {
        // Gyldig data


        // TC1 - true hvis ordinationen gives inden for ordinationens gyldighedsperiode.
        // TC1: TestGivesDenMellemStartOgSlut
        PN tc1 = new PN(new DateTime(2023, 01, 01), new DateTime(2023, 01, 08), 123, new Laegemiddel("Fucidin", 0.025, 0.025, 0.025, "Styk"));

        bool givDosis_tc1 = tc1.givDosis(new Dato { dato = new DateTime(2023, 01, 05).Date });

        Assert.AreEqual(true, givDosis_tc1);

        // TC2 - true hvis ordinationen gives inden for ordinationens gyldighedsperiode.
        // TC2: TestGivesDenPåStart
        PN tc2 = new PN(new DateTime(2023, 01, 01), new DateTime(2023, 02, 01), 123, new Laegemiddel("Fucidin", 0.025, 0.025, 0.025, "Styk"));

        bool givDosis_tc2 = tc2.givDosis(new Dato { dato = new DateTime(2023, 01, 01).Date });

        Assert.AreEqual(true, givDosis_tc2);

        // TC3 - true hvis ordinationen gives inden for ordinationens gyldighedsperiode.
        // TC3: TestGivesDenPåSlut
        PN tc3 = new PN(new DateTime(2023, 01, 01), new DateTime(2024, 01, 01), 123, new Laegemiddel("Fucidin", 0.025, 0.025, 0.025, "Styk"));

        bool givDosis_tc3 = tc3.givDosis(new Dato { dato = new DateTime(2024, 01, 01).Date });

        Assert.AreEqual(true, givDosis_tc3);


        // Ugyldig data

        // TC4 - false hvis ordinationen gives uden for ordinationens gyldighedsperiode
        // TC4: TestGivesDenFørStart
        PN tc4 = new PN(new DateTime(2023, 01, 01), new DateTime(2023, 01, 12), 123, new Laegemiddel("Fucidin", 0.025, 0.025, 0.025, "Styk"));

        bool givDosis_tc4 = tc3.givDosis(new Dato { dato = new DateTime(2022, 12, 31).Date });

        Assert.AreEqual(false, givDosis_tc4);

        // TC5 - false hvis ordinationen gives uden for ordinationens gyldighedsperiode
        // TC5: TestGivesDenEfterSlut
        PN tc5 = new PN(new DateTime(2023, 01, 1), new DateTime(2023, 01, 12), 123, new Laegemiddel("Fucidin", 0.025, 0.025, 0.025, "Styk"));

        bool givDosis_tc5 = tc5.givDosis(new Dato { dato = new DateTime(2023, 01, 13).Date });

        Assert.AreEqual(false, givDosis_tc5);
        

    }
}