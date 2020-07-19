FROM mcr.microsoft.com/dotnet/core/runtime:3.1 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build

WORKDIR /src
COPY ["src/EnvVariableTest/EnvVariableTest.csproj", "src/EnvVariableTest/"]
RUN dotnet restore "src/EnvVariableTest/EnvVariableTest.csproj"
COPY . .
WORKDIR "/src/src/EnvVariableTest"
RUN dotnet build "EnvVariableTest.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EnvVariableTest.csproj" -c Release -o /app/publish

FROM base AS final
ENV Host=local
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EnvVariableTest.dll"]
