# EatagramBackend

## Simple ASP .NET Core API for managing Recipes based social

[![Build and Deploy - EatagramApi](https://github.com/GabrieleToffanin/EatAGram/actions/workflows/azure-webapps-dotnet-core.yml/badge.svg)](https://github.com/GabrieleToffanin/EatAGram/actions/workflows/azure-webapps-dotnet-core.yml)

### Storage :

This Api purpouse is to enable mostly `CRUD` operations persistent in the database.

- `MongoDB` :
  > Is used to maintain chats between users, so when a Reconnection happens users can reopen older chats.
- `EntityFramework` :
  > Uses Entity framework for persisting Users, Recipes, Comments etc... it is the main data persister.

### Authentication :

- `JWT`:
  > Actually the authentication is managed by Asp .Net Core Identity and JSON web token Bearer Auth. Possibly in the future the auth will be managed by `Microsoft` service.

### Deployment :

[!IMPORTANT] Still trying to understeand why dotnet test launched in the pipeline is getting stuck.

- `YML Pipeline` :
  > The pipeline [Azure-WebApps-Core-Deployment](.github/workflows/azure-webapps-dotnet-core.yml) starts on `main` branch when push, builds and creates an artifacts of the ZIP compressed application and then publish it on the Azure WebService

- `ElasticSearch and Kibana Experimenting` :
  >Acutally did some reserch about kibana and elastic search working togheter. Fact is i have inserted a Tracer into the Eatagram API and let the logs into Kibana. I filtered Them using the following REGEX ```
  (?<timestamp>[\d+\-\:\.].{23})\+\d+\:\d+\s(?<LogLevel>\[+[\w+\]]+)\s\((?<RunningOn>[\w+]+)\|\)\s\(\w+\)\s(?<HttpMethod>\[+[\w+\]]+)\s(?<Endpoint>[\w+\/]+)\s\w+\s+\w+\s*\'(?<EndpointParameters>[\{\D+\W+\d+\}\-*\'*].*)\'\s*\(*\w+\:*(?<AuthenticatedUser>[\w+\s*]+)\)\s*\-*\s*\w+\:*\s*\-*\s*\w*\:*\s*(?<Duration>[\d+\.\s\w*+]+)```.
  There is a stupid but working graph about some api Stats
  ![KibanaGraph](https://github.com/GabrieleToffanin/EatAGram/blob/develop/screenshots/KibanaGraphs.png)
