------ Read me -------
Webapplikasjoner mappe 2 - 2015 - Karianne og Sofia
Github: https://github.com/kariannelokke/Bokhandel
--------------------------------------------------------
Administrator
Brukernavn: Admin
Passord: Admin

Valg vi gjorde:

-	Administratoren kan ikke se kundenes passord eller registrere nye kunder.

-	Hvis man sletter en sjanger fra databasen vil alle bøkene registrert i den sjangeren også slettes, samme gjelder forfatter.

-	Slettes en kunde slettes også alle bestillingene.

-	 …\Admin\registrerAdmin siden er ikke passord beskyttet da den måtte være åpen til å registrere nye admins hvis db er tom.

_____________________________________________________________________________________________________________________

OBS! Vi hadde litt trøbbel med samkjøring av databasene i mappe 1 og mappe 2.

Når du starter prosjektet må du gjøre disse stegene for å få databasene til å fungere sammen:

- Build Solution

- Trykk på «Butikk» i hovedmenyen først. (kaller på dbContext det «gamle» prosjektet)

- Deretter gå til …\Admin\registrerAdmin og registrer en administrator du kan bruke.

Nå skal alt fungere.
