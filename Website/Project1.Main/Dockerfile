FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Project1.Main/Project1.Main.csproj", "Project1.Main/"]
COPY ["Project1.Business/Project1.Business.csproj", "Project1.Business/"]
COPY ["Project1.DataAccess/Project1.DataAccess.csproj", "Project1.DataAccess/"]
RUN dotnet restore "Project1.Main/Project1.Main.csproj"
COPY . .
WORKDIR "/src/Project1.Main"
RUN dotnet build "Project1.Main.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Project1.Main.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Project1.Main.dll"]