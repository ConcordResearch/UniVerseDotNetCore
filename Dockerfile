FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 56560
EXPOSE 44336

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY UniVerseDotNetCore.csproj .
RUN dotnet restore UniVerseDotNetCore.csproj
COPY . .
RUN dotnet build UniVerseDotNetCore.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish UniVerseDotNetCore.csproj -c Release -o /app


FROM base AS final
WORKDIR /app
COPY --from=publish /app .

# Environment variables
#ENV RUN_IN_TEST_MODE "Yes"
ENV RUN_IN_TEST_MODE "No"

ENTRYPOINT ["dotnet", "UniVerseDotNetCore.dll"]
