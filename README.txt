------ Read me -------
Webapplikasjoner mappe 2 - 2015 - Karianne og Sofia
Github: https://github.com/kariannelokke/Bokhandel
--------------------------------------------------------
Administrator
Brukernavn: Admin
Passord: Admin

Valg vi gjorde:

-	Administratoren kan ikke se kundenes passord eller registrere nye kunder.

-	Hvis man sletter en sjanger fra databasen vil alle b�kene registrert i den sjangeren ogs� slettes, samme gjelder forfatter.

-	Slettes en kunde slettes ogs� alle bestillingene.

-	 �\Admin\registrerAdmin siden er ikke passord beskyttet da den m�tte v�re �pen til � registrere nye admins hvis db er tom.

_____________________________________________________________________________________________________________________

OBS! Vi hadde litt tr�bbel med samkj�ring av databasene i mappe 1 og mappe 2.

N�r du starter prosjektet m� du gj�re disse stegene for � f� databasene til � fungere sammen:

- Build Solution

- Trykk p� �Butikk� i hovedmenyen f�rst. (kaller p� dbContext det �gamle� prosjektet)

- Deretter g� til �\Admin\registrerAdmin og registrer en administrator du kan bruke.

N� skal alt fungere.
