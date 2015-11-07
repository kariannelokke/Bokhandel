using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BookStore.BLL;
using BookStore.Controllers;
using BookStore.DAL2;
using BookStore.Model;
using System.Web.Mvc;
using MvcContrib.TestHelper;


namespace Enhetstest
{
    [TestClass]
    public class AdminControllerTest
    {
        [TestMethod]
        public void Liste_vis_View() // test av liste() i admincontroller
        {
            var SessionMock = new TestControllerBuilder();

            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));

            SessionMock.InitializeController(controller);
            controller.Session["AdminLoggetInn"] = true;

            var forventetResultat = new List<Kunde>();
            var kunde = new Kunde()
            {
                id = 100,
                fornavn = "Ole",
                etternavn = "Olsen",
                adresse = "Storgata 1",
                postnr = "3557",
                poststed = "Molde"
            };

            forventetResultat.Add(kunde);
            forventetResultat.Add(kunde);
            forventetResultat.Add(kunde);


            var actionResult = (ViewResult)controller.Liste(); // kaller controller sin liste - returnerar Actionresult/Viewresult
            var resultat = (List<Kunde>)actionResult.Model; // får liste fra admincontroller, samme liste vi produserte i stub

            Assert.AreEqual(actionResult.ViewName, "");

            for (var i = 0; i < resultat.Count; i++)
            {
                Assert.AreEqual(forventetResultat[i].id, resultat[i].id);
                Assert.AreEqual(forventetResultat[i].fornavn, resultat[i].fornavn);
                Assert.AreEqual(forventetResultat[i].etternavn, resultat[i].etternavn);
                Assert.AreEqual(forventetResultat[i].adresse, resultat[i].adresse);
                Assert.AreEqual(forventetResultat[i].postnr, resultat[i].postnr);
                Assert.AreEqual(forventetResultat[i].poststed, resultat[i].poststed);
            }
        }


        [TestMethod]
        public void EndreKunde()
        {
            var SessionMock = new TestControllerBuilder();

            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));

            SessionMock.InitializeController(controller);
            controller.Session["AdminLoggetInn"] = true;

            // Act
            var actionResult = (ViewResult)controller.EndreKunde(1);

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
        }

        [TestMethod]
        public void EndreKunde_Ikke_Funnet_Ved_View()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();

            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));

            SessionMock.InitializeController(controller);
            controller.Session["AdminLoggetInn"] = true;

            // Act
            var actionResult = (ViewResult)controller.EndreKunde(0);
            var kundeResultat = (Kunde)actionResult.Model;

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
            Assert.AreEqual(kundeResultat.id, 0);
        }

        [TestMethod]
        public void EndreKunde_ikke_funnet_Post()
        {
            var SessionMock = new TestControllerBuilder();

            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));

            SessionMock.InitializeController(controller);
            controller.Session["AdminLoggetInn"] = true;

            var innKunde = new Kunde()
            {
                fornavn = "Ole",
                etternavn = "Olsen",
                adresse = "Storgata 1",
                postnr = "3557",
                poststed = "Molde"
            };

            // Act
            var actionResult = (ViewResult)controller.EndreKunde(0, innKunde);

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
        }

        [TestMethod]
        public void EndreKunde_feil_validering_Post()
        {
            var SessionMock = new TestControllerBuilder();

            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));

            SessionMock.InitializeController(controller);
            controller.Session["AdminLoggetInn"] = true;

            var innKunde = new Kunde();
            controller.ViewData.ModelState.AddModelError("feil", "ID = 0");

            // Act
            var actionResult = (ViewResult)controller.EndreKunde(0, innKunde);

            // Assert
            Assert.IsTrue(actionResult.ViewData.ModelState.Count == 1);
            Assert.AreEqual(actionResult.ViewData.ModelState["feil"].Errors[0].ErrorMessage, "ID = 0");
            Assert.AreEqual(actionResult.ViewName, "");
        }

        [TestMethod]
        public void EndreKunde_funnet()
        {
            var SessionMock = new TestControllerBuilder();

            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));

            SessionMock.InitializeController(controller);
            controller.Session["AdminLoggetInn"] = true;

            var innKunde = new Kunde()
            {
                fornavn = "Ole",
                etternavn = "Olsen",
                adresse = "Storgata 1",
                postnr = "3557",
                poststed = "Molde"
            };
            // Act
            var actionResultat = (RedirectToRouteResult)controller.EndreKunde(1, innKunde);

            // Assert
            Assert.AreEqual(actionResultat.RouteName, "");
            Assert.AreEqual(actionResultat.RouteValues.Values.First(), "Liste");
        }

        [TestMethod]
        public void Slett()
        {
            var SessionMock = new TestControllerBuilder();

            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));

            SessionMock.InitializeController(controller);
            controller.Session["AdminLoggetInn"] = true;

            // Act
            var actionResult = (ViewResult)controller.Slett(1);
            var resultat = (Kunde)actionResult.Model;

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");


        }

        [TestMethod]
        public void Slettet_funnet_Post()
        {
            var SessionMock = new TestControllerBuilder();

            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));

            SessionMock.InitializeController(controller);
            controller.Session["AdminLoggetInn"] = true;

            var innKunde = new Kunde()
            {
                fornavn = "Ole",
                etternavn = "Olsen",
                adresse = "Storgata 1",
                postnr = "3557",
                poststed = "Molde"
            };

            // Act
            var actionResult = (RedirectToRouteResult)controller.Slett(1, innKunde);

            // Assert
            Assert.AreEqual(actionResult.RouteName, "");
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "Liste");
        }
        [TestMethod]
        public void Slett_ikke_funnet_Post()
        {
            var SessionMock = new TestControllerBuilder();

            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));

            SessionMock.InitializeController(controller);
            controller.Session["AdminLoggetInn"] = true;

            var innKunde = new Kunde()
            {
                fornavn = "Ole",
                etternavn = "Olsen",
                adresse = "Storgata 1",
                postnr = "3557",
                poststed = "Molde"
            };

            // Act
            var actionResult = (ViewResult)controller.Slett(0, innKunde);

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
        }

        [TestMethod]
        public void SlettBok()
        {
            var SessionMock = new TestControllerBuilder();

            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));

            SessionMock.InitializeController(controller);
            controller.Session["AdminLoggetInn"] = true;

            var actionResult = (ViewResult)controller.SlettBok(1);
            var resultat = (Boken)actionResult.Model;

            Assert.AreEqual(actionResult.ViewName, "");


        }

        [TestMethod]
        public void Slettet_funnet_Post_Bok()
        {
            var SessionMock = new TestControllerBuilder();

            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));

            SessionMock.InitializeController(controller);
            controller.Session["AdminLoggetInn"] = true;

            var innBok = new Boken()
            {
                ForfatterId = 100,
                SjangerId = 100,
                Tittel = "Isprinsessen",
                Pris = 399,
                Sjanger = "Krim",
                Forfatter = "Camilla Läckberg"
            };

            // Act
            var actionResult = (RedirectToRouteResult)controller.SlettBok(1, innBok);

            // Assert
            Assert.AreEqual(actionResult.RouteName, "");
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "hentAlleBoker");
        }
        [TestMethod]
        public void Slett_ikke_funnet_Post_Bok()
        {
            var SessionMock = new TestControllerBuilder();

            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));

            SessionMock.InitializeController(controller);
            controller.Session["AdminLoggetInn"] = true;

            var innBok = new Boken()
            {
                ForfatterId = 100,
                SjangerId = 100,
                Tittel = "Isprinsessen",
                Pris = 399,
                Sjanger = "Krim",
                Forfatter = "Camilla Läckberg"
            };

            // Act
            var actionResult = (ViewResult)controller.SlettBok(0, innBok);

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
        }

        [TestMethod]
        public void EndreBok()
        {
            var SessionMock = new TestControllerBuilder();

            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));

            SessionMock.InitializeController(controller);
            controller.Session["AdminLoggetInn"] = true;

            // Act
            var actionResult = (ViewResult)controller.EndreBok(1);

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
        }

        [TestMethod]
        public void EndreBok_Ikke_Funnet_Ved_View()
        {
            var SessionMock = new TestControllerBuilder();

            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));

            SessionMock.InitializeController(controller);
            controller.Session["AdminLoggetInn"] = true;

            // Act
            var actionResult = (ViewResult)controller.EndreBok(0);
            var bokResultat = (Boken)actionResult.Model;

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
            Assert.AreEqual(bokResultat.ISBN, 0);
        }

        [TestMethod]
        public void EndreBok_ikke_funnet_Post()
        {
            var SessionMock = new TestControllerBuilder();

            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));

            SessionMock.InitializeController(controller);
            controller.Session["AdminLoggetInn"] = true;

            var innBok = new Boken()
            {
                ForfatterId = 100,
                SjangerId = 100,
                Tittel = "Isprinsessen",
                Pris = 399,
                Sjanger = "Krim",
                Forfatter = "Camilla Läckberg"
            };

            // Act
            var actionResult = (ViewResult)controller.EndreBok(0, innBok);

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
        }

        [TestMethod]
        public void EndreBok_feil_validering_Post()
        {
            var SessionMock = new TestControllerBuilder();

            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));

            SessionMock.InitializeController(controller);
            controller.Session["AdminLoggetInn"] = true;

            var innBok = new Boken();
            controller.ViewData.ModelState.AddModelError("feil", "ID = 0");

            // Act
            var actionResult = (ViewResult)controller.EndreBok(0, innBok);

            // Assert
            Assert.IsTrue(actionResult.ViewData.ModelState.Count == 1);
            Assert.AreEqual(actionResult.ViewData.ModelState["feil"].Errors[0].ErrorMessage, "ID = 0");
            Assert.AreEqual(actionResult.ViewName, "");
        }

        [TestMethod]
        public void EndreBok_funnet()
        {
            var SessionMock = new TestControllerBuilder();

            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));

            SessionMock.InitializeController(controller);
            controller.Session["AdminLoggetInn"] = true;

            var innBok = new Boken()
            {
                ForfatterId = 100,
                SjangerId = 100,
                Tittel = "Isprinsessen",
                Pris = 399,
                Sjanger = "Krim",
                Forfatter = "Camilla Läckberg"
            };
            // Act
            var actionResultat = (RedirectToRouteResult)controller.EndreBok(1, innBok);

            // Assert
            Assert.AreEqual(actionResultat.RouteName, "");
            Assert.AreEqual(actionResultat.RouteValues.Values.First(), "hentAlleBoker");
        }

        [TestMethod]
        public void registrerBok()
        {
            var SessionMock = new TestControllerBuilder();

            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));

            SessionMock.InitializeController(controller);
            controller.Session["AdminLoggetInn"] = true;

            // Act
            var actionResult = (ViewResult)controller.registrerBok();

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
        }
        [TestMethod]
        public void RegistrerBok_Post_OK()
        {
            var SessionMock = new TestControllerBuilder();

            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));

            SessionMock.InitializeController(controller);
            controller.Session["AdminLoggetInn"] = true;

            var forventetBok = new Boken()
            {
                ForfatterId = 100,
                SjangerId = 100,
                Tittel = "Isprinsessen",
                Pris = 399,
                Sjanger = "Krim",
                Forfatter = "Camilla Läckberg"
            };
            // Act
            var result = (RedirectToRouteResult)controller.registrerBok(forventetBok);

            // Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual(result.RouteValues.Values.First(), "hentAlleBoker");
        }
        [TestMethod]
        public void RegistrerBok_Post_Model_feil()
        {
            var SessionMock = new TestControllerBuilder();

            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));

            SessionMock.InitializeController(controller);
            controller.Session["AdminLoggetInn"] = true;

            var forventetBok = new Boken();
            controller.ViewData.ModelState.AddModelError("Tittel", "Ikke oppgitt tittel");

            // Act
            var actionResult = (ViewResult)controller.registrerBok(forventetBok);

            // Assert
            Assert.IsTrue(actionResult.ViewData.ModelState.Count == 1);
            Assert.AreEqual(actionResult.ViewName, "");
        }
        [TestMethod]
        public void RegistrerBok_Post_DB_feil()
        {
            var SessionMock = new TestControllerBuilder();

            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));

            SessionMock.InitializeController(controller);
            controller.Session["AdminLoggetInn"] = true;

            var forventetBok = new Boken();
            forventetBok.Tittel = "";

            // Act
            var actionResult = (ViewResult)controller.registrerBok(forventetBok);

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
        }

        [TestMethod]
        public void registrerSjanger()
        {
            var SessionMock = new TestControllerBuilder();

            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));

            SessionMock.InitializeController(controller);
            controller.Session["AdminLoggetInn"] = true;

            // Act
            var actionResult = (ViewResult)controller.registrerSjanger();

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
        }
        [TestMethod]
        public void RegistrerSjanger_Post_OK()
        {
            // Arrange
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));

            var forventetSjanger = new Sjangeren()
            {
                Navn = "Roman",
            };
            // Act
            var result = (RedirectToRouteResult)controller.registrerSjanger(forventetSjanger);

            // Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual(result.RouteValues.Values.First(), "hentSjangere");
        }
        [TestMethod]
        public void RegistrerSjanger_Post_Model_feil()
        {
            // Arrange
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));
            var forventetSjanger = new Sjangeren();
            controller.ViewData.ModelState.AddModelError("Navn", "Ikke oppgitt navn");

            // Act
            var actionResult = (ViewResult)controller.registrerSjanger(forventetSjanger);

            // Assert
            Assert.IsTrue(actionResult.ViewData.ModelState.Count == 1);
            Assert.AreEqual(actionResult.ViewName, "");
        }
        [TestMethod]
        public void RegistrerSjanger_Post_DB_feil()
        {
            // Arrange
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));
            var forventetSjanger = new Sjangeren();
            forventetSjanger.Navn = "";

            // Act
            var actionResult = (ViewResult)controller.registrerSjanger(forventetSjanger);

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
        }

        [TestMethod]
        public void SlettSjanger()
        {
            var SessionMock = new TestControllerBuilder();

            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));

            SessionMock.InitializeController(controller);
            controller.Session["AdminLoggetInn"] = true;

            var actionResult = (ViewResult)controller.SlettSjanger(1);
            var resultat = (Sjangeren)actionResult.Model;

            Assert.AreEqual(actionResult.ViewName, "");


        }

        [TestMethod]
        public void Slettet_funnet_Post_Sjanger()
        {
            var SessionMock = new TestControllerBuilder();

            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));

            SessionMock.InitializeController(controller);
            controller.Session["AdminLoggetInn"] = true;

            var innSjanger = new Sjangeren()
            {
                Navn = "Roman",

            };

            // Act
            var actionResult = (RedirectToRouteResult)controller.SlettSjanger(1, innSjanger);

            // Assert
            Assert.AreEqual(actionResult.RouteName, "");
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "hentSjangere");
        }

        [TestMethod]
        public void Slett_ikke_funnet_Post_Sjanger()
        {
            var SessionMock = new TestControllerBuilder();

            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));

            SessionMock.InitializeController(controller);
            controller.Session["AdminLoggetInn"] = true;

            var innSjanger = new Sjangeren()
            {
                Navn = "Roman",
            };

            // Act
            var actionResult = (ViewResult)controller.SlettSjanger(0, innSjanger);

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
        }


        [TestMethod]
        public void registrerForfatter()
        {
            var SessionMock = new TestControllerBuilder();

            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));

            SessionMock.InitializeController(controller);
            controller.Session["AdminLoggetInn"] = true;
            // Act
            var actionResult = (ViewResult)controller.registrerForfatter();

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
        }
        [TestMethod]
        public void registrerForfatter_Post_OK()
        {
            var SessionMock = new TestControllerBuilder();

            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));

            SessionMock.InitializeController(controller);
            controller.Session["AdminLoggetInn"] = true;

            var forventetForfatter = new Forfatteren()
            {
                Navn = "Sofia Aittamaa",
            };
            // Act
            var result = (RedirectToRouteResult)controller.registrerForfatter(forventetForfatter);

            // Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual(result.RouteValues.Values.First(), "hentForfattere");
        }
        [TestMethod]
        public void registrerForfatter_Post_Model_feil()
        {
            var SessionMock = new TestControllerBuilder();

            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));

            SessionMock.InitializeController(controller);
            controller.Session["AdminLoggetInn"] = true;

            var forventetForfatter = new Forfatteren();
            controller.ViewData.ModelState.AddModelError("Navn", "Ikke oppgitt navn");

            // Act
            var actionResult = (ViewResult)controller.registrerForfatter(forventetForfatter);

            // Assert
            Assert.IsTrue(actionResult.ViewData.ModelState.Count == 1);
            Assert.AreEqual(actionResult.ViewName, "");
        }
        [TestMethod]
        public void registrerForfatter_Post_DB_feil()
        {
            var SessionMock = new TestControllerBuilder();

            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));

            SessionMock.InitializeController(controller);
            controller.Session["AdminLoggetInn"] = true;

            var forventetForfatter = new Forfatteren();
            forventetForfatter.Navn = "";

            // Act
            var actionResult = (ViewResult)controller.registrerForfatter(forventetForfatter);

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
        }



        [TestMethod]
        public void VisBoker_vis_View()
        {
            var SessionMock = new TestControllerBuilder();

            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));

            SessionMock.InitializeController(controller);
            controller.Session["AdminLoggetInn"] = true;

            var forventetResultat = new List<Boken>();
            var bok = new Boken()
            {
                ISBN = 100,
                ForfatterId = 100,
                SjangerId = 100,
                Tittel = "Isprinsessen",
                Pris = 399,
                Sjanger = "Krim",
                Forfatter = "Camilla Läckberg"
            };

            forventetResultat.Add(bok);
            forventetResultat.Add(bok);
            forventetResultat.Add(bok);

            //act
            var actionResult = (ViewResult)controller.hentAlleBoker();
            var resultat = (List<Boken>)actionResult.Model;

            //assert 

            Assert.AreEqual(actionResult.ViewName, "");

            for (var i = 0; i < resultat.Count; i++)
            {
                Assert.AreEqual(forventetResultat[i].ISBN, resultat[i].ISBN);
                Assert.AreEqual(forventetResultat[i].ForfatterId, resultat[i].ForfatterId);
                Assert.AreEqual(forventetResultat[i].SjangerId, resultat[i].SjangerId);
                Assert.AreEqual(forventetResultat[i].Tittel, resultat[i].Tittel);
                Assert.AreEqual(forventetResultat[i].Pris, resultat[i].Pris);
                Assert.AreEqual(forventetResultat[i].Sjanger, resultat[i].Sjanger);
                Assert.AreEqual(forventetResultat[i].Forfatter, resultat[i].Forfatter);
            }
        }


        [TestMethod]
        public void VisSjangere_vis_View()
        {
            var SessionMock = new TestControllerBuilder();

            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));

            SessionMock.InitializeController(controller);
            controller.Session["AdminLoggetInn"] = true;

            var forventetResultat = new List<Sjangeren>();
            var sjanger = new Sjangeren()
            {
                Navn = "Roman",
            };

            forventetResultat.Add(sjanger);
            forventetResultat.Add(sjanger);
            forventetResultat.Add(sjanger);

            //act
            var actionResult = (ViewResult)controller.hentSjangere();
            var resultat = (List<Sjangeren>)actionResult.Model;

            //assert 

            Assert.AreEqual(actionResult.ViewName, "");

            for (var i = 0; i < resultat.Count; i++)
            {
                Assert.AreEqual(forventetResultat[i].Navn, resultat[i].Navn);

            }
        }

        [TestMethod]
        public void EndreSjanger()
        {
            var SessionMock = new TestControllerBuilder();

            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));

            SessionMock.InitializeController(controller);
            controller.Session["AdminLoggetInn"] = true;

            // Act
            var actionResult = (ViewResult)controller.EndreSjanger(1);

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
        }

        [TestMethod]
        public void EndreSjanger_Ikke_Funnet_Ved_View()
        {
            var SessionMock = new TestControllerBuilder();

            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));

            SessionMock.InitializeController(controller);
            controller.Session["AdminLoggetInn"] = true;

            // Act
            var actionResult = (ViewResult)controller.EndreSjanger(0);
            var sjangerResultat = (Sjangeren)actionResult.Model;

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
            Assert.AreEqual(sjangerResultat.Navn, null);
        }

        [TestMethod]
        public void EndreSjanger_ikke_funnet_Post()
        {
            var SessionMock = new TestControllerBuilder();

            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));

            SessionMock.InitializeController(controller);
            controller.Session["AdminLoggetInn"] = true;

            var innSjanger = new Sjangeren()
            {
                Navn = "Roman",

            };

            // Act
            var actionResult = (ViewResult)controller.EndreSjanger(0, innSjanger);

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
        }

        [TestMethod]
        public void EndreSjanger_feil_validering_Post()
        {
            var SessionMock = new TestControllerBuilder();

            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));

            SessionMock.InitializeController(controller);
            controller.Session["AdminLoggetInn"] = true;

            var innSjanger = new Sjangeren();
            controller.ViewData.ModelState.AddModelError("feil", "Navn = Humor");

            // Act
            var actionResult = (ViewResult)controller.EndreSjanger(0, innSjanger);

            // Assert
            Assert.IsTrue(actionResult.ViewData.ModelState.Count == 1);
            Assert.AreEqual(actionResult.ViewData.ModelState["feil"].Errors[0].ErrorMessage, "Navn = Humor");
            Assert.AreEqual(actionResult.ViewName, "");
        }

        [TestMethod]
        public void EndreSjanger_funnet()
        {
            var SessionMock = new TestControllerBuilder();

            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));

            SessionMock.InitializeController(controller);
            controller.Session["AdminLoggetInn"] = true;

            var innSjanger = new Sjangeren()
            {
                Navn = "Roman"
            };
            // Act
            var actionResultat = (RedirectToRouteResult)controller.EndreSjanger(1, innSjanger);

            // Assert
            Assert.AreEqual(actionResultat.RouteName, "");
            Assert.AreEqual(actionResultat.RouteValues.Values.First(), "hentSjangere");
        }

        [TestMethod]
        public void EndreForfatter()
        {
            var SessionMock = new TestControllerBuilder();

            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));

            SessionMock.InitializeController(controller);
            controller.Session["AdminLoggetInn"] = true;

            // Act
            var actionResult = (ViewResult)controller.EndreForfatter(1);

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
        }

        [TestMethod]
        public void EndreForfatter_Ikke_Funnet_Ved_View()
        {
            var SessionMock = new TestControllerBuilder();

            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));

            SessionMock.InitializeController(controller);
            controller.Session["AdminLoggetInn"] = true;

            // Act
            var actionResult = (ViewResult)controller.EndreForfatter(0);
            var forfatterResultat = (Forfatteren)actionResult.Model;

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
            Assert.AreEqual(forfatterResultat.Navn, null);
        }

        [TestMethod]
        public void EndreForfatter_ikke_funnet_Post()
        {
            // Arrange
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));
            var innForfatter = new Forfatteren()
            {
                Navn = "Sofia Aittamaa",
            };

            // Act
            var actionResult = (ViewResult)controller.EndreForfatter(0, innForfatter);

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
        }

        [TestMethod]
        public void EndreForfatter_feil_validering_Post()
        {
            // Arrange
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));
            var innForfatter = new Forfatteren();
            controller.ViewData.ModelState.AddModelError("feil", "Navn = Ida Aittamaa");

            // Act
            var actionResult = (ViewResult)controller.EndreForfatter(0, innForfatter);

            // Assert
            Assert.IsTrue(actionResult.ViewData.ModelState.Count == 1);
            Assert.AreEqual(actionResult.ViewData.ModelState["feil"].Errors[0].ErrorMessage, "Navn = Ida Aittamaa");
            Assert.AreEqual(actionResult.ViewName, "");
        }

        [TestMethod]
        public void EndreForfatter_funnet()
        {
            // Arrange
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));
            var innForfatter = new Forfatteren()
            {
                Navn = "Sofia Aittamaa"
            };
            // Act
            var actionResultat = (RedirectToRouteResult)controller.EndreForfatter(1, innForfatter);

            // Assert
            Assert.AreEqual(actionResultat.RouteName, "");
            Assert.AreEqual(actionResultat.RouteValues.Values.First(), "hentForfattere");
        }

        [TestMethod]
        public void ForfatterListe_vis_View()
        {
            var SessionMock = new TestControllerBuilder();

            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));

            SessionMock.InitializeController(controller);
            controller.Session["AdminLoggetInn"] = true;

            var forventetResultat = new List<Forfatteren>();
            var forfatter = new Forfatteren()
            {
                ForfatterId = 0,
                Navn = "Sofia Aittamaa",

            };

            forventetResultat.Add(forfatter);
            forventetResultat.Add(forfatter);
            forventetResultat.Add(forfatter);

            //act
            var actionResult = (ViewResult)controller.hentForfattere();
            var resultat = (List<Forfatteren>)actionResult.Model;

            //assert 

            Assert.AreEqual(actionResult.ViewName, "");

            for (var i = 0; i < resultat.Count; i++)
            {
                Assert.AreEqual(forventetResultat[i].ForfatterId, resultat[i].ForfatterId);
                Assert.AreEqual(forventetResultat[i].Navn, resultat[i].Navn);

            }
        }

        [TestMethod]
        public void VisOrdreDetaljer()
        {
            var SessionMock = new TestControllerBuilder();

            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));

            SessionMock.InitializeController(controller);
            controller.Session["AdminLoggetInn"] = true;

            var forventetResultat = new Bestilling()
            {
                BestillingsID = 100,
                KundeId = "sofia",
                BestillingsDato = new DateTime(2015, 11, 04, 08, 00, 00),
                Total = 0,
            };
            // Act
            var actionResult = (ViewResult)controller.VisOrdreDetaljer(1);
            var resultat = (Bestilling)actionResult.Model;

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
            Assert.AreEqual(forventetResultat.BestillingsID, resultat.BestillingsID);
            Assert.AreEqual(forventetResultat.KundeId, resultat.KundeId);
            Assert.AreEqual(forventetResultat.BestillingsDato, resultat.BestillingsDato);
            Assert.AreEqual(forventetResultat.Total, resultat.Total);
        }

        [TestMethod]
        public void VisOrdre_vis_View()
        {
            var SessionMock = new TestControllerBuilder();

            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));

            SessionMock.InitializeController(controller);
            controller.Session["AdminLoggetInn"] = true;

            var forventetResultat = new List<Bestilling>();
            var ordre = new Bestilling()
            {
                BestillingsID = 100,
                KundeId = "sofia",
                BestillingsDato = new DateTime(2015, 11, 04, 08, 00, 00),
                Total = 10,
            };

            forventetResultat.Add(ordre);
            forventetResultat.Add(ordre);
            forventetResultat.Add(ordre);

            //act
            var actionResult = (ViewResult)controller.VisOrdre(1);
            var resultat = (List<Bestilling>)actionResult.Model;

            //assert 

            Assert.AreEqual(actionResult.ViewName, "");

            for (var i = 0; i < resultat.Count; i++)
            {
                Assert.AreEqual(forventetResultat[i].BestillingsID, resultat[i].BestillingsID);
                Assert.AreEqual(forventetResultat[i].KundeId, resultat[i].KundeId);
                Assert.AreEqual(forventetResultat[i].BestillingsDato, resultat[i].BestillingsDato);
                Assert.AreEqual(forventetResultat[i].Total, resultat[i].Total);
            }
        }

        [TestMethod]
        public void registrerAdmin()
        {
            var SessionMock = new TestControllerBuilder();

            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));

            SessionMock.InitializeController(controller);
            controller.Session["AdminLoggetInn"] = true;

            // Act
            var actionResult = (ViewResult)controller.registrerAdmin();

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
        }
        [TestMethod]
        public void RegistrerAdmin_Post_OK()
        {
            var SessionMock = new TestControllerBuilder();

            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));

            SessionMock.InitializeController(controller);
            controller.Session["AdminLoggetInn"] = true;

            var forventetAdmin = new Administratoren()
            {
                Id = 100,
                Brukernavn = "Sofia",
               
            };
            // Act
            var result = (RedirectToRouteResult)controller.registrerAdmin(forventetAdmin);

            // Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual(result.RouteValues.Values.First(), "hentAlleBoker");
        }

        [TestMethod]
        public void RegistrerAdmin_Post_Model_feil()
        {
            var SessionMock = new TestControllerBuilder();

            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));

            SessionMock.InitializeController(controller);
            controller.Session["AdminLoggetInn"] = true;

            var forventetAdmin = new Administratoren();
            controller.ViewData.ModelState.AddModelError("Brukernavn", "Ikke oppgitt Brukernavn");

            // Act
            var actionResult = (ViewResult)controller.registrerAdmin(forventetAdmin);

            // Assert
            Assert.IsTrue(actionResult.ViewData.ModelState.Count == 1);
            Assert.AreEqual(actionResult.ViewName, "");
        }
        [TestMethod]
        public void RegistrerAdmin_Post_DB_feil()
        {
            var SessionMock = new TestControllerBuilder();

            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));

            SessionMock.InitializeController(controller);
            controller.Session["AdminLoggetInn"] = true;

            var forventetAdmin = new Administratoren();
            forventetAdmin.Brukernavn = "";

            // Act
            var actionResult = (ViewResult)controller.registrerAdmin(forventetAdmin);

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
        }


        [TestMethod]
        public void SlettForfatter()
        {
            var SessionMock = new TestControllerBuilder();

            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));

            SessionMock.InitializeController(controller);
            controller.Session["AdminLoggetInn"] = true;

            var actionResult = (ViewResult)controller.SlettForfatter(1);
            var resultat = (Forfatteren)actionResult.Model;

            Assert.AreEqual(actionResult.ViewName, "");


        }

        [TestMethod]
        public void SlettetForfatter_funnet_Post()
        {
            var SessionMock = new TestControllerBuilder();

            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));

            SessionMock.InitializeController(controller);
            controller.Session["AdminLoggetInn"] = true;

            var innForfatter = new Forfatteren()
            {
               
                Navn = "Sofia Aittamaa",
            };

            // Act
            var actionResult = (RedirectToRouteResult)controller.SlettForfatter(1, innForfatter);

            // Assert
            Assert.AreEqual(actionResult.RouteName, "");
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "hentForfattere");
        }

        [TestMethod]
        public void SlettForfatter_ikke_funnet_Post()
        {
            var SessionMock = new TestControllerBuilder();

            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));

            SessionMock.InitializeController(controller);
            controller.Session["AdminLoggetInn"] = true;

            var innForfatter = new Forfatteren()
            {
                Navn = "Sofia Aittamaa"
            };

            // Act
            var actionResult = (ViewResult)controller.SlettForfatter(0, innForfatter);

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
        }


        [TestMethod]
        public void adminSide_Vis_View()
        {
            var SessionMock = new TestControllerBuilder();

            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));

            SessionMock.InitializeController(controller);
            controller.Session["AdminLoggetInn"] = true;

            // Act
            var actionResult = (ViewResult)controller.adminSide();

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
        }

        [TestMethod]
        public void loggUtadmin_Vis_View()
        {
            var SessionMock = new TestControllerBuilder();

            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));

            SessionMock.InitializeController(controller);
            controller.Session["AdminLoggetInn"] = false;

            // Act
            var actionResult = (ViewResult)controller.loggUtadmin();

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
        }

        [TestMethod]
        public void Index_Vis_View()
        {
            var SessionMock = new TestControllerBuilder();

            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));

            SessionMock.InitializeController(controller);
            controller.Session["AdminLoggetInn"] = false;

            // Act
            var actionResult = (ViewResult)controller.Index();

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
        }

        [TestMethod]
        public void Index_Post_OK()
        {
            var SessionMock = new TestControllerBuilder();

            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));

            SessionMock.InitializeController(controller);
            controller.Session["AdminLoggetInn"] = true;

            var forventetAdmin = new Administratoren()
            {
                Id = 100,
                Brukernavn = "Sofia",

            };
            // Act
            var result = (RedirectToRouteResult)controller.Index(forventetAdmin);

            // Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual(result.RouteValues.Values.First(), "adminSide");
        }

    }
}

