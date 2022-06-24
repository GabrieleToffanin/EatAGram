FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

COPY ./bin/Release/net6.0/ .

EXPOSE 80
EXPOSE 443

RUN dotnet restore

RUN dotnet test

RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet" ,"Eatagram.Core.Api.dll" ]