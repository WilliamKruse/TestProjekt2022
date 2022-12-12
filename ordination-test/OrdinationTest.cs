namespace ordination_test;

using shared.Model;

[TestClass]
public class OrdinationTest
{

    [TestMethod]
    public void AntalDageTest()
    {
        // Gyldig data
        // TC1: KortOrdinationsPeriode
        PN tc1 = new PN(new DateTime(2023, 01, 01), new DateTime(2023, 01, 08), 123, new Laegemiddel("Paracetamol", 1, 1.5, 2, "Ml"));

        int antalDage_tc1 = tc1.antalDage();

        Assert.AreEqual(8, antalDage_tc1);

        // TC2: MellemlangOrdinationsPeriode
        PN tc2 = new PN(new DateTime(2023, 01, 01), new DateTime(2023, 02, 01), 123, new Laegemiddel("Paracetamol", 1, 1.5, 2, "Ml"));

        int antalDage_tc2 = tc2.antalDage();

        Assert.AreEqual(32, antalDage_tc2);

        // TC3: langOrdinationsPeriode
        PN tc3 = new PN(new DateTime(2023, 01, 01), new DateTime(2024, 01, 01), 123, new Laegemiddel("Paracetamol", 1, 1.5, 2, "Ml"));

        int antalDage_tc3 = tc3.antalDage();

        Assert.AreEqual(366, antalDage_tc3);


    }

    [TestMethod]
    public void AntalDageTestFejl()
    {

        // Ugyldig data - slutdato en dag før startdato
        // TC4: SlutDato1DagFørStartDato
        PN tc4 = new PN(new DateTime(2023, 01, 01), new DateTime(2022, 12, 31), 123, new Laegemiddel("Paracetamol", 1, 1.5, 2, "Ml"));

        int antalDage_tc4 = tc4.antalDage();

        Assert.AreEqual(-1, antalDage_tc4);


        // Ugyldig data - slutdato 14 dage før startdato
        // TC5: SlutDato14DageFørStartDato
        PN tc5 = new PN(new DateTime(2023, 01, 01), new DateTime(2022, 12, 18), 123, new Laegemiddel("Paracetamol", 1, 1.5, 2, "Ml"));

        int antalDage_tc5 = tc5.antalDage();

        Assert.AreEqual(-1, antalDage_tc5);
        

    }

}

