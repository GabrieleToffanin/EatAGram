FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env

WORKDIR /src

COPY . .

RUN dotnet restore 

RUN dotnet test

RUN dotnet build ./Eatagram.Core.Api/Eatagram.Core.Api.csproj -c Release

RUN dotnet publish ./Eatagram.Core.Api/Eatagram.Core.Api.csproj -c Release -o output


FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime

EXPOSE 80 
EXPOSE 443

ENTRYPOINT ["dotnet", "Msc.Amazon.Api.dll"]
