# Kit de démarrage FranceConnect - Fournisseur de données (FD) #

Ce projet exemple met à disposition un canevas de site/API web en [ASP.NET Core](https://dotnet.microsoft.com/learn/aspnet/what-is-aspnet-core "ASP.NET Core") qui propose des ressources à un fournisseur de service, et qui utilise FranceConnect pour sécuriser cet échange. La version utilisée est ASP.NET Core 3.1 (LTS), ainsi que .NET 5.0 sur la branche concernée

Le canevas ainsi proposé peut être executé localement en suivant **[le tutoriel de démarrage rapide](/Source/README.md)** fourni dans ce répertoire, ou être testé directement à l'adresse <https://aka.ms/FranceConnect-FD>, sur laquelle le code de ce répertoire GitHub est continuellement déployé. Utilisez un des fournisseurs d'identité de démonstration lors de la connexion.

API web :

- Scopes : value1, value2
- Endpoint : <https://franceconnect-data-provider-dotnet-webapi-aspnetcore.azurewebsites.net/api/values>

L'adresse suivante est fournie pour ajouter des valeurs dans la base de données : <http://franceconnect-data-provider-dotnet-webapi-aspnetcore.azurewebsites.net/Account/Register>