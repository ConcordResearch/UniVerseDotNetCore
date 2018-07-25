FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 56560
EXPOSE 44336

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ../../../../../../training/UniVerseDotNetCore/UniVerseDotNetCore.csproj ../../../../../../training/UniVerseDotNetCore/
RUN dotnet restore ../../../../../../training/UniVerseDotNetCore/UniVerseDotNetCore.csproj
COPY . .
WORKDIR /src/../../../../../../training/UniVerseDotNetCore
RUN dotnet build UniVerseDotNetCore.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish UniVerseDotNetCore.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "UniVerseDotNetCore.dll"]
