namespace ordination_test;
using Microsoft.EntityFrameworkCore;

using Service;
using Data;
using shared;
using shared.Model;

[TestClass]
public class ServiceTest
{
    private DataService? service;

    
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
    public void PatientsExist()
    {
        Assert.IsNotNull(service.GetPatienter());
    }

    [TestMethod]
    public void OpretDagligFast()
    {
        Patient patient = service.GetPatienter().First();
        Laegemiddel lm = service.GetLaegemidler().First();

        Assert.AreEqual(1, service.GetDagligFaste().Count());

        service.OpretDagligFast(patient.PatientId, lm.LaegemiddelId,
            2, 2, 1, 0, DateTime.Now, DateTime.Now.AddDays(3));

        Assert.AreEqual(2, service.GetDagligFaste().Count());
    }

    
    //Test af gyldig data - DagligSkaev
    [TestMethod]
    public void OpretDagligSkaev()
    {
        
        Patient patient = service.GetPatienter().First();
        Laegemiddel lm = service.GetLaegemidler().First();

        //som default er der oprettet 1 dagligSkæv fra Seed data - dataservicen
        Assert.AreEqual(1, service.GetDagligSkæve().Count());

        //Gyldige data

        //TC1: KortOrdinationsPeriode
        //opretter en ny dagligSkæv 
        service.OpretDagligSkaev(patient.PatientId, lm.LaegemiddelId,
            new Dosis[] { 
                new Dosis(Util.CreateTimeOnly(12, 0, 0), 0.5),
                new Dosis(Util.CreateTimeOnly(12, 40, 0), 1),
                new Dosis(Util.CreateTimeOnly(16, 0, 0), 2.5),
                new Dosis(Util.CreateTimeOnly(18, 45, 0), 3)  

            }, new DateTime(2023, 01, 01), new DateTime(2023, 01,08));

        //nu tjekker man om der er oprettet to list listen - den skulle gerne kører.
        Assert.AreEqual(2, service.GetDagligSkæve().Count());

        //TC2: MellemLangOrdinationsPeriode
        //opretter en ny dagligSkæv 
        service.OpretDagligSkaev(patient.PatientId, lm.LaegemiddelId,
            new Dosis[] {
                new Dosis(Util.CreateTimeOnly(12, 0, 0), 0.5),
                new Dosis(Util.CreateTimeOnly(12, 40, 0), 1),
                new Dosis(Util.CreateTimeOnly(16, 0, 0), 2.5),
                new Dosis(Util.CreateTimeOnly(18, 45, 0), 3)

            }, new DateTime(2023, 01, 01), new DateTime(2023, 02, 01));

        //nu tjekker man om der er oprettet 3 til listen - den skulle gerne kører 
        Assert.AreEqual(3, service.GetDagligSkæve().Count());

        //TC3: LangOrdinationsPeriode
        //opretter en ny dagligSkæv 
        service.OpretDagligSkaev(patient.PatientId, lm.LaegemiddelId,
            new Dosis[] {
                new Dosis(Util.CreateTimeOnly(12, 0, 0), 0.5),
                new Dosis(Util.CreateTimeOnly(12, 40, 0), 1),
                new Dosis(Util.CreateTimeOnly(16, 0, 0), 2.5),
                new Dosis(Util.CreateTimeOnly(18, 45, 0), 3)

            }, new DateTime(2023, 01, 01), new DateTime(2024, 01, 01));

        //nu tjekker man om der er 4 listen - den skulle gerne kører 
        Assert.AreEqual(4, service.GetDagligSkæve().Count());

    }

    //Test af ugyldige data når man opretter men virker ikke 
    /*
    //Test af ugyldig data - DagligSkaev
    [TestMethod]
    public void OpretDagligSkaevFejl()
    {

        Patient patient = service.GetPatienter().First();
        Laegemiddel lm = service.GetLaegemidler().First();

        //som default er der oprettet 1 dagligSkæv fra Seed data - dataservicen
        Assert.AreEqual(1, service.GetDagligSkæve().Count());

        //TC4:StartDato1DagFørDagsDato - grænseværdi
        //opretter en ny dagligSkæv 
        service.OpretDagligSkaev(patient.PatientId, lm.LaegemiddelId,
            new Dosis[] {
                new Dosis(Util.CreateTimeOnly(12, 0, 0), 0.5),
                new Dosis(Util.CreateTimeOnly(12, 40, 0), 1),
                new Dosis(Util.CreateTimeOnly(16, 0, 0), 2.5),
                new Dosis(Util.CreateTimeOnly(18, 45, 0), 3)

            }, new DateTime(2023, 01, 01), new DateTime(2022, 12, 31));

        //Tjek at den man prøver at oprette fejler fordi der skal kun være den ene fra seed data 
        Assert.AreEqual(1, service.GetDagligSkæve().Count());

        //TC5:StartDato14DageFørDagsDato - grænseværdi
        //opretter en ny dagligSkæv 
        service.OpretDagligSkaev(patient.PatientId, lm.LaegemiddelId,
            new Dosis[] {
                new Dosis(Util.CreateTimeOnly(12, 0, 0), 0.5),
                new Dosis(Util.CreateTimeOnly(12, 40, 0), 1),
                new Dosis(Util.CreateTimeOnly(16, 0, 0), 2.5),
                new Dosis(Util.CreateTimeOnly(18, 45, 0), 3)

            }, new DateTime(2023, 01, 01), new DateTime(2023, 12, 18));

        //Tjek at den man prøver at oprette fejler fordi der skal kun være den ene fra seed data 
        Assert.AreEqual(1, service.GetDagligSkæve().Count());

        //TC6:ugyldigDato
        //opretter en ny dagligSkæv 
        service.OpretDagligSkaev(patient.PatientId, lm.LaegemiddelId,
            new Dosis[] {
                new Dosis(Util.CreateTimeOnly(12, 0, 0), 0.5),
                new Dosis(Util.CreateTimeOnly(12, 40, 0), 1),
                new Dosis(Util.CreateTimeOnly(16, 0, 0), 2.5),
                new Dosis(Util.CreateTimeOnly(18, 45, 0), 3)

            }, new DateTime(2023, 01, 01), new DateTime(08, 2023, 01));

        //Tjek at den man prøver at oprette fejler fordi der skal kun være den ene fra seed data 
        Assert.AreEqual(1, service.GetDagligSkæve().Count());
    }

    */

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestAtKodenSmiderEnException()
    {
        // Herunder skal man så kalde noget kode,
        // der smider en exception.

        // Hvis koden _ikke_ smider en exception,
        // så fejler testen.

        Console.WriteLine("Her kommer der ikke en exception. Testen fejler.");
    }
    

    
}