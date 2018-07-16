Dokumentation ProfileBuilder:

WebApplications skapar inte ProfileCommon som default, därför finns detta projekt som generar en WebProfile

1: Skapa en <profile><properties><group name="PROJEKTNAMN"> i web-config för ProfileBuilder
2: Bygg ProfileBuilder
3: Lägg till properties även i web.config för WebSolution
4: Starta Visual Studio 2008 Command Prompt
5: Gå till projektmappen för ProfileBuilder (E:\aimitutv1\www\AimContent\WebSolutionLibraries\ProfileBuilder)
6: kör kommandot: csc /t:library WebProfile.cs
7: Lägg till dll WebProfile.dll som skapas i ditt projekt (via browse på reference, ej från BIN-mappen!!)