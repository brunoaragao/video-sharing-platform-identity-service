FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY Identity.API/Identity.API.csproj .
RUN dotnet restore
COPY Identity.API .
RUN dotnet publish -c Release -o /publish

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app
COPY --from=build /publish .
EXPOSE 80
ENTRYPOINT [ "dotnet", "Identity.API.dll" ]
